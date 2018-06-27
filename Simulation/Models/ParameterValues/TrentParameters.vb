Namespace SimulationStateService

    Public Class TrentParameters
        Inherits SimulationParameters

        Public Sub New()
            Shaft3 = New ShaftValue
            Shaft3LaneAVibration = New ParameterValue
            Shaft3LaneBVibration = New ParameterValue
        End Sub

        Public Overrides Sub Update(engine As Simulation.Models.IEngine)
            MyBase.Update(engine)
            Dim EngineShaft3 = engine.Shafts.First(Function(s) s.Number.Equals(3))
            Shaft3.Update(EngineShaft3)
            Shaft3LaneAVibration.Update(EngineShaft3.LaneATrackedOrderVibration)
            Shaft3LaneBVibration.Update(EngineShaft3.LaneBTrackedOrderVibration)
        End Sub

    End Class

End Namespace