Public Class UnifiedEventLogViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _Entries As New ObservableCollection(Of UnifiedEventLogEntry)
    Private WithEvents _LogServiceHost As UnifiedEventLogServiceHost

#End Region

#Region "Constructors"

    Public Sub New()
        _LogServiceHost = New UnifiedEventLogServiceHost
        _LogServiceHost.StartWaitingForUpdates()
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property Entries As ObservableCollection(Of UnifiedEventLogEntry)
        Get
            Return _Entries
        End Get
    End Property

    Private _SelectedEntry As UnifiedEventLogEntry
    Public Property SelectedEntry As UnifiedEventLogEntry
        Get
            Return _SelectedEntry
        End Get
        Set(ByVal Value As UnifiedEventLogEntry)
            SetProperty(Function() SelectedEntry, _SelectedEntry, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        SelectedEntry = Nothing
        Entries.Clear()
    End Sub

    Public Sub Log(comment As String)
        Log("MGT", comment)
    End Sub

    Public Sub Log(source As String, comment As String)
        Log(Severity.Information, source, comment)
    End Sub

    Public Sub Log(severity As Severity, source As String, comment As String)
        Dim Entry As New UnifiedEventLogEntry(severity, source, comment)
        Entries.Add(Entry)
        SelectedEntry = Entry
    End Sub

    Public Sub Log(severity As Severity, timestamp As Date, source As String, comment As String)
        Dim Entry As New UnifiedEventLogEntry(severity, timestamp, source, comment)
        Entries.Add(Entry)
        SelectedEntry = Entry
    End Sub

    Private Sub LogServiceHost_EntryLogged(sender As Object, e As UnifiedEventEntryLoggedEventArgs) Handles _LogServiceHost.EntryLogged
        Log(e.Severity, e.Source, e.Message)
    End Sub

#End Region

End Class
