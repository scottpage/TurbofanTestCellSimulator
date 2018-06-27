<DataContract()>
Public Class EnginePLCState

    <DataMember()>
    Public Property IsFuelOn As Boolean = False
    <DataMember()>
    Public Property StartSelectorState As StartSelectorState = Services.StartSelectorState.Normal
    <DataMember()>
    Public Property IsManualStartOn As Boolean = False
    <DataMember()>
    Public Property IsInFlight As Boolean = False
    <DataMember()>
    Public Property IsHighIdleSelected As Boolean = False
    <DataMember()>
    Public Property IsHydraulicPump1NotFitted As Boolean = True
    <DataMember()>
    Public Property IsHydraulicPump2NotFitted As Boolean = True
    <DataMember()>
    Public Property IsHydraulicPump1Detected As Boolean = False
    <DataMember()>
    Public Property IsHydraulicPump2Detected As Boolean = False
    <DataMember()>
    Public Property IsStartMasterOn As Boolean = False
    <DataMember()>
    Public Property IsGroundPowerOn As Boolean = False
    <DataMember()>
    Public Property IsEMUPowerOn As Boolean = False
    <DataMember()>
    Public Property IsStartAirValveOpened As Boolean = False
    <DataMember()>
    Public Property IgnitionState As IgnitionState = Services.IgnitionState.Off
    <DataMember()>
    Public Property IsIgnitor1Commanded As Boolean = False
    <DataMember()>
    Public Property IsIgnitor2Commanded As Boolean = False
    <DataMember()>
    Public Property StartReadings As New EnginePLCStartReadings
    <DataMember()>
    Public Property StopReadings As New EnginePLCStopReadings
    <DataMember()>
    Public Property IsMasterCautionActive As Boolean
    <DataMember()>
    Public Property IsMasterWarningActive As Boolean

End Class
