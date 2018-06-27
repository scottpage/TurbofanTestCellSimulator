<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)>
Public Class ControlService
    Implements IControlService

    Public Event SimulationReset As EventHandler
    Public Event ShuttingDown As EventHandler

    Public Sub Reset() Implements IControlService.Reset
        RaiseEvent SimulationReset(Me, EventArgs.Empty)
    End Sub

    Public Sub Shutdown() Implements IControlService.Shutdown
        RaiseEvent ShuttingDown(Me, EventArgs.Empty)
    End Sub

End Class
