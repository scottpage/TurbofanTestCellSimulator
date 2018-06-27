Public Class UnifiedEventEntryLoggedEventArgs
    Inherits EventArgs

    Private _Severity As Severity
    Private _Source As String
    Private _Message As String

    Public Sub New(source As String, message As String)
        _Severity = Severity.Information
        _Source = source
        _Message = message
    End Sub

    Public Sub New(severity As Severity, source As String, message As String)
        Me.New(source, message)
        _Severity = severity
    End Sub

    Public ReadOnly Property Severity As Severity
        Get
            Return _Severity
        End Get
    End Property

    Public ReadOnly Property Source As String
        Get
            Return _Source
        End Get
    End Property

    Public ReadOnly Property Message As String
        Get
            Return _Message
        End Get
    End Property

End Class
