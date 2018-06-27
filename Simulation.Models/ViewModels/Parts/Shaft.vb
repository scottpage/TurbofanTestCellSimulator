<DataContract()>
Public Class Shaft
    Inherits Parameter
    Implements IShaft

    Public Sub New(number As Integer)
        Alarms = New Alarms
        LaneATrackedOrderVibration = New Parameter
        LaneBTrackedOrderVibration = New Parameter
        Value = New Value
        _Number = number
    End Sub

    Public Sub New(number As Integer, name As String)
        Me.New(number)
        Me.Name = name
        Me.EngineeringUnits = "%"
    End Sub

    <DataMember()>
    Public Property CorrectedSpeed As Double Implements IShaft.CorrectedSpeed
    <DataMember()>
    Public Property CorrectedSpeedRoot20 As Double Implements IShaft.CorrectedSpeedRoot20
    <DataMember()>
    Public Property IsRotating As Boolean Implements IShaft.IsRotating
    <DataMember()>
    Public Property MaximumRPM As Double Implements IShaft.MaximumRPM
    <DataMember()>
    Public Property MinimumRPM As Double Implements IShaft.MinimumRPM
    <DataMember()>
    Public Property Number As Integer Implements IShaft.Number
    <DataMember()>
    Public Property LaneATrackedOrderVibration As Parameter Implements IShaft.LaneATrackedOrderVibration
    <DataMember()>
    Public Property LaneBTrackedOrderVibration As Parameter Implements IShaft.LaneBTrackedOrderVibration

    Public Overrides Sub Update(value As Double)
        MyBase.Update(value)
        LaneATrackedOrderVibration.Update(value)
        LaneBTrackedOrderVibration.Update(value)
    End Sub

    Public Sub UpdateCorrectedSpeeds(airInletTemperature As Double)
        Dim RPM = (Value.Average / 100) * MaximumRPM
        CorrectedSpeed = ((RPM / Math.Sqrt((airInletTemperature + 273.15) / 288.15)) / MaximumRPM) * 100
        CorrectedSpeedRoot20 = RPM / Math.Sqrt(airInletTemperature + 273.15)
    End Sub

End Class
