Namespace SimulationStateService

    Public Class SimulationState

        Public Sub New()
            Me.AirInletTemperature = 23.5
            Me.ConfigurationVersion = Integer.MinValue
            Me.EngineMode = SimulationStateService.EngineMode.None
            Me.EnginePLC = New EnginePLCState
            Me.Parameters = New TrentXWBParameters
        End Sub

    End Class

End Namespace