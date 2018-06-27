Public Class RealTimeDisplayBanner

#Region "Attached Properties"

    Public Shared ReadOnly DisplayNameProperty As  _
                           DependencyProperty = DependencyProperty.RegisterAttached("DisplayName", _
                           GetType(String), GetType(RealTimeDisplayBanner), _
                           New FrameworkPropertyMetadata("Banner"))

    Public Shared Function GetDisplayName(ByVal element As DependencyObject) As String
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        Return CStr(element.GetValue(DisplayNameProperty))
    End Function

    Public Shared Sub SetDisplayName(ByVal element As DependencyObject, ByVal value As String)
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If
        element.SetValue(DisplayNameProperty, value)
    End Sub

    Public Shared ReadOnly IsBannerVisibleProperty As DependencyProperty = _
                           DependencyProperty.RegisterAttached("IsBannerVisible", _
                           GetType(Boolean), GetType(RealTimeDisplayBanner), _
                           New FrameworkPropertyMetadata(False, New PropertyChangedCallback(AddressOf IsBannerVisibleChanged)))

    Public Shared Sub SetIsBannerVisible(ByVal element As DependencyObject, ByVal value As Boolean)
        element.SetValue(IsBannerVisibleProperty, value)
    End Sub

    Public Shared Function GetIsBannerVisible(ByVal element As DependencyObject) As Boolean
        Return CBool(element.GetValue(IsBannerVisibleProperty))
    End Function

    Public Shared Sub IsBannerVisibleChanged(ByVal d As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
        If Not TypeOf d Is Window Then Return
        Dim wnd As Window = DirectCast(d, Window)
        If CBool(args.NewValue) Then
            AddHandler wnd.Loaded, AddressOf Window_Loaded
        End If
    End Sub

    Public Shared Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Try
            Dim Wnd = TryCast(DirectCast(sender, DependencyObject), Window)
            Dim el As UIElement = TryCast(Wnd.Content, UIElement)
            Dim dp As New DockPanel()
            Dim sp As New StackPanel()
            Dim Banner As New RealTimeDisplayBanner
            Banner.Height = 70
            dp.LastChildFill = True
            dp.Children.Add(sp)
            sp.Orientation = Orientation.Vertical
            DockPanel.SetDock(sp, Dock.Top)
            sp.Children.Add(Banner)
            DockPanel.SetDock(el, Dock.Bottom)
            TryCast(DirectCast(sender, DependencyObject), Window).Content = Nothing
            dp.Children.Add(el)
            TryCast(DirectCast(sender, DependencyObject), Window).Content = dp
            Banner.DisplayNameTextBlock.Text = GetDisplayName(Wnd)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(String.Format("Exception : {0}", ex.Message))
        End Try
    End Sub

#End Region

End Class
