Public Class LineScaleConverter
    Implements IMultiValueConverter

    Public Function Convert(values() As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
        Dim L As New List(Of Line)
        Dim Minimum As Double = Double.NaN
        Dim Maximum As Double = Double.NaN
        Dim AvailableDimesion As Double = Double.NaN
        Dim IntervalCount As Integer = 0
        If values Is Nothing OrElse values.Count < 4 OrElse values(0) Is DependencyProperty.UnsetValue Or values(1) Is DependencyProperty.UnsetValue Or values(2) Is DependencyProperty.UnsetValue Or values(3) Is DependencyProperty.UnsetValue Then Return L
        If Double.TryParse(values(0).ToString, Minimum) Or _
           Double.TryParse(values(1).ToString, Maximum) Or _
           Double.TryParse(values(2).ToString, AvailableDimesion) Or _
           Integer.TryParse(values(3).ToString, IntervalCount) Then
            If Minimum > Maximum Or IntervalCount < 2 Or AvailableDimesion < 1 Then Return L
            Dim Invert As Boolean = False
            If parameter IsNot Nothing AndAlso Not parameter.Equals(DependencyProperty.UnsetValue) Then
                Boolean.TryParse(parameter.ToString, Invert)
            End If
            For Number = Minimum To Maximum Step (Maximum - Minimum) / IntervalCount
                Dim Line As New Line
                Line.Stroke = Brushes.White
                Line.StrokeThickness = 2
                If Invert Then
                    'Line.VerticalAlignment = VerticalAlignment.Bottom
                    Line.X2 = AvailableDimesion
                Else
                    Line.Y2 = AvailableDimesion
                End If
                L.Add(Line)
            Next
        End If
        Return L
    End Function

    Public Function ConvertBack(value As Object, targetTypes() As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
        Throw New NotSupportedException
    End Function

End Class
