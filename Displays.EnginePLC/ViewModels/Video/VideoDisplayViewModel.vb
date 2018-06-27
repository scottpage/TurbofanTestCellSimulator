Imports System.Windows.Threading

Public Class VideoDisplayViewModel
    Inherits RealTimeDisplayViewModel

    Private Shared _Instance As VideoDisplayViewModel
    Private WithEvents _Timer As DispatcherTimer
    Private _DidEngineModeChange As Boolean
    Private _EngineMode As EngineMode = EngineMode.Stopped

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance As VideoDisplayViewModel
        Get
            If _Instance Is Nothing Then _Instance = New VideoDisplayViewModel
            Return _Instance
        End Get
    End Property

    Private _MediaPlayer As WPFMediaKit.DirectShow.Controls.MediaUriElement
    Public Property MediaPlayer As WPFMediaKit.DirectShow.Controls.MediaUriElement
        Get
            Return _MediaPlayer
        End Get
        Set(value As WPFMediaKit.DirectShow.Controls.MediaUriElement)
            If _MediaPlayer Is value Then Return
            _MediaPlayer = value
            If MediaPlayer IsNot Nothing Then
                StartPlaying()
            Else
                StopPlaying()
            End If
        End Set
    End Property

    Private _Volume As Double = 1
    Public Property Volume As Double
        Get
            Return _Volume
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() Volume, _Volume, Value)
        End Set
    End Property

    Private _Source As Uri = New Uri(System.IO.Path.Combine(My.Settings.BaseVideoPath, My.Settings.EngineStoppedVideoFilename))
    Public Property Source As Uri
        Get
            Return _Source
        End Get
        Set(ByVal Value As Uri)
            SetProperty(Function() Source, _Source, Value)
            If MediaPlayer IsNot Nothing Then
                StartPlaying()
            Else
                StopPlaying()
            End If
        End Set
    End Property

    Public Sub StartPlaying()
        _Timer = New DispatcherTimer
        _Timer.Interval = TimeSpan.FromMilliseconds(100)
        _Timer.Start()
    End Sub

    Public Sub StopPlaying()
        If _Timer IsNot Nothing Then
            _Timer.Stop()
            _Timer = Nothing
        End If
    End Sub

    Public Overrides Sub Update(state As SimulationState)
        If state.EngineMode = _EngineMode Then Return
        _DidEngineModeChange = True
        _EngineMode = state.EngineMode
    End Sub

    Private Sub _Timer_Tick(sender As Object, e As System.EventArgs) Handles _Timer.Tick
        If _DidEngineModeChange Then
            _DidEngineModeChange = False
            Select Case _EngineMode
                Case Services.EngineMode.Stopped
                    Source = New Uri(System.IO.Path.Combine(My.Settings.BaseVideoPath, My.Settings.EngineStoppedVideoFilename))
                    Volume = 0.5
                Case Services.EngineMode.Running
                    Source = New Uri(System.IO.Path.Combine(My.Settings.BaseVideoPath, My.Settings.EngineIdleVideoFilename))
                    Volume = 1
                Case Services.EngineMode.Starting
                    Source = New Uri(System.IO.Path.Combine(My.Settings.BaseVideoPath, My.Settings.EngineStartVideoFilename))
                    Volume = 1
                Case Services.EngineMode.Stopping
                    Source = New Uri(System.IO.Path.Combine(My.Settings.BaseVideoPath, My.Settings.EngineShutdownVideoFilename))
                    Volume = 1
            End Select
        End If
    End Sub

End Class
