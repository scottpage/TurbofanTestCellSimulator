<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)>
Public Class UnifiedEventLogService
    Implements IUnifiedEventLogService

    Public Event EntryLogged(sender As Object, e As UnifiedEventEntryLoggedEventArgs)

    Public Sub LogEntry(severity As Severity, source As String, message As String) Implements IUnifiedEventLogService.LogEntry
        RaiseEvent EntryLogged(Me, New UnifiedEventEntryLoggedEventArgs(severity, source, message))
    End Sub

End Class
