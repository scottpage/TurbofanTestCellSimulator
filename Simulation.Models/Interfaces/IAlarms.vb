Public Interface IAlarms

    ReadOnly Property High As Alarm
    ReadOnly Property HighCritical As Alarm
    ReadOnly Property Low As Alarm
    ReadOnly Property LowCritical As Alarm
    ReadOnly Property All() As ObservableCollection(Of Alarm)
    Function GetAlarmingAlarm() As Alarm
    Sub Update(value As Double)

End Interface
