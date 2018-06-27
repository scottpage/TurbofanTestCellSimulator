Class Application

#Region "Application"

    Private _IsMainWindowsClosed As Boolean

    Protected Overrides Sub OnStartup(e As System.Windows.StartupEventArgs)
        MyBase.OnStartup(e)
        MainWindow = New MainWindow
        AddHandler MainWindow.Closed, AddressOf MainWindow_Closed
        MainWindow.DataContext = MainViewModel.Instance
        MainWindow.Show()
        MainViewModel.Instance.Services.StartAll()
        'StartVoiceCommand()
    End Sub

    Protected Overrides Sub OnExit(e As System.Windows.ExitEventArgs)
        If Not _IsMainWindowsClosed Then
            RemoveHandler MainWindow.Closed, AddressOf MainWindow_Closed
            MainWindow.Close()

        End If
        MainViewModel.Instance.Services.StopAll()
        MyBase.OnExit(e)
    End Sub

    Private Sub MainWindow_Closed(sender As Object, e As EventArgs)
        _IsMainWindowsClosed = True
        Shutdown()
    End Sub

#End Region

#Region "Voice Command"

    Private WithEvents _SpeechRecognizer As SpeechRecognizer
    Private WithEvents _SpeechSynthesizer As SpeechSynthesizer

    Private Sub StartVoiceCommand()
        Dim SpeechChoices = GetSpeechChoices()
        Dim Builder As New GrammarBuilder(SpeechChoices)
        Dim Grammer As New Grammar(Builder)
        _SpeechSynthesizer = New SpeechSynthesizer
        _SpeechRecognizer = New SpeechRecognizer
        _SpeechRecognizer.LoadGrammar(Grammer)
        _SpeechRecognizer.Enabled = True
    End Sub

    Private Sub _SpeechRecognizer_SpeechRecognized(sender As Object, e As System.Speech.Recognition.SpeechRecognizedEventArgs) Handles _SpeechRecognizer.SpeechRecognized
        Select Case e.Result.Text
            Case "engine prestart"
                MainViewModel.Instance.Simulator.ForceEngineMode(EngineMode.Stopped)
                _SpeechSynthesizer.SpeakAsync("engine is ready to start")
            Case "engine start"
                MainViewModel.Instance.Simulator.ForceEngineMode(EngineMode.Starting)
                _SpeechSynthesizer.SpeakAsync("starting engine")
            Case "engine run"
                MainViewModel.Instance.Simulator.ForceEngineMode(EngineMode.Running)
                _SpeechSynthesizer.SpeakAsync("engine is running")
            Case "engine shutdown"
                MainViewModel.Instance.Simulator.ForceEngineMode(EngineMode.Stopping)
                _SpeechSynthesizer.SpeakAsync("shutting down")
            Case "exit"
                _SpeechSynthesizer.Speak("simulation is terminating")
                MainViewModel.Instance.Simulator.Shutdown()
                Shutdown()
            Case "reset"
                MainViewModel.Instance.Reset()
                _SpeechSynthesizer.SpeakAsync("resetting simulation")
        End Select
    End Sub

    Private Function GetSpeechChoices() As Choices
        Dim Choices As New Choices
        Choices.Add("engine prestart")
        Choices.Add("engine start")
        Choices.Add("engine run")
        Choices.Add("engine shutdown")
        Choices.Add("exit")
        Choices.Add("reset")
        Return Choices
    End Function

#End Region

End Class
