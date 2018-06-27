Public Class BooleanToColorConverter
    Implements IValueConverter

    Public Property TrueBrush As Brush = Brushes.Lime
    Public Property FalseBrush As Brush = Brushes.Red

    Public Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If Not TypeOf value Is Boolean Then Return DependencyProperty.UnsetValue
        If Boolean.Parse(value.ToString) Then Return TrueBrush
        Return FalseBrush
    End Function

    Public Function ConvertBack(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        If Not TypeOf value Is Brush Then Return DependencyProperty.UnsetValue
        Dim Brush = DirectCast(value, Brush)
        If Brush.Equals(TrueBrush) Then Return True
        If Brush.Equals(FalseBrush) Then Return False
        Return DependencyProperty.UnsetValue
    End Function

End Class
