Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

#Region "Application"

    Protected Overrides Sub OnStartup(e As System.Windows.StartupEventArgs)
        MyBase.OnStartup(e)
        With My.Windows
            With .MainWindow
                .DataContext = MainViewModel.Instance
            End With
            With .VideoDisplayWindow
                .DataContext = VideoDisplayViewModel.Instance
            End With
            ShowWindow(.MainWindow)
            ShowWindow(.VideoDisplayWindow)
        End With

        StartControlServiceHost()
        StartSimulationStateUpdater()

        MainViewModel.Instance.CurrentPage = MainViewModel.Instance.EngineStartShutdownAndEEC
    End Sub

    Protected Overrides Sub OnExit(e As System.Windows.ExitEventArgs)
        StopControlServiceHost()
        StopSimulationStateUpdater()
        MyBase.OnExit(e)
    End Sub

    Private Sub ShowWindow(window As Window)
        AddHandler window.Closed, AddressOf Window_Closed
        window.Show()
    End Sub

    Private Sub Window_Closed(sender As Object, e As EventArgs)
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

    Private Sub _SimulationStateUpdater_StateUpdated(sender As Object, e As Displays.Services.SimulationStateUpdatedEventArgs) Handles _SimulationStateUpdater.StateUpdated
        MainViewModel.Instance.Update(e.State)
        VideoDisplayViewModel.Instance.Update(e.State)
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
