Public Class VerticalLinearGauge

    Public Shared ReadOnly IsDisplayCurrentValueProperty As DependencyProperty = DependencyProperty.Register("IsDisplayCurrentValue", GetType(Boolean), GetType(VerticalLinearGauge), New FrameworkPropertyMetadata(False))
    <Category("Gauge")> _
    Public Property IsDisplayCurrentValue As Boolean
        Get
            Return Convert.ToBoolean(GetValue(IsDisplayCurrentValueProperty))
        End Get
        Set(ByVal value As Boolean)
            SetValue(IsDisplayCurrentValueProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NumberOfMinorGridLinesProperty As DependencyProperty = DependencyProperty.Register("NumberOfMinorGridLines", GetType(Integer), GetType(VerticalLinearGauge), New FrameworkPropertyMetadata(50, FrameworkPropertyMetadataOptions.AffectsRender Or FrameworkPropertyMetadataOptions.AffectsParentMeasure))
    <Category("Common")> _
    Public Property NumberOfMinorGridLines As Integer
        Get
            Return CType(GetValue(NumberOfMinorGridLinesProperty), Integer)
        End Get
        Set(value As Integer)
            SetValue(NumberOfMinorGridLinesProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NumberOfMajorGridLinesProperty As DependencyProperty = DependencyProperty.Register("NumberOfMajorGridLines", GetType(Integer), GetType(VerticalLinearGauge), New FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.AffectsRender Or FrameworkPropertyMetadataOptions.AffectsParentMeasure))
    ''' <summary>
    ''' Gets or Sets the number of major lines along the dependent axis.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Common")> _
    Public Property NumberOfMajorGridLines As Integer
        Get
            Return CType(GetValue(NumberOfMajorGridLinesProperty), Integer)
        End Get
        Set(value As Integer)
            SetValue(NumberOfMajorGridLinesProperty, value)
        End Set
    End Property

End Class
