Public Class StartSelectorStateStatusButton

    Public Shared ReadOnly TopStatusTextProperty As DependencyProperty = DependencyProperty.Register("TopStatusText", GetType(String), GetType(StartSelectorStateStatusButton), New FrameworkPropertyMetadata("Row 1"))
    Public Property TopStatusText As String
        Get
            Return Convert.ToString(GetValue(TopStatusTextProperty))
        End Get
        Set(ByVal value As String)
            SetValue(TopStatusTextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly BottomStatusTextProperty As DependencyProperty = DependencyProperty.Register("BottomStatusText", GetType(String), GetType(StartSelectorStateStatusButton), New FrameworkPropertyMetadata("Row 1"))
    Public Property BottomStatusText As String
        Get
            Return Convert.ToString(GetValue(BottomStatusTextProperty))
        End Get
        Set(ByVal value As String)
            SetValue(BottomStatusTextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ButtonBackgroundGridColorProperty As DependencyProperty = DependencyProperty.Register("ButtonBackgroundGridColor", GetType(Brush), GetType(StartSelectorStateStatusButton), New FrameworkPropertyMetadata(Brushes.Red))
    Public Property ButtonBackgroundGridColor As Brush
        Get
            Return CType(GetValue(ButtonBackgroundGridColorProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(ButtonBackgroundGridColorProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StatusLabelBackgroundGridColorProperty As DependencyProperty = DependencyProperty.Register("StatusLabelBackgroundGridColor", GetType(Brush), GetType(StartSelectorStateStatusButton), New FrameworkPropertyMetadata(Brushes.Red))
    Public Property StatusLabelBackgroundGridColor As Brush
        Get
            Return CType(GetValue(StatusLabelBackgroundGridColorProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(StatusLabelBackgroundGridColorProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateProperty As DependencyProperty = DependencyProperty.Register("State", GetType(IgnitionState), GetType(StartSelectorStateStatusButton), New FrameworkPropertyMetadata(IgnitionState.Off))
    Public Property State As IgnitionState
        Get
            Return CType(GetValue(StateProperty), IgnitionState)
        End Get
        Set(ByVal value As IgnitionState)
            SetValue(StateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CommandProperty As DependencyProperty = DependencyProperty.Register("Command", GetType(ICommand), GetType(StartSelectorStateStatusButton), New FrameworkPropertyMetadata(Nothing))
    Public Property Command As ICommand
        Get
            Return CType(GetValue(CommandProperty), ICommand)
        End Get
        Set(ByVal value As ICommand)
            SetValue(CommandProperty, value)
        End Set
    End Property

End Class
