Namespace SimulationStateService

    Public Class TrentXWBParameters
        Inherits TrentParameters

        Public Sub New()
            StarterPressure = New ParameterValue
        End Sub

        Public Overrides Sub Update(engine As Simulation.Models.IEngine)
            MyBase.Update(engine)
            StarterPressure.Update(DirectCast(engine.Starters(0), PneumaticStarter).DeliveryPressure)
        End Sub

    End Class

End Namespace