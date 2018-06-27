Imports ScottPage.RollsRoyce.TestCellSimulator.Simulation.Common.ViewModels

<DataContract()>
Public Class Alarms
    Inherits ViewModelBase
    Implements IAlarms

    Public Sub New()
        _All = New ObservableCollection(Of Alarm)(New Alarm() {High, HighCritical, Low, LowCritical})
    End Sub

    Private _High As New Alarm(AlarmType.High, 999999)
    <DataMember()>
    Public ReadOnly Property High As Alarm Implements IAlarms.High
        Get
            Return _High
        End Get
    End Property

    Private _HighCritical As New Alarm(AlarmType.HighCritical, 999999)
    <DataMember()>
    Public ReadOnly Property HighCritical As Alarm Implements IAlarms.HighCritical
        Get
            Return _HighCritical
        End Get
    End Property

    Private _Low As New Alarm(AlarmType.Low, -999999)
    <DataMember()>
    Public ReadOnly Property Low As Alarm Implements IAlarms.Low
        Get
            Return _Low
        End Get
    End Property

    Private _LowCritical As New Alarm(AlarmType.LowCritical, -999999)
    <DataMember()>
    Public ReadOnly Property LowCritical As Alarm Implements IAlarms.LowCritical
        Get
            Return _LowCritical
        End Get
    End Property

    Private _All As ObservableCollection(Of Alarm)
    Public ReadOnly Property All() As ObservableCollection(Of Alarm) Implements IAlarms.All
        Get
            Return _All
        End Get
    End Property

    Public Function GetAlarmingAlarm() As Alarm Implements IAlarms.GetAlarmingAlarm
        If High.IsAlarming And Not HighCritical.IsAlarming Then
            Return High
        ElseIf High.IsAlarming And HighCritical.IsAlarming Then
            Return HighCritical
        ElseIf Low.IsAlarming And Not LowCritical.IsAlarming Then
            Return Low
        ElseIf Low.IsAlarming And LowCritical.IsAlarming Then
            Return LowCritical
        End If
        Return Nothing
    End Function

    Public Sub Update(value As Double) Implements IAlarms.Update
        High.Update(value)
        HighCritical.Update(value)
        Low.Update(value)
        LowCritical.Update(value)
    End Sub

End Class
