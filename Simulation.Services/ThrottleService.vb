<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)>
Public Class ThrottleService
    Inherits ServiceBase
    Implements IThrottleService

    Public Sub New()
        MyBase.New(Nothing)
    End Sub

    Public Sub New(simulator As Simulator)
        MyBase.New(simulator)
    End Sub

    Public Function GetLeverAngle() As Double Implements IThrottleService.GetLeverAngle
        Return Simulator.Engine.Throttle.LeverAngle.Value.Current
    End Function

    Public Function GetRange() As Range Implements IThrottleService.GetRange
        Return New Range(Simulator.Engine.Throttle.LeverAngle.Value.Minimum, _
                         Simulator.Engine.Throttle.LeverAngle.Value.Maximum)
    End Function

    Public Sub UpdateLeverAngle(value As Double) Implements IThrottleService.UpdateLeverAngle
        Simulator.Engine.Throttle.LeverAngle.Value.Update(value)
    End Sub

End Class
