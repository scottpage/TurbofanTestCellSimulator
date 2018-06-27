<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)>
Public Class SimulationStateService
    Implements ISimulationStateService

    Public Event StateUpdated(sender As Object, e As SimulationStateUpdatedEventArgs)

    Public Sub Update(state As SimulationState) Implements ISimulationStateService.Update
        RaiseEvent StateUpdated(Me, New SimulationStateUpdatedEventArgs(state))
    End Sub

End Class
