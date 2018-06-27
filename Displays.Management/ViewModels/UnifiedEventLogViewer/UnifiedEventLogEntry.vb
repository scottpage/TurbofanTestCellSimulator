Public Class UnifiedEventLogEntry
    Inherits ViewModelBase

    Public Sub New(source As String, message As String)
        Me.New(Severity.Information, source, message)
    End Sub

    Public Sub New(severity As Severity, source As String, message As String)
        Me.New(severity, DateTime.UtcNow, source, message)
    End Sub

    Public Sub New(severity As Severity, timestamp As Date, source As String, comment As String)
        _Severity = severity
        _Timestamp = timestamp
        _Source = source
        _Message = comment
    End Sub

#Region "Properties"

    Private _Timestamp As DateTime
    Public Property Timestamp As DateTime
        Get
            Return _Timestamp
        End Get
        Set(ByVal Value As DateTime)
            SetProperty(Function() Timestamp, _Timestamp, Value)
        End Set
    End Property

    Private _Severity As Severity
    Public Property Severity As Severity
        Get
            Return _Severity
        End Get
        Set(ByVal Value As Severity)
            SetProperty(Function() Severity, _Severity, Value)
        End Set
    End Property

    Private _Source As String
    Public Property Source As String
        Get
            Return _Source
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() Source, _Source, Value)
        End Set
    End Property

    Private _Message As String
    Public Property Message As String
        Get
            Return _Message
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() Message, _Message, Value)
        End Set
    End Property

#End Region

End Class
