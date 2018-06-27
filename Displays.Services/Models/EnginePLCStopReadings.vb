<DataContract()>
Public Class EnginePLCStopReadings

    <DataMember()>
    Public Property NoHPRotation() As Double
    <DataMember()>
    Public Property NoIPRotation() As Double
    <DataMember()>
    Public Property NoLPRotation() As Double
    <DataMember()>
    Public Property EngineOilContent() As Double
    <DataMember()>
    Public Property EngineOilTemperature() As Double
    <DataMember()>
    Public Property MaxVibrationDuringRundown() As Double

End Class
