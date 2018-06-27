Public Class CombinedParameterViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New(importParameter As ImportParameterViewModel, simulationParameter As IParameter)
        _ImportParameter = importParameter
        _SimulationParameter = simulationParameter
    End Sub

#End Region

#Region "Properties"

    Private _ImportParameter As ImportParameterViewModel
    Public ReadOnly Property ImportParameter As ImportParameterViewModel
        Get
            Return _ImportParameter
        End Get
    End Property

    Private _SimulationParameter As IParameter
    Public ReadOnly Property SimulationParameter As IParameter
        Get
            Return _SimulationParameter
        End Get
    End Property

#End Region

#Region "Commands"



#End Region

#Region "Methods"



#End Region

End Class
