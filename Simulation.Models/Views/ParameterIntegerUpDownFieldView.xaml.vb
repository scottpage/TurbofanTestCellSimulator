Public Class ParameterIntegerUpDownFieldView

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
                           GetType(String), GetType(ParameterIntegerUpDownFieldView), _
                           New FrameworkPropertyMetadata(String.Empty))

    Public Property Value As Integer
        Get
            Return CInt(GetValue(ValueProperty))
        End Get

        Set(ByVal value As Integer)
            SetValue(ValueProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ValueProperty As DependencyProperty = _
                           DependencyProperty.Register("Value", _
                           GetType(Integer), GetType(ParameterIntegerUpDownFieldView), _
                           New FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault))

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
                           GetType(Double), GetType(ParameterIntegerUpDownFieldView), _
                           New FrameworkPropertyMetadata(Nothing))

    Public Property Minimum As Integer
        Get
            Return CInt(GetValue(MinimumProperty))
        End Get

        Set(ByVal value As Integer)
            SetValue(MinimumProperty, value)
        End Set
    End Property

    Public Shared ReadOnly MinimumProperty As DependencyProperty = _
                           DependencyProperty.Register("Minimum", _
                           GetType(Integer), GetType(ParameterIntegerUpDownFieldView), _
                           New FrameworkPropertyMetadata(-999999))

    Public Property Maximum As Integer
        Get
            Return CInt(GetValue(MaximumProperty))
        End Get

        Set(ByVal value As Integer)
            SetValue(MaximumProperty, value)
        End Set
    End Property

    Public Shared ReadOnly MaximumProperty As DependencyProperty = _
                           DependencyProperty.Register("Maximum", _
                           GetType(Integer), GetType(ParameterIntegerUpDownFieldView), _
                           New FrameworkPropertyMetadata(999999))

End Class
