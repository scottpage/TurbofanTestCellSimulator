Public Class ParameterDoubleUpDownFieldView

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
                           GetType(String), GetType(ParameterDoubleUpDownFieldView), _
                           New FrameworkPropertyMetadata(String.Empty))

    Public Property Value As Double
        Get
            Return CDbl(GetValue(ValueProperty))
        End Get

        Set(ByVal value As Double)
            SetValue(ValueProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ValueProperty As DependencyProperty = _
                           DependencyProperty.Register("Value", _
                           GetType(Double), GetType(ParameterDoubleUpDownFieldView), _
                           New FrameworkPropertyMetadata(0.0#, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault))

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
                           GetType(Double), GetType(ParameterDoubleUpDownFieldView), _
                           New FrameworkPropertyMetadata(Nothing))

    Public Property Minimum As Double
        Get
            Return CDbl(GetValue(MinimumProperty))
        End Get

        Set(ByVal value As Double)
            SetValue(MinimumProperty, value)
        End Set
    End Property

    Public Shared ReadOnly MinimumProperty As DependencyProperty = _
                           DependencyProperty.Register("Minimum", _
                           GetType(Double), GetType(ParameterDoubleUpDownFieldView), _
                           New FrameworkPropertyMetadata(-999999.0#))

    Public Property Maximum As Double
        Get
            Return CDbl(GetValue(MaximumProperty))
        End Get

        Set(ByVal value As Double)
            SetValue(MaximumProperty, value)
        End Set
    End Property

    Public Shared ReadOnly MaximumProperty As DependencyProperty = _
                           DependencyProperty.Register("Maximum", _
                           GetType(Double), GetType(ParameterDoubleUpDownFieldView), _
                           New FrameworkPropertyMetadata(999999.0#))

End Class
