Public Class AlarmSizeConverter
    Implements IMultiValueConverter

    Public Function Convert(values() As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
        Dim Value As Double = Double.NaN
        Dim Minimum As Double = Double.NaN
        Dim Maximum As Double = Double.NaN
        Dim AvailableDimension As Double = Double.NaN
        Dim Scale As Double = 0
        If Double.TryParse(values(0).ToString, Value) Or _
            Double.TryParse(values(1).ToString, Minimum) Or _
            Double.TryParse(values(2).ToString, Maximum) Or _
            Double.TryParse(values(3).ToString, AvailableDimension) Then
            If Value < Minimum Then Return 0.0001
            If Value > Maximum Then Return 0.0001
            Scale = AvailableDimension * (Value - Minimum) / (Maximum - Minimum)
            If parameter IsNot Nothing AndAlso Not parameter.Equals(DependencyProperty.UnsetValue) Then
                Dim Invert As Boolean = False
                Boolean.TryParse(parameter.ToString, Invert)
                If Invert Then Return AvailableDimension - Scale
            End If
        End If
        If Scale <= 0 Then Return DependencyProperty.UnsetValue
        Return Scale
    End Function

    Public Function ConvertBack(value As Object, targetTypes() As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
        Throw New NotSupportedException
    End Function

End Class
