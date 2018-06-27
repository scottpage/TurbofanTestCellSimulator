Public Class DomainUpDown
    Inherits Control

#Region "Fields"
    Private _SelectedIndex As Integer
    Private _UpButton As RepeatButton
    Private _DownButton As RepeatButton
#End Region
#Region "Dependency properties"
    Public Shared ReadOnly ValueProperty As DependencyProperty = DependencyProperty.Register("Value", GetType(Object), GetType(DomainUpDown), New FrameworkPropertyMetadata(Nothing, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, AddressOf OnValueChanged))

    Public Shared ReadOnly ItemsProperty As DependencyProperty = DependencyProperty.Register("Items", GetType(IEnumerable), GetType(DomainUpDown), New PropertyMetadata(AddressOf OnItemsChanged))
#End Region

    ''' <summary>
    ''' Initializes the <see cref="DomainUpDown"/> class.
    ''' </summary>
    Shared Sub New()
        DefaultStyleKeyProperty.OverrideMetadata(GetType(DomainUpDown), New FrameworkPropertyMetadata(GetType(DomainUpDown)))
        BorderBrushProperty.OverrideMetadata(GetType(DomainUpDown), New FrameworkPropertyMetadata(SystemColors.ControlLightBrush))
    End Sub

    ''' <summary>
    ''' Gets or sets the selected value.
    ''' </summary>
    ''' <value>The value.</value>
    Public Property Value() As Object
        Get
            Return GetValue(ValueProperty)
        End Get
        Set(value As Object)
            SetValue(ValueProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the items.
    ''' </summary>
    ''' <value>The items.</value>
    Public Property Items() As IEnumerable
        Get
            Return TryCast(GetValue(ItemsProperty), IEnumerable)
        End Get
        Set(value As IEnumerable)
            SetValue(ItemsProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the index of the selected value.
    ''' </summary>
    ''' <value>The index of the selected.</value>
    Protected Property SelectedIndex() As Integer
        Get
            Return _SelectedIndex
        End Get
        Set(value As Integer)
            If _SelectedIndex = value Then Return
            _SelectedIndex = value
            Me.Value = Items.Cast(Of Object)().Skip(SelectedIndex).First
        End Set
    End Property

    Protected Overrides Sub OnPreviewKeyDown(e As KeyEventArgs)
        MyBase.OnPreviewKeyDown(e)

        If e.Key = Key.Down Then
            If _DownButton IsNot Nothing Then
                _DownButton.Focus()
            End If

            OnDown(Me, Nothing)
            e.Handled = True
        End If

        If e.Key = Key.Up Then
            If _UpButton IsNot Nothing Then
                _UpButton.Focus()
            End If

            OnUp(Me, Nothing)
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' When overridden in a derived class, is invoked whenever application code or internal processes call 
    ''' <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
    ''' </summary>
    Public Overrides Sub OnApplyTemplate()
        MyBase.OnApplyTemplate()

        If _UpButton IsNot Nothing Then
            RemoveHandler _UpButton.Click, AddressOf OnUp
        End If

        If _DownButton IsNot Nothing Then
            RemoveHandler _DownButton.Click, AddressOf OnDown
        End If

        ' Get the parts and attach event handlers to them
        _UpButton = TryCast(GetTemplateChild("PART_UpButton"), RepeatButton)
        _DownButton = TryCast(GetTemplateChild("PART_DownButton"), RepeatButton)

        If _UpButton IsNot Nothing Then
            AddHandler _UpButton.Click, AddressOf OnUp
        End If

        If _DownButton IsNot Nothing Then
            AddHandler _DownButton.Click, AddressOf OnDown
        End If
    End Sub

    ''' <summary>
    ''' Invoked whenever an unhandled <see cref="E:System.Windows.UIElement.GotFocus"/> event reaches this element in its route.
    ''' </summary>
    ''' <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
    Protected Overrides Sub OnGotFocus(e As RoutedEventArgs)
        MyBase.OnGotFocus(e)

        ' Move focus immediately to the buttons
        If _UpButton IsNot Nothing Then
            _UpButton.Focus()
        End If
    End Sub

    Private Sub OnUp(sender As Object, routedEventArgs As RoutedEventArgs)
        If SelectedIndex > 0 Then
            SelectedIndex -= 1
        End If
    End Sub

    Private Sub OnDown(sender As Object, routedEventArgs As RoutedEventArgs)
        If Items IsNot Nothing AndAlso SelectedIndex < Items.Cast(Of Object)().Count() - 1 Then
            SelectedIndex += 1
        End If
    End Sub

    Private Shared Sub OnValueChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim updown = TryCast(d, DomainUpDown)
        SynchroniseValueWithIndex(updown, e.NewValue, e.OldValue)
    End Sub

    Private Shared Sub OnItemsChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim updown = TryCast(d, DomainUpDown)
        SynchroniseValueWithIndex(updown, If(updown Is Nothing, Nothing, updown.Value), e.OldValue)
    End Sub

    Private Shared Sub SynchroniseValueWithIndex(updown As DomainUpDown, newValue As Object, oldValue As Object)
        If newValue Is Nothing OrElse updown Is Nothing OrElse updown.Items Is Nothing Then Return

        Dim i As Integer = 0
        Dim Found As Boolean = False

        For Each element In updown.Items
            If element.ToString.Equals(newValue.ToString, StringComparison.InvariantCultureIgnoreCase) Then
                updown.SelectedIndex = i
                Found = True
                Exit For
            End If

            i += 1
        Next
        If Not Found Then updown.Value = oldValue
    End Sub

End Class
