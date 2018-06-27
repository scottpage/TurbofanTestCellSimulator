<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)>
Public Class AlarmService
    Inherits ServiceBase
    Implements IAlarmService

    Public Sub New()
        MyBase.New(Nothing)
    End Sub

    Public Sub New(simulator As Simulator)
        MyBase.New(simulator)
    End Sub

    Public Sub Acknowledge(parameterName As String) Implements IAlarmService.Acknowledge
        Dim Parameter = Simulator.Engine.Parameters.First(Function(p) p.Name.Equals(parameterName))
        Dim Alarm = Parameter.Alarms.GetAlarmingAlarm
        If Alarm IsNot Nothing Then Alarm.Acknowledge()
    End Sub

End Class
