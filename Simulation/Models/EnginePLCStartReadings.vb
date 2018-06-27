Namespace SimulationStateService

    Public Class EnginePLCStartReadings

        Public Sub Clear()
            Me.AirIntakeTemperatureAtIdle = 0
            Me.FuelOn = 0
            Me.FuelOnSeconds = 0
            Me.HPRotation = 0
            Me.IPRotation = 0
            Me.LightupFuelFlow = 0
            Me.Lit = 0
            Me.LitSeconds = 0
            Me.LPRotation = 0
            Me.MaxTGT = 0
            Me.N2DeadCrankSeconds = 0
            Me.N2DeadCrankSpeed = 0
            Me.N3DeadCrankSeconds = 0
            Me.N3DeadCrankSpeed = 0
            Me.OilPressureAtIdle = 0
            Me.PrestartStartAirPressure = 0
            Me.PrestartTGT = 0
            Me.StarterCut = 0
            Me.StarterCutSeconds = 0
            Me.StartingStartAirPressure = 0
            Me.TimeToGI = 0
            Me.VibrationAtIdle = 0
        End Sub

    End Class

End Namespace