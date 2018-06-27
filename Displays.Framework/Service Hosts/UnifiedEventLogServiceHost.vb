Public Class UnifiedEventLogServiceHost

    Public Event EntryLogged(sender As Object, e As UnifiedEventEntryLoggedEventArgs)

    Private WithEvents _Service As UnifiedEventLogService
    Private _Host As ServiceHost

    Public Sub StartWaitingForUpdates()
        _Service = New UnifiedEventLogService
        _Host = New ServiceHost(_Service)
        _Host.BeginOpen(Nothing, Nothing)
    End Sub

    Public Sub StopWaitingForUpdates()
        _Host.Close()
        _Host = Nothing
        _Service = Nothing
    End Sub

    Private Sub Service_EntryLogged(sender As Object, e As UnifiedEventEntryLoggedEventArgs) Handles _Service.EntryLogged
        RaiseEvent EntryLogged(Me, e)
    End Sub

End Class
