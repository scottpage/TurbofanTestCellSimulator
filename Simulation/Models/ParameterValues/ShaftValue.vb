Namespace SimulationStateService

    Public Class ShaftValue
        Inherits ParameterValue

        Public Sub New()
        End Sub

        Public Sub New(shaft As Shaft)
            MyBase.New(shaft)
            Update(shaft)
        End Sub

        Public Overrides Sub Update(parameter As Simulation.Models.IParameter)
            Dim Shaft = DirectCast(parameter, Shaft)
            Number = Shaft.Number
            CorrectedSpeed = Shaft.CorrectedSpeed
            CorrectedSpeedRoot20 = Shaft.CorrectedSpeedRoot20
            MyBase.Update(parameter)
        End Sub

    End Class

End Namespace