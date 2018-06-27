Namespace SimulationStateService

    Public Class ShaftConfiguration
        Inherits ParameterConfiguration

        Public Sub New()
        End Sub

        Public Sub New(shaft As Shaft)
            MyBase.New(shaft)
        End Sub

        Public Property Number As Integer

        Public Overrides Sub Update(parameter As Simulation.Models.IParameter)
            Dim Shaft = DirectCast(parameter, Shaft)
            Number = Shaft.Number
            MyBase.Update(parameter)
        End Sub

    End Class

End Namespace