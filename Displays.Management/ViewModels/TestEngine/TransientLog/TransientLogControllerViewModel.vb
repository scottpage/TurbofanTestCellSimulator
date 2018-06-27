Public Class TransientLogControllerViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _ExtractId As Integer
    Private _CTLog As New TransientLogViewModel("CTLog", 0)
    Private _DTLog As New TransientLogViewModel("DTLog", 0)
    Private _AvailableLogs As New TransientLogListViewModel
    Private _RunningLogs As New TransientLogListViewModel
    Private _StartLogCommand As ICommand
    Private _StopLogCommand As ICommand
    Private _SaveCriticalSentryLogCommand As ICommand

#End Region

#Region "Constructors"

    Public Sub New()
        AvailableLogs.Logs.Add(_CTLog)
        AvailableLogs.Logs.Add(_DTLog)
        RunningLogs.IsIndexVisible = True
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property AvailableLogs As TransientLogListViewModel
        Get
            Return _AvailableLogs
        End Get
    End Property

    Public ReadOnly Property RunningLogs As TransientLogListViewModel
        Get
            Return _RunningLogs
        End Get
    End Property

    Public ReadOnly Property StartLogCommand As ICommand
        Get
            If _StartLogCommand Is Nothing Then _StartLogCommand = New RelayCommand(AddressOf StartLog, AddressOf CanStartLog)
            Return _StartLogCommand
        End Get
    End Property

    Public ReadOnly Property StopLogCommand As ICommand
        Get
            If _StopLogCommand Is Nothing Then _StopLogCommand = New RelayCommand(AddressOf StopLog, AddressOf CanStopLog)
            Return _StopLogCommand
        End Get
    End Property

    Public ReadOnly Property SaveCriticalSentryLogCommand As ICommand
        Get
            If _SaveCriticalSentryLogCommand Is Nothing Then _SaveCriticalSentryLogCommand = New RelayCommand(AddressOf SaveCriticalSentryLog, AddressOf CanSaveCriticalSentryLog)
            Return _SaveCriticalSentryLogCommand
        End Get
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        StopAllLogs()
    End Sub

    Private Sub IncrementExtractId()
        _ExtractId += 1
    End Sub

    Public Function GetIsCTLogStarted() As Boolean
        Return RunningLogs.Logs.FirstOrDefault(Function(l) l.Name.Equals(_CTLog.Name)) IsNot Nothing
    End Function

    Public Function StartLog(log As TransientLogViewModel) As TransientLogViewModel
        Dim NewLog As New TransientLogViewModel(log.Name, log.Index)
        log.IncrementIndex()
        NewLog.IncrementIndex()
        IncrementExtractId()
        RunningLogs.Logs.Add(NewLog)
        AddLogStartedTestDiaryComment(log)
        Return NewLog
    End Function

    Private Sub StartSelectedLog()
        StartLog(AvailableLogs.SelectedLog)
    End Sub

    Public Function StartCTLog() As TransientLogViewModel
        Return StartLog(_CTLog)
    End Function

    Public Function StartDTLog() As TransientLogViewModel
        Return StartLog(_DTLog)
    End Function

    Public Sub StopLog(log As TransientLogViewModel)
        If RunningLogs.Logs.Remove(log) Then AddLogStoppedTestDiaryComment(log)
    End Sub

    Private Sub StopSelectedLog()
        StopLog(RunningLogs.SelectedLog)
    End Sub

    Public Sub StopCTLog()
        StopLog(_CTLog)
    End Sub

    Public Sub StopAllLogs()
        Dim Logs As New List(Of TransientLogViewModel)
        For Each L In RunningLogs.Logs
            Logs.Add(L)
        Next
        Logs.ForEach(Sub(l) StopLog(l))
    End Sub

    Private Sub AddLogStartedTestDiaryComment(log As TransientLogViewModel)
        Dim TestDiaryComment = String.Concat("START TRANSIENT LOG ", log.DisplayName, " EXTRACT ID: ", _ExtractId)
        MainViewModel.Instance.TestEngine.TestDiary.Add(TestDiaryComment)
        MainViewModel.Instance.UnifiedEventLog.Log(TestDiaryComment)
    End Sub

    Private Sub AddLogStoppedTestDiaryComment(log As TransientLogViewModel)
        Dim TestDiaryComment = String.Concat("STOP TRANSIENT LOG ", log.DisplayName)
        MainViewModel.Instance.TestEngine.TestDiary.Add(TestDiaryComment)
        MainViewModel.Instance.UnifiedEventLog.Log(TestDiaryComment)
    End Sub

#End Region

#Region "Command Handlers"

    Private Function CanStartLog(obj As Object) As Boolean
        Return AvailableLogs.SelectedLog IsNot Nothing And RunningLogs.Logs.Count < 5
    End Function

    Private Sub StartLog(obj As Object)
        StartSelectedLog()
    End Sub

    Private Function CanStopLog(obj As Object) As Boolean
        Return RunningLogs.SelectedLog IsNot Nothing
    End Function

    Private Sub StopLog(obj As Object)
        StopSelectedLog()
    End Sub

    Private Function CanSaveCriticalSentryLog(obj As Object) As Boolean
        Return True
    End Function

    Private Sub SaveCriticalSentryLog(obj As Object)
        Dim TestDiaryComment = "Critical/Sentry log saved"
        MainViewModel.Instance.TestEngine.TestDiary.Add(TestDiaryComment)
        MainViewModel.Instance.UnifiedEventLog.Log(TestDiaryComment)
    End Sub

#End Region

End Class
