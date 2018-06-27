Public Class TwoStateStatusButton

    Public Shared ReadOnly StateProperty As DependencyProperty = DependencyProperty.Register("State", GetType(Boolean), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(False))
    <Category("Common")> _
    Public Property State As Boolean
        Get
            Return CType(GetValue(StateProperty), Boolean)
        End Get
        Set(ByVal value As Boolean)
            SetValue(StateProperty, value)
        End Set
    End Property

#Region "Button Properties"

    Public Shared ReadOnly IsButtonIndicatorEnabledProperty As DependencyProperty = DependencyProperty.Register("IsButtonIndicatorEnabled", GetType(Boolean), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(True))
    <Category("Common")> _
    Public Property IsButtonIndicatorEnabled As Boolean
        Get
            Return CType(GetValue(IsButtonIndicatorEnabledProperty), Boolean)
        End Get
        Set(ByVal value As Boolean)
            SetValue(IsButtonIndicatorEnabledProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ButtonIndicatorFillProperty As DependencyProperty = DependencyProperty.Register("ButtonIndicatorFill", GetType(Brush), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(Brushes.Lime))
    <Category("Brushes")> _
    Public Property ButtonIndicatorFill As Brush
        Get
            Return CType(GetValue(ButtonIndicatorFillProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(ButtonIndicatorFillProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ButtonIndicatorVisiblityProperty As DependencyProperty = DependencyProperty.Register("ButtonIndicatorVisiblity", GetType(Visibility), GetType(TwoStateButton), New FrameworkPropertyMetadata(Visibility.Hidden))
    <Category("Visiblity")> _
    Public Property ButtonIndicatorVisiblity As Visibility
        Get
            Return CType(GetValue(ButtonIndicatorVisiblityProperty), Visibility)
        End Get
        Set(ByVal value As Visibility)
            SetValue(ButtonIndicatorVisiblityProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ButtonBackgroundProperty As DependencyProperty = DependencyProperty.Register("ButtonBackground", GetType(Brush), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(Brushes.Red))
    <Category("Brushes")> _
    Public Property ButtonBackground As Brush
        Get
            Return CType(GetValue(ButtonBackgroundProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(ButtonBackgroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ButtonTopTextProperty As DependencyProperty = DependencyProperty.Register("ButtonTopText", GetType(String), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(String.Empty))
    <Category("Text")> _
    Public Property ButtonTopText As String
        Get
            Return Convert.ToString(GetValue(ButtonTopTextProperty))
        End Get
        Set(ByVal value As String)
            SetValue(ButtonTopTextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ButtonBottomTextProperty As DependencyProperty = DependencyProperty.Register("ButtonBottomText", GetType(String), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(String.Empty))
    <Category("Text")> _
    Public Property ButtonBottomText As String
        Get
            Return Convert.ToString(GetValue(ButtonBottomTextProperty))
        End Get
        Set(ByVal value As String)
            SetValue(ButtonBottomTextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ButtonMarginProperty As DependencyProperty = DependencyProperty.Register("ButtonMargin", GetType(Thickness), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(New Thickness(5)))
    <Category("Layout")> _
    Public Property ButtonMargin As Thickness
        Get
            Return DirectCast(GetValue(ButtonMarginProperty), Thickness)
        End Get
        Set(ByVal value As Thickness)
            SetValue(ButtonMarginProperty, value)
        End Set
    End Property

#End Region

    Public Shared ReadOnly StatusLabelBackgroundBrushProperty As DependencyProperty = DependencyProperty.Register("StatusLabelBackgroundBrush", GetType(Brush), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(Nothing))
    <Category("Brushes")> _
    Public Property StatusLabelBackgroundBrush As Brush
        Get
            Return CType(GetValue(StatusLabelBackgroundBrushProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(StatusLabelBackgroundBrushProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TopStatusTextProperty As DependencyProperty = DependencyProperty.Register("TopStatusText", GetType(String), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(String.Empty))
    <Category("Text")> _
    Public Property TopStatusText As String
        Get
            Return Convert.ToString(GetValue(TopStatusTextProperty))
        End Get
        Set(ByVal value As String)
            SetValue(TopStatusTextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly BottomStatusTextProperty As DependencyProperty = DependencyProperty.Register("BottomStatusText", GetType(String), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(String.Empty))
    <Category("Text")> _
    Public Property BottomStatusText As String
        Get
            Return Convert.ToString(GetValue(BottomStatusTextProperty))
        End Get
        Set(ByVal value As String)
            SetValue(BottomStatusTextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ButtonBorderBrushProperty As DependencyProperty = DependencyProperty.Register("ButtonBorderBrush", GetType(Brush), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(Nothing))
    <Category("Brushes")> _
    Public Property ButtonBorderBrush As Brush
        Get
            Return DirectCast(GetValue(ButtonBorderBrushProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(ButtonBorderBrushProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CommandProperty As DependencyProperty = DependencyProperty.Register("Command", GetType(ICommand), GetType(TwoStateStatusButton), New FrameworkPropertyMetadata(Nothing))
    <Category("Common")> _
    Public Property Command As ICommand
        Get
            Return DirectCast(GetValue(CommandProperty), ICommand)
        End Get
        Set(ByVal value As ICommand)
            SetValue(CommandProperty, value)
        End Set
    End Property

    Private Sub OnOffButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        State = Not State
    End Sub

    Private Sub TwoStateStatusButton_IsEnabledChanged(sender As Object, e As System.Windows.DependencyPropertyChangedEventArgs) Handles Me.IsEnabledChanged
        If Not IsEnabled Then State = False
    End Sub

End Class
