Public Class VerticalParameterGauge

#Region "Properties"

    Public Shared ReadOnly IsDisplayCurrentValueProperty As DependencyProperty = DependencyProperty.Register("IsDisplayCurrentValue", GetType(Boolean), GetType(VerticalParameterGauge), New FrameworkPropertyMetadata(False))
    <Category("Gauge")> _
    Public Property IsDisplayCurrentValue As Boolean
        Get
            Return Convert.ToBoolean(GetValue(IsDisplayCurrentValueProperty))
        End Get
        Set(ByVal value As Boolean)
            SetValue(IsDisplayCurrentValueProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IsGaugeVisibleProperty As DependencyProperty = DependencyProperty.Register("IsGaugeVisible", GetType(Boolean), GetType(VerticalParameterGauge), New FrameworkPropertyMetadata(True, New PropertyChangedCallback(AddressOf Property_Changed)))
    <Category("Gauge")> _
    Public Property IsGaugeVisible As Boolean
        Get
            Return Convert.ToBoolean(GetValue(IsGaugeVisibleProperty))
        End Get
        Set(ByVal value As Boolean)
            SetValue(IsGaugeVisibleProperty, value)
        End Set
    End Property

    Public Shared ReadOnly DecimalPlacesProperty As DependencyProperty = DependencyProperty.Register("DecimalPlaces", GetType(Integer), GetType(VerticalParameterGauge), New FrameworkPropertyMetadata(2, New PropertyChangedCallback(AddressOf Property_Changed)))
    <Category("Gauge")> _
    Public Property DecimalPlaces As Integer
        Get
            Return Convert.ToInt32(GetValue(DecimalPlacesProperty))
        End Get
        Set(ByVal value As Integer)
            SetValue(DecimalPlacesProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NumberOfMinorScaleLinesProperty As DependencyProperty = DependencyProperty.Register("NumberOfMinorScaleLines", GetType(Integer), GetType(VerticalParameterGauge), New FrameworkPropertyMetadata(25, New PropertyChangedCallback(AddressOf Property_Changed)))
    <Category("Gauge")> _
    Public Property NumberOfMinorScaleLines As Integer
        Get
            Return Convert.ToInt32(GetValue(NumberOfMinorScaleLinesProperty).ToString)
        End Get
        Set(value As Integer)
            SetValue(NumberOfMinorScaleLinesProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NumberOfMajorScaleLinesProperty As DependencyProperty = DependencyProperty.Register("NumberOfMajorScaleLines", GetType(Integer), GetType(VerticalParameterGauge), New FrameworkPropertyMetadata(5, New PropertyChangedCallback(AddressOf Property_Changed)))
    <Category("Gauge")> _
    Public Property NumberOfMajorScaleLines As Integer
        Get
            Return Convert.ToInt32(GetValue(NumberOfMajorScaleLinesProperty).ToString)
        End Get
        Set(value As Integer)
            SetValue(NumberOfMajorScaleLinesProperty, value)
        End Set
    End Property

#End Region

    Public Shared Sub Property_Changed(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        If TypeOf d Is VerticalParameterGauge Then
            Dim C = DirectCast(d, VerticalParameterGauge)
            Select Case e.Property.Name
                Case IsGaugeVisibleProperty.Name
                    If Convert.ToBoolean(e.NewValue) Then
                        If Not C.MainGrid.Children.Contains(C.LinearGauge) Then C.MainGrid.Children.Add(C.LinearGauge)
                    Else
                        C.MainGrid.Children.Remove(C.LinearGauge)
                    End If
                Case DecimalPlacesProperty.Name
                    'C.ValueLabel.Text = String.Format(C.Value.ToString("F" & C.DecimalPlaces.ToString))
            End Select
        End If
    End Sub

End Class
