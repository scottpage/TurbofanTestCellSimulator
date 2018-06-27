<ServiceContract(SessionMode:=SessionMode.NotAllowed)>
Public Interface IEnginePLCService

    <OperationContract(IsOneWay:=True)>
    Sub UpdateFuelOn(isOn As Boolean)
    <OperationContract(IsOneWay:=True)>
    Sub UpdateManualStart(isOn As Boolean)
    <OperationContract(IsOneWay:=True)>
    Sub UpdateStartSelectorCrank()
    <OperationContract(IsOneWay:=True)>
    Sub UpdateStartSelectorNormal()
    <OperationContract(IsOneWay:=True)>
    Sub UpdateStartSelectorIgnitionStart()
    <OperationContract(IsOneWay:=True)>
    Sub UpdateStartMasterOn(isOn As Boolean)
    <OperationContract(IsOneWay:=True)>
    Sub UpdateIgnitionOn()
    <OperationContract(IsOneWay:=True)>
    Sub UpdateIgnitionArmed()
    <OperationContract(IsOneWay:=True)>
    Sub UpdateIgnitionOff()
    <OperationContract(IsOneWay:=True)>
    Sub UpdateGroundPowerOn(isOn As Boolean)
    <OperationContract(IsOneWay:=True)>
    Sub UpdateEMUPowerOn(isOn As Boolean)
    <OperationContract(IsOneWay:=True)>
    Sub UpdateIsInFlight(isInFlight As Boolean)
    <OperationContract(IsOneWay:=True)>
    Sub UpdateHighIdle(isHighIdle As Boolean)
    <OperationContract(IsOneWay:=True)>
    Sub UpdateHydraulicPump1Fitted(isFitted As Boolean)
    <OperationContract(IsOneWay:=True)>
    Sub UpdateHydraulicPump2Fitted(isFitted As Boolean)
    <OperationContract(IsOneWay:=True)>
    Sub ClearStartReadings()
    <OperationContract(IsOneWay:=True)>
    Sub ClearStopReadings()

End Interface
