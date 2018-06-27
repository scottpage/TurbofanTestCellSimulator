<DataContract()>
Public Class ElectronicEngineController
    Implements IElectronicEngineController

    <DataMember()>
    Public Property IsInFlightMode As Boolean Implements IElectronicEngineController.IsInFlightMode
    <DataMember()>
    Public Property IsInHighIdleMode As Boolean Implements IElectronicEngineController.IsInHighIdleMode
    <DataMember()>
    Public Property IsPowerOn As Boolean Implements IElectronicEngineController.IsPowerOn
    <DataMember()>
    Public Property IsMasterCautionActive As Boolean Implements IElectronicEngineController.IsMasterCautionActive
    <DataMember()>
    Public Property IsMasterWarningActive As Boolean Implements IElectronicEngineController.IsMasterWarningActive

End Class
