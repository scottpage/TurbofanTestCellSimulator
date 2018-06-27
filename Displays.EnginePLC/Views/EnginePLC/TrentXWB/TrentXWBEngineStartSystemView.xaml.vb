Public Class TrentXWBEngineStartSystemView

    Public Shared ReadOnly StartSelectorStateProperty As DependencyProperty = _
                           DependencyProperty.Register("StartSelectorState", _
                           GetType(StartSelectorState), GetType(TrentXWBEngineStartSystemView), _
                           New FrameworkPropertyMetadata(StartSelectorState.Normal))

    <Category("Common")> _
    Public Property StartSelectorState As StartSelectorState
        Get
            Return DirectCast(GetValue(StartSelectorStateProperty), StartSelectorState)
        End Get

        Set(ByVal value As StartSelectorState)
            SetValue(StartSelectorStateProperty, value)
        End Set
    End Property

End Class
