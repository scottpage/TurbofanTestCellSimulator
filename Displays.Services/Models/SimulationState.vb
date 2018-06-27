<DataContract()>
Public Class SimulationState

    <DataMember()>
    Public Property ConfigurationVersion As Integer = Integer.MinValue
    <DataMember()>
    Public Property EngineMode As EngineMode = EngineMode.None
    <DataMember()>
    Public Property Parameters As New SimulationParameters
    <DataMember()>
    Public Property Alarms As IEnumerable(Of AlarmValue)
    <DataMember()>
    Public Property AirInletTemperature As Double
    <DataMember()>
    Public Property EnginePLC As New EnginePLCState
    <DataMember()>
    Public Property EngineType As String
    <DataMember()>
    Public Property EngineSerialNumber As String
    <DataMember()>
    Public Property EngineBuild As String
    <DataMember()>
    Public Property EnginePartTest As String
    <DataMember()>
    Public Property IsFullsetRunning As Boolean

End Class
