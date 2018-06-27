Imports System.Collections.Specialized

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

#Region "Fields"

    Private _EngineStartCTLog As TransientLogViewModel
    Private _LastEngineMode As EngineMode = EngineMode.None

#End Region

#Region "Application"

    Protected Overrides Sub OnStartup(e As System.Windows.StartupEventArgs)
        MyBase.OnStartup(e)

        LoadUELEntries()
        LoadTestDiaryComments()
        AddAvailbleTestEngineComments()

        With My.Windows
            With .TestEngineWindow
                .DataContext = MainViewModel.Instance.TestEngine
            End With
            With .UnifiedEventLogWindow
                .DataContext = MainViewModel.Instance.UnifiedEventLog
                .Width = SystemParameters.FullPrimaryScreenWidth
                .Left = -1280
                .Top = SystemParameters.PrimaryScreenHeight - 300
            End With
            With .STINCAControlWindow
                .DataContext = MainViewModel.Instance.Telemetry.STINCAControl
                .Width = SystemParameters.FullPrimaryScreenWidth
                .Left = -1280
                .Height = SystemParameters.PrimaryScreenHeight - My.Windows.UnifiedEventLogWindow.Height - 2
                .Top = 0
            End With
            With .CHINCAControlWindow
                .DataContext = MainViewModel.Instance.Telemetry.CHINCAControl
                .Width = My.Windows.STINCAControlWindow.Width
                .Left = My.Windows.STINCAControlWindow.Left
                .Height = My.Windows.STINCAControlWindow.Height
                .Top = My.Windows.STINCAControlWindow.Top
            End With
            With .DIGBERTControlWindow
                .DataContext = MainViewModel.Instance.Telemetry.DIGBERTControl
                .Width = My.Windows.STINCAControlWindow.Width
                .Left = My.Windows.STINCAControlWindow.Left
                .Height = My.Windows.STINCAControlWindow.Height
                .Top = My.Windows.STINCAControlWindow.Top
            End With
            ShowWindow(.TestEngineWindow)
            ShowWindow(.UnifiedEventLogWindow)
            ShowWindow(.DIGBERTControlWindow)
            ShowWindow(.CHINCAControlWindow)
            ShowWindow(.STINCAControlWindow)
        End With
        StartControlServiceHost()
        StartSimulationStateUpdater()
    End Sub

    Protected Overrides Sub OnExit(e As System.Windows.ExitEventArgs)
        StopControlServiceHost()
        StopSimulationStateUpdater()
        SaveTestDiaryComments()
        SaveUELEntries()
        My.Settings.Save()
        MyBase.OnExit(e)
    End Sub

    Private Sub ShowWindow(window As Window)
        AddHandler window.Closed, AddressOf Window_Closed
        window.Show()
    End Sub

    Private Sub Window_Closed(sender As Object, e As EventArgs)
        Shutdown()
    End Sub

    Private Sub AddAvailbleTestEngineComments()
        For Each Comment In My.Settings.AvailableTestEngineComments
            MainViewModel.Instance.TestEngine.CommentController.AvailableComments.Add(Comment)
        Next
    End Sub

#End Region

#Region "Test Diary"

    Private Sub LoadTestDiaryComments()
        If My.Settings.TestDiaryEntries Is Nothing Then
            MainViewModel.Instance.TestEngine.TestDiary.Add(String.Format("New test created {0}", My.Settings.TestName))
            Return
        End If
        For Each MySettingsEntry In My.Settings.TestDiaryEntries
            Dim Timestamp = DateTime.Parse(MySettingsEntry.Substring(0, MySettingsEntry.IndexOf("|"c)))
            Dim Comment = MySettingsEntry.Substring(MySettingsEntry.IndexOf("|"c) + 1)
            MainViewModel.Instance.TestEngine.TestDiary.Add(Timestamp, Comment)
        Next
    End Sub

    Private Sub SaveTestDiaryComments()
        If My.Settings.TestDiaryEntries Is Nothing Then My.Settings.TestDiaryEntries = New StringCollection
        Dim StartPos = MainViewModel.Instance.TestEngine.TestDiary.Entries.Count - 1000
        If StartPos < 0 Then StartPos = 0
        My.Settings.TestDiaryEntries.Clear()
        For I = StartPos To MainViewModel.Instance.TestEngine.TestDiary.Entries.Count - 1
            Dim Entry = MainViewModel.Instance.TestEngine.TestDiary.Entries(I)
            My.Settings.TestDiaryEntries.Add(String.Concat(Entry.Timestamp.ToString, "|", Entry.Comment))
        Next
    End Sub

#End Region

#Region "Unified Event Log"

    Private Sub LoadUELEntries()
        If My.Settings.UELLogEntries Is Nothing Then
            MainViewModel.Instance.UnifiedEventLog.Log(String.Format("New test created {0}", My.Settings.TestName))
            Return
        End If
        For Each MySettingsEntry In My.Settings.UELLogEntries
            Dim EntrySplit = MySettingsEntry.Split("|"c)
            Dim Severity = DirectCast([Enum].Parse(GetType(Severity), EntrySplit(0)), Severity)
            Dim Timestamp = DateTime.Parse(EntrySplit(1))
            Dim Source = EntrySplit(2)
            Dim Comment = EntrySplit(3)
            MainViewModel.Instance.UnifiedEventLog.Log(Severity, Timestamp, Source, Comment)
        Next
    End Sub

    Private Sub SaveUELEntries()
        If My.Settings.UELLogEntries Is Nothing Then My.Settings.UELLogEntries = New StringCollection
        Dim StartPos = MainViewModel.Instance.UnifiedEventLog.Entries.Count - 1000
        If StartPos < 0 Then StartPos = 0
        My.Settings.UELLogEntries.Clear()
        For I = StartPos To MainViewModel.Instance.UnifiedEventLog.Entries.Count - 1
            Dim Entry = MainViewModel.Instance.UnifiedEventLog.Entries(I)
            My.Settings.UELLogEntries.Add(String.Concat(Entry.Severity.ToString, "|", Entry.Timestamp.ToString, "|", Entry.Source, "|", Entry.Message))
        Next
    End Sub

#End Region

#Region "SimulationStateUpdater"

    Private WithEvents _SimulationStateUpdater As SimulationStateUpdateServiceHost

    Private Sub StartSimulationStateUpdater()
        _SimulationStateUpdater = New SimulationStateUpdateServiceHost
        _SimulationStateUpdater.StartWaitingForUpdates()
    End Sub

    Private Sub StopSimulationStateUpdater()
        If _SimulationStateUpdater IsNot Nothing Then
            _SimulationStateUpdater.StopWaitingForUpdates()
            _SimulationStateUpdater = Nothing
        End If
    End Sub

    Private Sub _SimulationStateUpdater_StateUpdated(sender As Object, e As Displays.Services.SimulationStateUpdatedEventArgs) Handles _SimulationStateUpdater.StateUpdated
        MainViewModel.Instance.TestEngine.Update(e.State)
        If e.State.EngineMode = _LastEngineMode Then Return
        _LastEngineMode = e.State.EngineMode
        If e.State.EngineMode = EngineMode.Starting Or e.State.EngineMode = EngineMode.Running Then
            If Not MainViewModel.Instance.TestEngine.TransientLogController.GetIsCTLogStarted Then
                _EngineStartCTLog = MainViewModel.Instance.TestEngine.TransientLogController.StartCTLog()
            End If
        ElseIf Not e.State.EngineMode = EngineMode.Stopped And _EngineStartCTLog IsNot Nothing Then
            MainViewModel.Instance.TestEngine.TransientLogController.StopLog(_EngineStartCTLog)
            _EngineStartCTLog = Nothing
        End If
    End Sub

#End Region

#Region "ControlServiceHost"

    Private WithEvents _ControlServiceHost As ControlServiceHost

    Private Sub StartControlServiceHost()
        _ControlServiceHost = New ControlServiceHost
        _ControlServiceHost.StartWaitingForUpdates()
    End Sub

    Private Sub StopControlServiceHost()
        If _ControlServiceHost IsNot Nothing Then
            _ControlServiceHost.Stop()
            _ControlServiceHost = Nothing
        End If
    End Sub

    Private Sub _ControlServiceHost_SimulationReset(sender As Object, e As System.EventArgs) Handles _ControlServiceHost.SimulationReset
        MainViewModel.Instance.Reset()
    End Sub

#End Region

End Class
