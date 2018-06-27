Imports System.Runtime.CompilerServices

Public Module Extensions

    <Extension()> _
    Public Function StandardDeviation(values As IEnumerable(Of Double)) As Double
        Dim Count = values.Count
        Dim Result = 0.0
        If Count > 1 Then
            Dim Mean = values.Average
            Dim Sum = values.Sum(Function(X) (X - Mean) * (X - Mean))
            Result = (Sum / Count) ^ 0.5
        End If
        Return Result
    End Function

    <Extension()> _
    Public Function IsInRange(value As Double, minimum As Double, maximum As Double) As Boolean
        Return value >= minimum And value <= maximum
    End Function

    <Extension()> _
    Public Function IsStable(values As IEnumerable(Of Double), minimum As Double, maximum As Double, maximumStandardDeviation As Double) As Boolean
        Dim Mean = values.Average
        Dim IsInRange = Mean.IsInRange(minimum, maximum)
        Return values.StandardDeviation <= maximumStandardDeviation And IsInRange
    End Function

    <Extension()> _
    Public Function Slope(data As IEnumerable(Of DataPoint)) As Double
        Dim XAverage = data.Average(Function(d) d.X)
        Dim YAverage = data.Average(Function(d) d.Y)
        Return data.Sum(Function(d) (d.X - XAverage) * (d.Y - YAverage)) / data.Sum(Function(d) Math.Pow(d.X - XAverage, 2))
    End Function

    <Extension()> _
    Public Function Intercept(data As IEnumerable(Of DataPoint)) As Double
        Return data.Average(Function(d) d.Y) - data.Slope * data.Average(Function(d) d.X)
    End Function

    <Extension()> _
    Public Function Intercept(data As IEnumerable(Of DataPoint), slope As Double) As Double
        Return data.Average(Function(d) d.Y) - slope * data.Average(Function(d) d.X)
    End Function

End Module
