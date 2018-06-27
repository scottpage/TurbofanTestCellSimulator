Imports System.Windows.Input

Public NotInheritable Class Simulator
    Inherits ViewModelBase
    Implements IDisposable

#Region "Fields"

    Private _SimulatorStartTimestamp As DateTime = DateTime.UtcNow

    Private _State As New SimulationState
    Private _Engine As IEngine = New TrentXWB
    Private _SimulationUpdateClients As List(Of SimulationStateServiceClient)
    Private _EngineUpdateTimer As Timer
    Private _EngineStartVideoLeadTimeTimer As Timer
    Private _CurrentPlaybackTime As Double = 0.0#

#End Region

#Region "Constructors"

    Public Sub New()
        Start()
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property UpTimeUTC As TimeSpan
        Get
            Return DateTime.UtcNow.Subtract(_SimulatorStartTimestamp)
        End Get
    End Property

    Public ReadOnly Property State As SimulationState
        Get
            Return _State
        End Get
    End Property

    Public ReadOnly Property Engine As Simulation.Models.IEngine
        Get
            Return _Engine
        End Get
    End Property

    Private _SelectedEngineModeToForce As Models.EngineMode = Models.EngineMode.Stopped
    Public Property SelectedEngineModeToForce As Models.EngineMode
        Get
            Return _SelectedEngineModeToForce
        End Get
        Set(ByVal Value As Models.EngineMode)
            SetProperty(Function() SelectedEngineModeToForce, _SelectedEngineModeToForce, Value)
        End Set
    End Property

    Private _IsPaused As Boolean
    Public Property IsPaused As Boolean
        Get
            Return _IsPaused
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsPaused, _IsPaused, value)
        End Set
    End Property

#End Region

#Region "Commands"

#Region "Toggle Master Caution Command"

    Private _ToggleMasterCautionActiveCommand As ICommand
    Public ReadOnly Property ToggleMasterCautionActiveCommand As ICommand
        Get
            If _ToggleMasterCautionActiveCommand Is Nothing Then _
               _ToggleMasterCautionActiveCommand = New RelayCommand(AddressOf ToggleMasterCautionActive, AddressOf CanToggleMasterCautionActive)
            Return _ToggleMasterCautionActiveCommand
        End Get
    End Property

    Private Function CanToggleMasterCautionActive(obj As Object) As Boolean
        Return True
    End Function

    Private Sub ToggleMasterCautionActive(obj As Object)
        IsMasterCautionActive = Not IsMasterCautionActive
    End Sub

    Public Property IsMasterCautionActive As Boolean
        Get
            Return Engine.EEC.IsMasterCautionActive
        End Get
        Set(value As Boolean)
            If Engine.EEC.IsMasterCautionActive = value Then Return
            Engine.EEC.IsMasterCautionActive = value
            State.EnginePLC.IsMasterCautionActive = value
            OnPropertyChanged(Function() IsMasterCautionActive)
        End Set
    End Property

#End Region

#Region "Toggle Master Warning Command"

    Private _ToggleMasterWarningActiveCommand As ICommand
    Public ReadOnly Property ToggleMasterWarningActiveCommand As ICommand
        Get
            If _ToggleMasterWarningActiveCommand Is Nothing Then _
               _ToggleMasterWarningActiveCommand = New RelayCommand(AddressOf ToggleMasterWarningActive, AddressOf CanToggleMasterWarningActive)
            Return _ToggleMasterWarningActiveCommand
        End Get
    End Property

    Private Function CanToggleMasterWarningActive(obj As Object) As Boolean
        Return True
    End Function

    Private Sub ToggleMasterWarningActive(obj As Object)
        IsMasterWarningActive = Not IsMasterWarningActive
    End Sub

    Public Property IsMasterWarningActive As Boolean
        Get
            Return Engine.EEC.IsMasterWarningActive
        End Get
        Set(value As Boolean)
            If Engine.EEC.IsMasterWarningActive = value Then Return
            Engine.EEC.IsMasterWarningActive = value
            State.EnginePLC.IsMasterWarningActive = value
            OnPropertyChanged(Function() IsMasterWarningActive)
        End Set
    End Property

#End Region

    Private _ForceEngineModeCommand As ICommand
    Public ReadOnly Property ForceEngineModeCommand As ICommand
        Get
            If _ForceEngineModeCommand Is Nothing Then _
               _ForceEngineModeCommand = New RelayCommand(AddressOf ForceEngineMode, AddressOf CanForceEngineMode)
            Return _ForceEngineModeCommand
        End Get
    End Property

    Private Function CanForceEngineMode(obj As Object) As Boolean
        Return Not SelectedEngineModeToForce.Equals(SimulationConverter.ToModelEngineMode(State.EngineMode))
    End Function

    Private Sub ForceEngineMode(obj As Object)
        ForceEngineMode(SelectedEngineModeToForce)
    End Sub

    Private _TogglePauseCommand As ICommand
    Public ReadOnly Property TogglePauseCommand As ICommand
        Get
            If _TogglePauseCommand Is Nothing Then _
               _TogglePauseCommand = New RelayCommand(AddressOf TogglePause, AddressOf CanTogglePause)
            Return _TogglePauseCommand
        End Get
    End Property

    Private Function CanTogglePause(obj As Object) As Boolean
        Return True
    End Function

    Private Sub TogglePause(obj As Object)
        If IsPaused Then
            [Resume]()
        Else
            Pause()
        End If
    End Sub



#End Region

#Region "Simulation Control"

    Public Sub Start()
        _Engine = New TrentXWB
        _Engine.Type = "Trent XWB"
        _Engine.SerialNumber = "20005"
        _Engine.Build = "1A"
        _Engine.PartTest = "4"
        State.EngineBuild = _Engine.Build
        State.EnginePartTest = _Engine.PartTest
        State.EngineSerialNumber = _Engine.SerialNumber
        State.EngineType = _Engine.Type
        State.Parameters.Update(Engine)
        Reset()
        InitiateSimulationStateUpdateClients()
        _EngineUpdateTimer = New Timer(AddressOf Update, Nothing, 0, 100)
        Log("RTE ready to configure")
        Log("RTE is scanning...")
    End Sub

    Public Sub Pause()
        'TODO:  Shouldn't we terminate the SimulationStateUpdateClients?
        IsPaused = True
        _EngineUpdateTimer.Dispose()
        _EngineUpdateTimer = Nothing
    End Sub

    Public Sub [Resume]()
        'TODO:  Since we don't destroy the SimulationStateServiceClients in Pause(), should we be intantiating them again here?
        InitiateSimulationStateUpdateClients()
        _EngineUpdateTimer = New Timer(AddressOf Update, Nothing, 0, 100)
        IsPaused = False
    End Sub

    Public Overrides Sub Reset()
        ForceEngineMode(Models.EngineMode.Stopped)
    End Sub

    Private Sub InitiateSimulationStateUpdateClients()
        If _SimulationUpdateClients Is Nothing Then
            _SimulationUpdateClients = New List(Of SimulationStateServiceClient)
        Else
            For Each Client In _SimulationUpdateClients
                If Not Client.State = CommunicationState.Closing Or Not Client.State = CommunicationState.Closed Then
                    Client.Close()
                End If
            Next
            _SimulationUpdateClients.Clear()
        End If
        _SimulationUpdateClients.Add(New SimulationStateServiceClient("DriverSimulationStateClient"))
        _SimulationUpdateClients.Add(New SimulationStateServiceClient("EnginePLCSimulationStateClient"))
        _SimulationUpdateClients.Add(New SimulationStateServiceClient("ManagementSimulationStateClient"))
    End Sub

    ''' <summary>
    ''' Sends the shutdown command to all clients.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Shutdown()
        Dim Client As ControlServiceClient = Nothing
        Try
            Client = New ControlServiceClient("EnginePLCControlServiceClient")
            Client.ShutdownAsync()
            Client.Close()
            Client = New ControlServiceClient("DriverControlServiceClient")
            Client.ShutdownAsync()
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

    ''' <summary>
    ''' Forces the simulator to switch to the selected engine mode.  This changes the Engine PLC, Management, and Driver consoles and Engine to appear 
    ''' in their nominal state for the provided mode.
    ''' </summary>
    ''' <param name="mode">The mode to force.</param>
    ''' <remarks></remarks>
    Public Sub ForceEngineMode(mode As Models.EngineMode)
        Select Case mode
            Case Models.EngineMode.Running
                With State.EnginePLC
                    .IgnitionState = SimulationStateService.IgnitionState.Armed
                    .IsEMUPowerOn = True
                    .IsGroundPowerOn = True
                    .IsHighIdleSelected = False
                    .IsHydraulicPump1Detected = False
                    .IsHydraulicPump1NotFitted = True
                    .IsHydraulicPump2Detected = False
                    .IsHydraulicPump2NotFitted = True
                    .IsIgnitor1Commanded = False
                    .IsIgnitor2Commanded = False
                    .IsInFlight = False
                    .IsManualStartOn = False
                    .IsStartAirValveOpened = False
                    .IsStartMasterOn = False
                    .StartSelectorState = StartSelectorState.Normal
                    .IsFuelOn = True
                    .StartReadings.Clear()
                    .StopReadings.Clear()
                End With
                RunEngine()
            Case (Models.EngineMode.Starting)
                With State.EnginePLC
                    .IgnitionState = SimulationStateService.IgnitionState.Armed
                    .IsEMUPowerOn = True
                    .IsGroundPowerOn = True
                    .IsHighIdleSelected = False
                    .IsHydraulicPump1Detected = False
                    .IsHydraulicPump1NotFitted = True
                    .IsHydraulicPump2Detected = False
                    .IsHydraulicPump2NotFitted = True
                    .IsIgnitor1Commanded = True
                    .IsIgnitor2Commanded = True
                    .IsInFlight = False
                    .IsManualStartOn = False
                    .IsStartAirValveOpened = True
                    .IsStartMasterOn = True
                    .StartSelectorState = StartSelectorState.IgnitionStart
                    .IsFuelOn = True
                    .StartReadings.Clear()
                    .StopReadings.Clear()
                End With
                StartEngine()
            Case Models.EngineMode.Stopped
                With State.EnginePLC
                    .IgnitionState = SimulationStateService.IgnitionState.Off
                    .IsEMUPowerOn = False
                    .IsGroundPowerOn = False
                    .IsHighIdleSelected = False
                    .IsHydraulicPump1Detected = False
                    .IsHydraulicPump1NotFitted = True
                    .IsHydraulicPump2Detected = False
                    .IsHydraulicPump2NotFitted = True
                    .IsIgnitor1Commanded = False
                    .IsIgnitor2Commanded = False
                    .IsInFlight = False
                    .IsManualStartOn = False
                    .IsStartAirValveOpened = False
                    .IsStartMasterOn = False
                    .IsMasterCautionActive = False
                    .IsMasterWarningActive = False
                    .StartSelectorState = StartSelectorState.Normal
                    .IsFuelOn = False
                    .StartReadings.Clear()
                    .StopReadings.Clear()
                End With
                StopEngine()
            Case Models.EngineMode.Stopping
                With State.EnginePLC
                    .IgnitionState = SimulationStateService.IgnitionState.Armed
                    .IsEMUPowerOn = True
                    .IsGroundPowerOn = True
                    .IsHighIdleSelected = False
                    .IsHydraulicPump1Detected = False
                    .IsHydraulicPump1NotFitted = True
                    .IsHydraulicPump2Detected = False
                    .IsHydraulicPump2NotFitted = True
                    .IsIgnitor1Commanded = False
                    .IsIgnitor2Commanded = False
                    .IsInFlight = False
                    .IsManualStartOn = False
                    .IsStartAirValveOpened = False
                    .IsStartMasterOn = True
                    .StartSelectorState = StartSelectorState.Normal
                    .IsFuelOn = False
                    .StartReadings.Clear()
                    .StopReadings.Clear()
                End With
                ShutdownEngine()
        End Select
    End Sub

#End Region

#Region "Engine Control"

#Region "Engine Stopped/Prestart"

    ''' <summary>
    ''' Stops the engine.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopEngine()
        Engine.Mode = Models.EngineMode.Stopped
        State.EngineMode = SimulationStateService.EngineMode.Stopped
        State.EnginePLC.IsHydraulicPump1Detected = False
        State.EnginePLC.IsHydraulicPump2Detected = False
        State.EnginePLC.IsHydraulicPump1NotFitted = False
        State.EnginePLC.IsHydraulicPump2NotFitted = False
    End Sub

#End Region

#Region "Engine Starting"

    Private _EngineStartTimestamp As DateTime
    Private _IsAbortingStart As Boolean

    ''' <summary>
    ''' Starts the engine from stopped.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartEngine()
        _ShaftStartRotationRecorded = False
        _StarterCutRecorded = False
        _EngineStartTimestamp = DateTime.Now
        _IsRestarting = False
        _CurrentPlaybackTime = 0
        ClearStartReadings()
        If TypeOf State.Parameters Is TrentXWBParameters Then
            State.EnginePLC.StartReadings.PrestartStartAirPressure = DirectCast(State.Parameters, TrentXWBParameters).StarterPressure.Value
        End If
        State.EnginePLC.StartReadings.PrestartTGT = State.Parameters.TurbineGasTemperature.Average
        State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.On
        State.EnginePLC.IsStartAirValveOpened = True
        State.EnginePLC.IsIgnitor1Commanded = True
        State.EnginePLC.IsIgnitor2Commanded = True
        State.EngineMode = SimulationStateService.EngineMode.Starting
        _EngineStartVideoLeadTimeTimer = New Timer(AddressOf EngineStartVideoLeadTimeTimerElapsed, Nothing, Convert.ToInt64(TimeSpan.FromSeconds(Engine.StartVideoLeadTime).TotalMilliseconds), Timeout.Infinite)
        'Engine.Mode now changed by EngineStartVideoLeadTimeTimer to allow for starter to engage on video playback before setting parameters to Starting.
        Log("SIM", "Engine start")
    End Sub

    ''' <summary>
    ''' Shuts down the engine from starting instead of from running.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbortStart()
        _IsAbortingStart = True
        _CurrentPlaybackTime = Engine.ShutdownDuration - _CurrentPlaybackTime
        If State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.On Then State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.Armed
        State.EnginePLC.IsStartAirValveOpened = False
        State.EnginePLC.IsIgnitor1Commanded = False
        State.EnginePLC.IsIgnitor2Commanded = False
        Engine.Mode = Models.EngineMode.Stopping
        State.EngineMode = SimulationStateService.EngineMode.Stopping
        Log("SIM", "Engine start aborted")
    End Sub

    Private Sub UpdateStartingEngine()
        Engine.Update(_CurrentPlaybackTime)
        UpdateStartReadings()
        _CurrentPlaybackTime = DateTime.Now.Subtract(_EngineStartTimestamp).TotalSeconds - Engine.StartVideoLeadTime
        If _CurrentPlaybackTime >= Engine.StartDuration Then
            RunEngine()
            UpdateStartReadings()
        End If
    End Sub

    Private Sub EngineStartVideoLeadTimeTimerElapsed(obj As Object)
        If _EngineStartVideoLeadTimeTimer IsNot Nothing Then
            _EngineStartVideoLeadTimeTimer.Dispose()
            _EngineStartVideoLeadTimeTimer = Nothing
        End If
        Engine.Mode = Models.EngineMode.Starting
    End Sub

#Region "Start Readings"

    Private _MaxTGTDuringStart As Double
    Private _StarterCutRecorded As Boolean
    Private _ShaftStartRotationRecorded As Boolean

    Private Sub ClearStartReadings()
        State.EnginePLC.StartReadings.Clear()
        _MaxTGTDuringStart = 0
    End Sub

    Private Sub UpdateStartReadings()
        With State.EnginePLC.StartReadings
            _MaxTGTDuringStart = Math.Max(_MaxTGTDuringStart, Engine.TurbineGasTemperature.Value.Average)
            If _CurrentPlaybackTime >= Engine.StartVideoLeadTime And Not _ShaftStartRotationRecorded Then
                _ShaftStartRotationRecorded = True
                .HPRotation = 0
                .IPRotation = 3
                .LPRotation = 5
                If TypeOf State.Parameters Is TrentXWBParameters Then
                    State.EnginePLC.StartReadings.StartingStartAirPressure = DirectCast(State.Parameters, TrentXWBParameters).StarterPressure.Value
                End If
            End If
            If _CurrentPlaybackTime >= Engine.StarterCutTime And Not _StarterCutRecorded Then
                Dim StarterCutSeconds = DateTime.Now.Subtract(_EngineStartTimestamp).TotalSeconds
                _StarterCutRecorded = True
                If TypeOf Engine Is TrentXWB Then
                    .StarterCut = DirectCast(Engine, TrentXWB).Shaft3.Value.Current
                Else
                    .StarterCut = Engine.Shaft2.Value.Current
                End If
                .StarterCutSeconds = StarterCutSeconds
                .N2DeadCrankSpeed = State.Parameters.Shaft2.Average
                .N2DeadCrankSeconds = StarterCutSeconds
                .N3DeadCrankSpeed = State.Parameters.Shaft2.Average
                .N3DeadCrankSeconds = StarterCutSeconds
                If TypeOf State.Parameters Is TrentXWBParameters Then .N3DeadCrankSpeed = DirectCast(State.Parameters, TrentXWBParameters).Shaft3.Average
                .N3DeadCrankSeconds = StarterCutSeconds
                .Lit = .N3DeadCrankSpeed - 3
                .LitSeconds = StarterCutSeconds - 2
                .LightupFuelFlow = 512
                .FuelOn = .N3DeadCrankSpeed
                .FuelOnSeconds = Engine.StarterCutTime - 5
            End If
            If State.EngineMode = SimulationStateService.EngineMode.Running Then
                .TimeToGI = DateTime.Now.Subtract(_EngineStartTimestamp).TotalSeconds
                .MaxTGT = _MaxTGTDuringStart
                .OilPressureAtIdle = State.Parameters.OilPressure.Average
                .AirIntakeTemperatureAtIdle = State.AirInletTemperature
            End If
        End With
    End Sub

#End Region

#End Region

#Region "Engine Shutdown"

    Private _EngineShutdownTimestamp As DateTime
    Private _IsRestarting As Boolean

    ''' <summary>
    ''' Shuts down the engine from running.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShutdownEngine()
        _EngineShutdownTimestamp = DateTime.Now
        _MaxVibrationDuringShutdown = 0
        State.EnginePLC.StopReadings.Clear()
        If _IsAbortingStart Then
            _IsAbortingStart = False
            _EngineShutdownTimestamp = _EngineStartTimestamp
        End If
        _CurrentPlaybackTime = 0
        If State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.On Then State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.Armed
        State.EnginePLC.IsStartAirValveOpened = False
        State.EnginePLC.IsIgnitor1Commanded = False
        State.EnginePLC.IsIgnitor2Commanded = False
        Engine.Mode = Models.EngineMode.Stopping
        State.EngineMode = SimulationStateService.EngineMode.Stopping
        Log("SIM", "Engine stop")
    End Sub

    ''' <summary>
    ''' Restarts the engine during a shutdown instead of starting from stopped.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RestartEngine()
        ClearStartReadings()
        _IsRestarting = True
        State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.On
        State.EnginePLC.IsStartAirValveOpened = True
        State.EnginePLC.IsIgnitor1Commanded = True
        State.EnginePLC.IsIgnitor2Commanded = True
        Engine.Mode = Models.EngineMode.Starting
        State.EngineMode = SimulationStateService.EngineMode.Starting
        Log("SIM", "Engine start")
    End Sub

    Private Sub UpdateStoppingEngine()
        Engine.Update(_CurrentPlaybackTime)
        UpdateStopReadings()
        _CurrentPlaybackTime = DateTime.Now.Subtract(_EngineShutdownTimestamp).TotalSeconds
        If _CurrentPlaybackTime >= Engine.ShutdownDuration Then
            StopEngine()
            UpdateStopReadings()
        End If
    End Sub

#Region "Stop Readings"

    Private _MaxVibrationDuringShutdown As Double

    Private Sub UpdateStopReadings()
        _MaxVibrationDuringShutdown = Math.Max(_MaxVibrationDuringShutdown, Engine.LaneABroadbandVibration.Value.Average)
        Select Case Engine.Mode
            Case Models.EngineMode.Stopped
                State.EnginePLC.StopReadings.MaxVibrationDuringRundown = _MaxVibrationDuringShutdown
                State.EnginePLC.StopReadings.NoIPRotation = Engine.ShutdownDuration
                State.EnginePLC.StopReadings.NoHPRotation = Engine.ShutdownDuration - 43
                State.EnginePLC.StopReadings.NoLPRotation = Engine.ShutdownDuration - 90
                State.EnginePLC.StopReadings.EngineOilContent = Engine.Oil.Quantity.Value.Average
                State.EnginePLC.StopReadings.EngineOilTemperature = Engine.Oil.Temperature.Value.Average
        End Select
    End Sub

#End Region

#End Region

#Region "Engine Running"

    ''' <summary>
    ''' Runs the engine from starting.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RunEngine()
        State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.Armed
        State.EnginePLC.IsStartAirValveOpened = False
        State.EnginePLC.IsIgnitor1Commanded = True
        State.EnginePLC.IsIgnitor2Commanded = True
        Engine.Mode = Models.EngineMode.Running
        State.EngineMode = SimulationStateService.EngineMode.Running
        Log("SIM", "Engine running")
    End Sub

#End Region

#Region "Engine Dry Crank"

    ''' <summary>
    ''' Dry cranks the engine.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DryCrankEngine()
        _CurrentPlaybackTime = 0
        State.EnginePLC.IsStartAirValveOpened = True
        State.EnginePLC.IsIgnitor1Commanded = False
        State.EnginePLC.IsIgnitor2Commanded = False
        Engine.Mode = Models.EngineMode.DryCranking
        State.EngineMode = SimulationStateService.EngineMode.DryCranking
        Log("SIM", "Engine dry crank")
    End Sub

#End Region

#Region "Engine Wet Crank"

    ''' <summary>
    ''' Wet cranks the engine.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub WetCrankEngine()
        _CurrentPlaybackTime = 0
        State.EnginePLC.IsStartAirValveOpened = True
        State.EnginePLC.IsIgnitor1Commanded = False
        State.EnginePLC.IsIgnitor2Commanded = False
        Engine.Mode = Models.EngineMode.WetCranking
        State.EngineMode = SimulationStateService.EngineMode.WetCranking
        Log("SIM", "Engine wet crank")
    End Sub

#End Region

    Private Sub Update(obj As Object)
        Select Case Engine.Mode
            Case Models.EngineMode.None
            Case Models.EngineMode.DryCranking
                DryCrankEngine()
            Case Models.EngineMode.Running
                Engine.Update()
            Case Models.EngineMode.Starting
                UpdateStartingEngine()
            Case Models.EngineMode.Stopped
                Engine.Update()
            Case Models.EngineMode.Stopping
                UpdateStoppingEngine()
            Case Models.EngineMode.WetCranking
                WetCrankEngine()
        End Select
        Engine.Shaft1.UpdateCorrectedSpeeds(State.AirInletTemperature)
        State.Parameters.Update(Engine)
        State.Alarms = FindAllAlarmingAlarms.ToArray
        For Each Client In _SimulationUpdateClients
            If Client IsNot Nothing Then
                Try
                    Client.UpdateAsync(State)
                Catch ex As Exception
                    'TODO:  Do something with the exception
                End Try
            End If
        Next
    End Sub

#End Region

#Region "Alarm Methods"

    Private Function FindAllAlarmingAlarms() As IEnumerable(Of AlarmValue)
        Dim AlarmingAlarms As New List(Of AlarmValue)
        Dim AlarmingParameters = Engine.Parameters.ToList.Where(Function(p) p.IsAlarming)
        If AlarmingParameters IsNot Nothing Then
            For Each P In AlarmingParameters
                Dim Param = P
                Dim Alarm = Param.Alarms.GetAlarmingAlarm
                If Alarm IsNot Nothing Then
                    Dim AlarmValue As New AlarmValue(Param, Alarm)
                    AlarmingAlarms.Add(AlarmValue)
                End If
            Next
        End If
        Return AlarmingAlarms
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).

            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
