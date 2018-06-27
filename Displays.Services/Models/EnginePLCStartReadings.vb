<DataContract()>
Public Class EnginePLCStartReadings

    <DataMember()>
    Public Property HPRotation() As Double
    <DataMember()>
    Public Property IPRotation() As Double
    <DataMember()>
    Public Property LPRotation() As Double
    <DataMember()>
    Public Property PrestartTGT() As Double
    <DataMember()>
    Public Property PrestartStartAirPressure() As Double
    <DataMember()>
    Public Property StartingStartAirPressure() As Double
    <DataMember()>
    Public Property N3DeadCrankSpeed() As Double
    <DataMember()>
    Public Property N3DeadCrankSeconds() As Double
    <DataMember()>
    Public Property N2DeadCrankSpeed() As Double
    <DataMember()>
    Public Property N2DeadCrankSeconds() As Double
    <DataMember()>
    Public Property FuelOn() As Double
    <DataMember()>
    Public Property FuelOnSeconds() As Double
    <DataMember()>
    Public Property Lit() As Double
    <DataMember()>
    Public Property LitSeconds() As Double
    <DataMember()>
    Public Property LightupFuelFlow() As Double
    <DataMember()>
    Public Property StarterCut() As Double
    <DataMember()>
    Public Property StarterCutSeconds() As Double
    <DataMember()>
    Public Property MaxTGT() As Double
    <DataMember()>
    Public Property TimeToGI() As Double
    <DataMember()>
    Public Property VibrationAtIdle() As Double
    <DataMember()>
    Public Property OilPressureAtIdle() As Double
    <DataMember()>
    Public Property AirIntakeTemperatureAtIdle() As Double

End Class
