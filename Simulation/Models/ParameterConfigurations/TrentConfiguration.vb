Namespace SimulationStateService

    Public Class TrentConfiguration
        Inherits EngineConfiguration

        <DataMember()>
        Public Property Shaft3 As New ShaftConfiguration
        <DataMember()>
        Public Property Shaft3LaneAVibration As New ParameterConfiguration
        <DataMember()>
        Public Property Shaft3LaneBVibration As New ParameterConfiguration

        Public Overrides Sub Update(engine As Simulation.Models.IEngine)
            MyBase.Update(engine)
            Shaft3.Update(engine.Shafts.First(Function(s) s.Number.Equals(3)))
            Shaft3LaneAVibration.Update(engine.Shafts.First(Function(s) s.Number.Equals(3)).LaneATrackedOrderVibration)
            Shaft3LaneBVibration.Update(engine.Shafts.First(Function(s) s.Number.Equals(3)).LaneBTrackedOrderVibration)
        End Sub

    End Class

End Namespace