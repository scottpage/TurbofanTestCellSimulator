<DataContract()>
Public Class EngineMonitoringUnit
    Implements IEngineMonitoringUnit

    <DataMember()>
    Public Property IsPowerOn As Boolean Implements IEngineMonitoringUnit.IsPowerOn

End Class
