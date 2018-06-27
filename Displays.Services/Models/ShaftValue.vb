<DataContract()>
Public Class ShaftValue
    Inherits ParameterValue

    <DataMember()>
    Public Property Number As Integer
    <DataMember()>
    Public Property CorrectedSpeed As Double
    <DataMember()>
    Public Property CorrectedSpeedRoot20 As Double

    Public Overloads Sub Update(shaft As ShaftValue)
        MyBase.Update(shaft)
        Number = shaft.Number
        CorrectedSpeed = shaft.CorrectedSpeed
        CorrectedSpeedRoot20 = shaft.CorrectedSpeedRoot20
    End Sub

End Class
