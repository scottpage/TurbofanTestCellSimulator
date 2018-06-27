Public Class TemperatureToColorConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If Not TypeOf value Is Double Then Return DependencyProperty.UnsetValue
        Dim ColorPercent = CInt(CDbl(value) * 0.231818182)
        If ColorPercent > Byte.MaxValue Then ColorPercent = Byte.MaxValue
        If ColorPercent < Byte.MinValue Then ColorPercent = Byte.MinValue
        Return New SolidColorBrush(Color.FromArgb(200, CByte(ColorPercent), 0, 0))
    End Function

    Public Function ConvertBack(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Throw New NotSupportedException
    End Function

End Class
