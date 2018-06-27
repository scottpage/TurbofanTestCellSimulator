Namespace SimulationStateService

    Public Class TrentXWBConfiguration
        Inherits TrentConfiguration

        <DataMember()>
        Public Property StarterPressure As New ParameterConfiguration

        Public Overrides Sub Update(engine As Simulation.Models.IEngine)
            MyBase.Update(engine)
            StarterPressure.Update(DirectCast(engine.Starters(0), PneumaticStarter).DeliveryPressure)
        End Sub

    End Class

End Namespace