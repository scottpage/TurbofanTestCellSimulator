Public Class ThreeTwoStateStatusButtonPanel

    <Category("Common")> _
    Public Property LeftButtonCommand As ICommand
        Get
            Return TryCast(GetValue(LeftButtonCommandProperty), ICommand)
        End Get

        Set(ByVal value As ICommand)
            SetValue(LeftButtonCommandProperty, value)
        End Set
    End Property

    Public Shared ReadOnly LeftButtonCommandProperty As DependencyProperty = _
                           DependencyProperty.Register("LeftButtonCommand", _
                           GetType(ICommand), GetType(ThreeTwoStateStatusButtonPanel), _
                           New FrameworkPropertyMetadata(Nothing))

    <Category("Common")> _
    Public Property MiddleButtonCommand As ICommand
        Get
            Return TryCast(GetValue(MiddleButtonCommandProperty), ICommand)
        End Get

        Set(ByVal value As ICommand)
            SetValue(MiddleButtonCommandProperty, value)
        End Set
    End Property

    Public Shared ReadOnly MiddleButtonCommandProperty As DependencyProperty = _
                           DependencyProperty.Register("MiddleButtonCommand", _
                           GetType(ICommand), GetType(ThreeTwoStateStatusButtonPanel), _
                           New FrameworkPropertyMetadata(Nothing))

    <Category("Common")> _
    Public Property RightButtonCommand As ICommand
        Get
            Return TryCast(GetValue(RightButtonCommandProperty), ICommand)
        End Get

        Set(ByVal value As ICommand)
            SetValue(RightButtonCommandProperty, value)
        End Set
    End Property

    Public Shared ReadOnly RightButtonCommandProperty As DependencyProperty = _
                           DependencyProperty.Register("RightButtonCommand", _
                           GetType(ICommand), GetType(ThreeTwoStateStatusButtonPanel), _
                           New FrameworkPropertyMetadata(Nothing))

    <Category("Common")> _
    Public Property State As StartSelectorState
        Get
            Return DirectCast(GetValue(StateProperty), StartSelectorState)
        End Get
        Set(ByVal value As StartSelectorState)
            SetValue(StateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateProperty As DependencyProperty = _
                           DependencyProperty.Register("State", _
                           GetType(StartSelectorState), GetType(ThreeTwoStateStatusButtonPanel), _
                           New FrameworkPropertyMetadata(StartSelectorState.Normal, New PropertyChangedCallback(AddressOf StateChanged)))

    Private Shared Sub StateChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim O = DirectCast(d, ThreeTwoStateStatusButtonPanel)
        Dim State = DirectCast(e.NewValue, StartSelectorState)
        Select Case State
            Case StartSelectorState.Crank
                O.IgnitionStartButton.IsEnabled = False
            Case StartSelectorState.IgnitionStart
                O.CrankButton.IsEnabled = False
            Case StartSelectorState.Normal
                O.CrankButton.IsEnabled = True
                O.IgnitionStartButton.IsEnabled = True
        End Select
    End Sub

End Class
