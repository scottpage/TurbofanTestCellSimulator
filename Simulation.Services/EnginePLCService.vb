<ServiceBehavior(InstanceContextMode:=InstanceContextMode.Single)>
Public Class EnginePLCService
    Inherits ServiceBase
    Implements IEnginePLCService

#Region "Constructors"

    Public Sub New()
        MyBase.New(Nothing)
    End Sub

    Public Sub New(simulator As Simulator)
        MyBase.New(simulator)
    End Sub

#End Region

#Region "IEnginePLCService"

    Public Sub ClearStartReadings() Implements IEnginePLCService.ClearStartReadings
        Simulator.State.EnginePLC.StartReadings.Clear()
    End Sub

    Public Sub ClearStopReadings() Implements IEnginePLCService.ClearStopReadings
        Simulator.State.EnginePLC.StopReadings.Clear()
    End Sub

    Public Sub UpdateEMUPowerOn(isOn As Boolean) Implements IEnginePLCService.UpdateEMUPowerOn
        Simulator.State.EnginePLC.IsEMUPowerOn = isOn
        UpdateEngineState()
    End Sub

    Public Sub UpdateFuelOn(isOn As Boolean) Implements IEnginePLCService.UpdateFuelOn
        Simulator.State.EnginePLC.IsFuelOn = isOn
        UpdateEngineState()
    End Sub

    Public Sub UpdateGroundPowerOn(isOn As Boolean) Implements IEnginePLCService.UpdateGroundPowerOn
        Simulator.State.EnginePLC.IsGroundPowerOn = isOn
        UpdateEngineState()
    End Sub

    Public Sub UpdateHighIdle(isHighIdle As Boolean) Implements IEnginePLCService.UpdateHighIdle
        Simulator.State.EnginePLC.IsHighIdleSelected = isHighIdle
        Simulator.Engine.EEC.IsInHighIdleMode = True
        UpdateEngineState()
    End Sub

    Public Sub UpdateHydraulicPump1Fitted(isNotFitted As Boolean) Implements IEnginePLCService.UpdateHydraulicPump1Fitted
        Simulator.State.EnginePLC.IsHydraulicPump1NotFitted = isNotFitted
        UpdateEngineState()
    End Sub

    Public Sub UpdateHydraulicPump2Fitted(isNotFitted As Boolean) Implements IEnginePLCService.UpdateHydraulicPump2Fitted
        Simulator.State.EnginePLC.IsHydraulicPump2NotFitted = isNotFitted
        UpdateEngineState()
    End Sub

    Public Sub UpdateIgnitionArmed() Implements IEnginePLCService.UpdateIgnitionArmed
        Simulator.State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.Armed
        UpdateEngineState()
    End Sub

    Public Sub UpdateIgnitionOff() Implements IEnginePLCService.UpdateIgnitionOff
        Simulator.State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.Off
        UpdateEngineState()
    End Sub

    Public Sub UpdateIgnitionOn() Implements IEnginePLCService.UpdateIgnitionOn
        Simulator.State.EnginePLC.IgnitionState = SimulationStateService.IgnitionState.On
        UpdateEngineState()
    End Sub

    Public Sub UpdateIsInFlight(isInFlight As Boolean) Implements IEnginePLCService.UpdateIsInFlight
        If isInFlight Then
            Simulator.State.EnginePLC.IsInFlight = True
            Simulator.Engine.EEC.IsInFlightMode = True
        Else
            Simulator.State.EnginePLC.IsInFlight = False
            Simulator.Engine.EEC.IsInFlightMode = False
            Simulator.State.EnginePLC.IsHighIdleSelected = False
            Simulator.Engine.EEC.IsInHighIdleMode = False
        End If
        UpdateEngineState()
    End Sub

    Public Sub UpdateManualStart(isOn As Boolean) Implements IEnginePLCService.UpdateManualStart
        Simulator.State.EnginePLC.IsManualStartOn = isOn
        UpdateEngineState()
    End Sub

    Public Sub UpdateStartMasterOn(isOn As Boolean) Implements IEnginePLCService.UpdateStartMasterOn
        Simulator.State.EnginePLC.IsStartMasterOn = isOn
        If Not isOn Then UpdateStartSelectorNormal()
        UpdateEngineState()
    End Sub

    Public Sub UpdateStartSelectorCrank() Implements IEnginePLCService.UpdateStartSelectorCrank
        Simulator.State.EnginePLC.StartSelectorState = SimulationStateService.StartSelectorState.Crank
        UpdateEngineState()
    End Sub

    Public Sub UpdateStartSelectorIgnitionStart() Implements IEnginePLCService.UpdateStartSelectorIgnitionStart
        Simulator.State.EnginePLC.StartSelectorState = SimulationStateService.StartSelectorState.IgnitionStart
        UpdateEngineState()
    End Sub

    Public Sub UpdateStartSelectorNormal() Implements IEnginePLCService.UpdateStartSelectorNormal
        Simulator.State.EnginePLC.StartSelectorState = SimulationStateService.StartSelectorState.Normal
        UpdateEngineState()
    End Sub

#End Region

    Private Sub UpdateEngineState()
        Dim Handler As New Action(AddressOf UpdateEngineStateAsync)
        Handler.BeginInvoke(Nothing, Nothing)
    End Sub

    Private Sub UpdateEngineStateAsync()
        Select Case Simulator.Engine.Mode
            Case EngineMode.DryCranking
                If IsAbortingStart() Then Simulator.AbortStart()
            Case EngineMode.Running
                If CanShutdown() Then Simulator.ShutdownEngine()
            Case EngineMode.Starting
                If IsAbortingStart() Then Simulator.AbortStart()
            Case EngineMode.Stopped
                If CanStart() Then
                    Simulator.StartEngine()
                ElseIf CanDryCrank() Then
                    Simulator.StartEngine()
                ElseIf CanWetCrank() Then
                    Simulator.StartEngine()
                End If
            Case EngineMode.Stopping
                If IsRestarting() Then Simulator.RestartEngine()
            Case EngineMode.WetCranking
                If IsAbortingStart() Then Simulator.AbortStart()
        End Select
    End Sub

    Private Function CanStart() As Boolean
        Dim Result As Boolean = True
        Select Case Simulator.Engine.Mode
            Case EngineMode.AbortingStart, EngineMode.DryCranking, EngineMode.None, EngineMode.Running, EngineMode.Starting, EngineMode.WetCranking
                Result = False
            Case EngineMode.Stopped, EngineMode.Stopping
                With Simulator.State.EnginePLC
                    Result = .StartSelectorState = SimulationStateService.StartSelectorState.IgnitionStart _
                        And .IsGroundPowerOn _
                        And .IsEMUPowerOn _
                        And .IsStartMasterOn _
                        And (.IgnitionState = SimulationStateService.IgnitionState.Armed _
                             Or .IgnitionState = SimulationStateService.IgnitionState.On) _
                         And .IsHydraulicPump1NotFitted _
                         And .IsHydraulicPump2NotFitted
                End With
        End Select
        Return Result
    End Function

    Private Function CanShutdown() As Boolean
        Dim Result As Boolean = True
        Select Case Simulator.Engine.Mode
            Case EngineMode.AbortingStart, EngineMode.None, EngineMode.Stopped, EngineMode.Stopping
                Result = False
            Case EngineMode.DryCranking, EngineMode.Running, EngineMode.Starting, EngineMode.WetCranking
                Result = Not Simulator.State.EnginePLC.IsFuelOn
        End Select
        Return Result
    End Function

    Private Function CanDryCrank() As Boolean
        Dim Result As Boolean = True
        Select Case Simulator.Engine.Mode
            Case EngineMode.DryCranking, EngineMode.None, EngineMode.Running, EngineMode.Starting, EngineMode.WetCranking
                Return False
            Case EngineMode.AbortingStart, EngineMode.Stopped, EngineMode.Stopping
                With Simulator.State.EnginePLC
                    Result = .IsGroundPowerOn _
                        And .IsEMUPowerOn _
                        And .IsStartMasterOn _
                        And .StartSelectorState = SimulationStateService.StartSelectorState.Crank _
                         And .IsHydraulicPump1NotFitted _
                         And .IsHydraulicPump2NotFitted
                End With
        End Select
        Return Result
    End Function

    Private Function CanWetCrank() As Boolean
        Return False
    End Function

    Private Function IsRestarting() As Boolean
        Dim Result As Boolean = True
        Select Case Simulator.Engine.Mode
            Case EngineMode.AbortingStart, EngineMode.Stopping
                Result = CanStart()
            Case Else
                Result = False
        End Select
        Return Result
    End Function

    Private Function IsAbortingStart() As Boolean
        Return Simulator.Engine.Mode = EngineMode.Starting And CanShutdown()
    End Function

End Class
