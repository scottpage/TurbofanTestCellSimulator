<DataContract(), XmlInclude(GetType(TrentXWB)), KnownType(GetType(TrentXWB))>
Public MustInherit Class Trent
    Inherits Engine

    Protected Sub New(type As String)
        MyBase.New(type)
    End Sub

    <DataMember()> _
    Public Property Shaft3 As Shaft

    Protected Overrides Function GetAdditionalShafts() As System.Collections.Generic.List(Of Shaft)
        Dim Shafts As New List(Of Shaft)
        Shaft3 = New Shaft(3, "NHPPERC")
        Shaft3.LaneATrackedOrderVibration.Name = "A2298AHP"
        Shaft3.LaneATrackedOrderVibration.EngineeringUnits = "IN/S_PK"
        Shaft3.LaneBTrackedOrderVibration.Name = "A2298BHP"
        Shaft3.LaneBTrackedOrderVibration.EngineeringUnits = "IN/S_PK"
        Shafts.Add(Shaft3)
        Return Shafts
    End Function

    Protected Overrides Sub InitializeConfiguration()
        Shaft2.LaneATrackedOrderVibration.Name = "A2298AIP"
        Shaft2.LaneATrackedOrderVibration.EngineeringUnits = "IN/S_PK"
        Shaft2.LaneBTrackedOrderVibration.Name = "A2298BIP"
        Shaft2.LaneBTrackedOrderVibration.EngineeringUnits = "IN/S_PK"
        MyBase.InitializeConfiguration()
    End Sub

End Class
