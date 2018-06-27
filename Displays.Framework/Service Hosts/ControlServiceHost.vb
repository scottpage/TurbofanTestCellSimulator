Public Class ControlServiceHost

    Public Event SimulationReset As EventHandler

    Private WithEvents _service As ControlService
    Private _Host As ServiceHost

    Public Sub StartWaitingForUpdates()
        _Service = New ControlService
        _Host = New ServiceHost(_Service)
        _Host.BeginOpen(Nothing, Nothing)
    End Sub

    Public Sub [Stop]()
        If _Host IsNot Nothing AndAlso Not _Host.State = CommunicationState.Faulted Then
            _Host.Close()
        End If
        _Host = Nothing
        _Service = Nothing
    End Sub

    Private Sub Service_SimulationReset(sender As Object, e As EventArgs) Handles _Service.SimulationReset
        RaiseEvent SimulationReset(Me, e)
    End Sub

    Private Sub Service_ShuttingDown(sender As Object, e As EventArgs) Handles _Service.ShuttingDown
        'RaiseEvent ShuttingDown(Me, e)
        'Force shutdown for now until proof exists that this doesn't work.
        Application.Current.Shutdown()
    End Sub

End Class
