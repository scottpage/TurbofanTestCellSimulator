Namespace SimulationStateService

    Public Class ParameterValue

        Public Sub New()
        End Sub

        Public Sub New(parameter As IParameter)
            Me.New()
            Update(parameter)
        End Sub

        Public Overridable Sub Update(parameter As IParameter)
            With parameter
                Name = .Name
                EngineeringUnits = parameter.EngineeringUnits
                Minimum = .Value.Minimum
                Maximum = .Value.Maximum
                HighAlarmLimit = parameter.Alarms.High.Limit
                HighCriticalAlarmLimit = parameter.Alarms.HighCritical.Limit
                LowAlarmLimit = parameter.Alarms.Low.Limit
                LowCriticalAlarmLimit = parameter.Alarms.LowCritical.Limit
                Quality = SimulationConverter.ToSimulationQuality(parameter.Value.Quality)
                Value = .Value.Current
                Average = .Value.Average
                Version = .ConfigurationVersion
                Select Case .Value.Quality
                    Case Models.Quality.Bad
                        Quality = SimulationStateService.Quality.Bad
                    Case Models.Quality.Good
                        Quality = SimulationStateService.Quality.Good
                    Case Models.Quality.Suspect
                        Quality = SimulationStateService.Quality.Suspect
                End Select
            End With
        End Sub

    End Class

End Namespace