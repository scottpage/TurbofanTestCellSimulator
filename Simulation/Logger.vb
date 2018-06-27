Public Module Logger

    Public Const Source As String = "RTE"

    Public Sub Log(message As String)
        Log(Severity.Information, Source, message)
    End Sub

    Public Sub Log(message As String, ParamArray args() As Object)
        Log(String.Format(message, args))
    End Sub

    Public Sub Log(source As String, message As String, ParamArray args() As Object)
        Log(Severity.Information, source, String.Format(message, args))
    End Sub

    Public Sub Log(severity As Severity, source As String, message As String, ParamArray args() As Object)
        Log(severity, source, String.Format(message, args))
    End Sub

    Public Sub Log(severity As Severity, source As String, message As String)
        Dim C As New UnifiedEventLogServiceClient
        C.LogEntryAsync(severity, source, message)
        C.Close()
        C = Nothing
    End Sub

End Module
