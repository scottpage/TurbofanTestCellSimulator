<DataContract(), KnownType(GetType(TrentXWBParameters))>
Public MustInherit Class TrentParameters
    Inherits SimulationParameters

    <DataMember()>
    Public Property Shaft3 As New ShaftValue
    <DataMember()>
    Public Property Shaft3LaneAVibration As New ParameterValue
    <DataMember()>
    Public Property Shaft3LaneBVibration As New ParameterValue

    Public Overloads Sub Update(values As TrentParameters)
        MyBase.Update(values)
        Dim TrentValues = DirectCast(values, TrentParameters)
        Shaft3.Update(values.Shaft3)
        Shaft3LaneAVibration.Update(values.Shaft3LaneAVibration)
        Shaft3LaneBVibration.Update(values.Shaft3LaneBVibration)
    End Sub

End Class
