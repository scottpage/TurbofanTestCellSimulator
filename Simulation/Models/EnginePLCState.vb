Namespace SimulationStateService

    Public Class EnginePLCState

        Public Sub New()
            IgnitionState = SimulationStateService.IgnitionState.Off
            IsEMUPowerOn = False
            IsFuelOn = False
            IsGroundPowerOn = False
            IsHighIdleSelected = False
            IsHydraulicPump1Detected = False
            IsHydraulicPump1NotFitted = False
            IsHydraulicPump2Detected = False
            IsHydraulicPump2NotFitted = False
            IsIgnitor1Commanded = False
            IsIgnitor2Commanded = False
            IsInFlight = False
            IsManualStartOn = False
            IsStartAirValveOpened = False
            IsStartMasterOn = False
            IsMasterCautionActive = False
            IsMasterWarningActive = False
            StartReadings = New EnginePLCStartReadings
            StopReadings = New EnginePLCStopReadings
            StartSelectorState = SimulationStateService.StartSelectorState.Normal
        End Sub

    End Class

End Namespace