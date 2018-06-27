Public Interface IAlarm
    Inherits INotifyConfigurationChanged

    ReadOnly Property Type As AlarmType
    Property Limit As Double
    ReadOnly Property MinimumMaximum As Double
    ReadOnly Property IsAlarming As Boolean
    ReadOnly Property IsAcknowledged As Boolean
    Sub Update(value As Double)
    Sub Acknowledge()

End Interface
