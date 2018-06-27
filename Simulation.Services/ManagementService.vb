<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)>
Public Class ManagementService
    Inherits ServiceBase
    Implements IManagementService

    Public Sub New()
        MyBase.New(Nothing)
    End Sub

    Public Sub New(simulator As Simulator)
        MyBase.New(simulator)
    End Sub

    Public Sub StartFullset(comment As String) Implements IManagementService.StartFullset
        Simulator.State.IsFullsetRunning = True
    End Sub

    Public Sub StopFullsetAsync() Implements IManagementService.StopFullsetAsync
        Simulator.State.IsFullsetRunning = False
    End Sub

End Class
