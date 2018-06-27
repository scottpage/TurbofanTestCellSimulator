<DataContract()>
Public Class Range
    Implements IRange

    Public Sub New(minimum As Double, maximum As Double)
        _Minimum = minimum
        _Maximum = maximum
    End Sub

    <DataMember()>
    Public Property Maximum As Double Implements IRange.Maximum
    <DataMember()>
    Public Property Minimum As Double Implements IRange.Minimum

End Class
