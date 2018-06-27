Public Class ParameterTextBoxFieldView

    Public Property Text As String
        Get
            Return CStr(GetValue(TextProperty))
        End Get

        Set(ByVal value As String)
            SetValue(TextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TextProperty As DependencyProperty = _
                           DependencyProperty.Register("Text", _
                           GetType(String), GetType(ParameterTextBoxFieldView), _
                           New FrameworkPropertyMetadata(String.Empty))

    Public Property Value As String
        Get
            Return CStr(GetValue(ValueProperty))
        End Get

        Set(ByVal value As String)
            SetValue(ValueProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ValueProperty As DependencyProperty = _
                           DependencyProperty.Register("Value", _
                           GetType(String), GetType(ParameterTextBoxFieldView), _
                           New FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault))




    Public Property TextMinWidth As Double
        Get
            Return CDbl(GetValue(TextMinWidthProperty))
        End Get

        Set(ByVal value As Double)
            SetValue(TextMinWidthProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TextMinWidthProperty As DependencyProperty = _
                           DependencyProperty.Register("TextMinWidth", _
                           GetType(Double), GetType(ParameterTextBoxFieldView), _
                           New FrameworkPropertyMetadata(Nothing))

End Class
