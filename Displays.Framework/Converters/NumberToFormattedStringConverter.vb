Public Class NumberToFormattedStringConverter
    Implements IMultiValueConverter

    Public Const BadValue As Double = -99999.0#
    Public Const DefaultNanString As String = "--"

    Public Property DefaultDecimalPlaces As Integer = 2

    Public Function Convert(values() As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
        If values Is Nothing OrElse values.Count < 1 OrElse Not TypeOf values(0) Is Double OrElse Double.IsNaN(Double.Parse(values(0).ToString)) Then Return DefaultNanString
        Dim Value = Double.Parse(values(0).ToString)
        If Value.Equals(BadValue) Then Return DefaultNanString
        Dim DecimalPlaces = DefaultDecimalPlaces
        If values.Count > 1 AndAlso TypeOf values(1) Is Integer Then
            DecimalPlaces = Integer.Parse(values(1).ToString)
        End If
        Return Value.ToString(String.Concat("F", DecimalPlaces))
    End Function

    Public Function ConvertBack(value As Object, targetTypes() As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
        Throw New NotSupportedException
    End Function

End Class
