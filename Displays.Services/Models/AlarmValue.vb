<DataContract()>
Public Class AlarmValue

    Public Sub New(parameterName As String, type As AlarmType, limit As Double)
        _ParameterName = parameterName
        _Type = type
        _Limit = limit
    End Sub

    <DataMember()>
    Public Property ParameterName As String
    <DataMember()>
    Public Property Type As AlarmType
    <DataMember()>
    Public Property Limit As Double
    <DataMember()>
    Public Property MinimumMaximum As Double
    <DataMember()>
    Public Property CurrentValue As Double
    <DataMember()>
    Public Property IsAcknowledged As Boolean

    Public Sub Update(alarm As AlarmValue)
        _Type = alarm.Type
        _Limit = alarm.Limit
        _CurrentValue = alarm.CurrentValue
        _MinimumMaximum = alarm.MinimumMaximum
        _IsAcknowledged = alarm.IsAcknowledged
    End Sub

End Class
