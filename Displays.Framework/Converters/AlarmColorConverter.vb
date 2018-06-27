Public Class AlarmColorConverter
    Implements IMultiValueConverter

    Public Function Convert(values() As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
        Dim Value As Double = Double.NaN
        Dim LowCriticalAlarm As Double = Double.NaN
        Dim LowWarningAlarm As Double = Double.NaN
        Dim HighWarningAlarm As Double = Double.NaN
        Dim HighCriticalAlarm As Double = Double.NaN
        Dim C = Brushes.Lime
        If Double.TryParse(values(0).ToString, Value) Or _
            Double.TryParse(values(1).ToString, LowCriticalAlarm) Or _
            Double.TryParse(values(2).ToString, LowWarningAlarm) Or _
            Double.TryParse(values(3).ToString, HighWarningAlarm) Or _
            Double.TryParse(values(4).ToString, HighCriticalAlarm) Then
            If Value.Equals(NumberToFormattedStringConverter.BadValue) Then Return Brushes.Red
            If Value <= LowCriticalAlarm Then
                C = Brushes.Red
            ElseIf Value > LowCriticalAlarm And Value <= LowWarningAlarm Then
                C = Brushes.Yellow
            ElseIf Value >= HighCriticalAlarm Then
                C = Brushes.Red
            ElseIf Value < HighCriticalAlarm And Value >= HighWarningAlarm Then
                C = Brushes.Yellow
            End If
        End If
        Return C
    End Function

    Public Function ConvertBack(value As Object, targetTypes() As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
        Throw New NotSupportedException
    End Function

End Class
