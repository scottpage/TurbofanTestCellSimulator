Imports System.Windows.Threading
Imports System.Threading

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

#Region "Application"

    Private _IsDriverWindowClosed As Boolean
    Private _IsVibrationWindowClosed As Boolean
    Private _IsAlarmWindowClosed As Boolean

    Protected Overrides Sub OnStartup(e As System.Windows.StartupEventArgs)
        'LoadClassicTheme()
        Try
            ThrottleViewModel.Instance.Throttle.Connect()
        Catch ex As Exception
            Xceed.Wpf.Toolkit.MessageBox.Show("Error starting throttle")
        End Try

        With My.Windows
            .AlarmPanelWindow.DataContext = AlarmPanelViewModel.Instance
            .DriverWindow.DataContext = DriverViewModel.Instance
            .VibrationWindow.DataContext = VibrationViewModel.Instance
            ShowWindow(.DriverWindow)
            ShowWindow(.VibrationWindow)
            ShowWindow(.AlarmPanelWindow)
        End With

        StartControlServiceHost()
        StartSimulationStateUpdater()
        StartUpdatingThrottleLeverAngle()
        MyBase.OnStartup(e)
    End Sub

    Private Sub LoadClassicTheme()
        Dim Uri As New Uri(“PresentationFramework.Classic;component/themes/classic.xaml", UriKind.RelativeOrAbsolute)
        Dim RD As ResourceDictionary = CType(LoadComponent(Uri), ResourceDictionary)
        Resources.MergedDictionaries.Add(RD)
    End Sub

    Protected Overrides Sub OnExit(e As System.Windows.ExitEventArgs)
        If _IsDriverWindowClosed Then
            RemoveHandler My.Windows.VibrationWindow.Closed, AddressOf Window_Closed
            RemoveHandler My.Windows.AlarmPanelWindow.Closed, AddressOf Window_Closed
            My.Windows.VibrationWindow.Close()
            My.Windows.AlarmPanelWindow.Close()
        ElseIf _IsVibrationWindowClosed Then
            RemoveHandler My.Windows.DriverWindow.Closed, AddressOf Window_Closed
            RemoveHandler My.Windows.AlarmPanelWindow.Closed, AddressOf Window_Closed
            My.Windows.DriverWindow.Close()
            My.Windows.AlarmPanelWindow.Close()
        ElseIf _IsAlarmWindowClosed Then
            RemoveHandler My.Windows.DriverWindow.Closed, AddressOf Window_Closed
            RemoveHandler My.Windows.VibrationWindow.Closed, AddressOf Window_Closed
            My.Windows.DriverWindow.Close()
            My.Windows.VibrationWindow.Close()
        End If
        StopUpdatingThrottleLeverAngle()
        StopSimulationStateUpdater()
        StopControlServiceHost()
        ThrottleViewModel.Instance.TerminateThrottle()
        MyBase.OnExit(e)
    End Sub

    Private Sub ShowWindow(window As Window)
        AddHandler window.Closed, AddressOf Window_Closed
        window.Show()
    End Sub

    Private Sub Window_Closed(sender As Object, e As EventArgs)
        _IsDriverWindowClosed = (sender Is My.Windows.DriverWindow)
        _IsVibrationWindowClosed = (sender Is My.Windows.VibrationWindow)
        _IsAlarmWindowClosed = (sender Is My.Windows.AlarmPanelWindow)
        Shutdown()
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

    Private Sub _SimulationStateUpdater_StateUpdated(sender As Object, e As SimulationStateUpdatedEventArgs) Handles _SimulationStateUpdater.StateUpdated
        DriverViewModel.Instance.Update(e.State.Parameters)
        VibrationViewModel.Instance.Update(e.State.Parameters)
        AlarmPanelViewModel.Instance.UpdateAlarms(e.State.Alarms)
        ThrottleViewModel.Instance.Throttle.MinimumLeverAngle = e.State.Parameters.ThrottleLeverAngle.Minimum
        ThrottleViewModel.Instance.Throttle.MaximumLeverAngle = e.State.Parameters.ThrottleLeverAngle.Maximum
        DriverViewModel.Instance.IsFullsetRunning = e.State.IsFullsetRunning
        DriverViewModel.Instance.IsMasterCautionActive = e.State.EnginePLC.IsMasterCautionActive
        DriverViewModel.Instance.IsMasterWarningActive = e.State.EnginePLC.IsMasterWarningActive
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
        DriverViewModel.Instance.Reset()
        VibrationViewModel.Instance.Reset()
        AlarmPanelViewModel.Instance.Reset()
    End Sub

#End Region

#Region "Throttle"

    Private WithEvents _ThrottleServiceMonitor As ServiceAnnouncementMonitor
    Private _ThrottleClient As ThrottleServiceClient
    Private _ThrottleLeverAngleUpdateTimer As Timer

    Private Sub StartUpdatingThrottleLeverAngle()
        _ThrottleServiceMonitor = New ServiceAnnouncementMonitor(GetType(IThrottleService))
        _ThrottleClient = New ThrottleServiceClient
        _ThrottleLeverAngleUpdateTimer = New Timer(AddressOf UpdateThrottleLeverAngle, Nothing, 0, 100)
    End Sub

    Private Sub StopUpdatingThrottleLeverAngle()
        If _ThrottleLeverAngleUpdateTimer IsNot Nothing Then
            _ThrottleLeverAngleUpdateTimer.Dispose()
            _ThrottleLeverAngleUpdateTimer = Nothing
        End If
        If _ThrottleClient IsNot Nothing Then
            _ThrottleClient.Close()
            _ThrottleClient = Nothing
        End If
    End Sub

    Private Sub UpdateThrottleLeverAngle(state As Object)
        If _ThrottleClient IsNot Nothing Then
            Dim TLA = ThrottleViewModel.Instance.Throttle.LeverAngle
            If Double.IsNaN(TLA) Or Double.IsInfinity(TLA) Then TLA = 0
            Try
                _ThrottleClient.UpdateLeverAngleAsync(TLA)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub _ThrottleServiceMonitor_Offline(sender As Object, e As System.EventArgs) Handles _ThrottleServiceMonitor.Offline
        StopUpdatingThrottleLeverAngle()
    End Sub

    Private Sub _ThrottleServiceMonitor_Online(sender As Object, e As System.EventArgs) Handles _ThrottleServiceMonitor.Online
        StartUpdatingThrottleLeverAngle()
    End Sub

#End Region

End Class
