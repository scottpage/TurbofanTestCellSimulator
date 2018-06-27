Public Class SimulationStateUpdateServiceHost

    Public Event StateUpdated(sender As Object, e As SimulationStateUpdatedEventArgs)

    Private WithEvents _Service As SimulationStateService
    Private _Host As ServiceHost

    Public Sub StartWaitingForUpdates()
        _Service = New SimulationStateService
        _Host = New ServiceHost(_Service)
        _Host.BeginOpen(Nothing, Nothing)
    End Sub

    Public Sub StopWaitingForUpdates()
        If _Host IsNot Nothing AndAlso _Host.State = CommunicationState.Opened Then _Host.Close()
        _Host = Nothing
        _Service = Nothing
    End Sub

    Private Sub Service_StateUpdated(sender As Object, e As SimulationStateUpdatedEventArgs) Handles _Service.StateUpdated
        RaiseEvent StateUpdated(Me, e)
    End Sub

End Class
