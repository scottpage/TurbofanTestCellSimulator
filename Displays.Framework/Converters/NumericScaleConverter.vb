Public Class NumericScaleConverter
    Implements IMultiValueConverter

    Public Function Convert(values() As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
        Dim Numbers As New List(Of Double)
        Dim Minimum As Double = Double.NaN
        Dim Maximum As Double = Double.NaN
        Dim IntervalCount As Integer = 0
        Dim Invert As Boolean = False
        If Double.TryParse(values(0).ToString, Minimum) Or _
           Double.TryParse(values(1).ToString, Maximum) Or _
           Integer.TryParse(values(2).ToString, IntervalCount) Then
            For Number = Minimum To Maximum - 0.1 Step (Maximum - Minimum) / IntervalCount
                Numbers.Add(Number)
            Next
            If parameter IsNot Nothing AndAlso Not parameter.Equals(DependencyProperty.UnsetValue) Then
                Boolean.TryParse(parameter.ToString, Invert)
                If Invert Then Numbers.Reverse()
            End If
        End If
        Dim ViewBoxes As New List(Of Viewbox)
        For Each N In Numbers
            Dim VB As New Viewbox
            Dim TB As New TextBlock
            TB.Foreground = Brushes.White
            VB.Child = TB
            VB.Margin = New Thickness(-4, 0, 0, 0)
            VB.HorizontalAlignment = HorizontalAlignment.Left
            TB.Text = N.ToString("#0")
            If Invert Then
                VB.HorizontalAlignment = HorizontalAlignment.Right
                VB.Margin = New Thickness(0, 6, 3, -6)
            End If
            If Numbers.Max < 10 Then
                TB.Text = N.ToString("#0.0")
            End If
            ViewBoxes.Add(VB)
        Next
        Return ViewBoxes
    End Function

    Public Function ConvertBack(value As Object, targetTypes() As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
        Throw New NotSupportedException
    End Function

End Class
