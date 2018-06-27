Namespace SimulationStateService

    Public Class AlarmValue

        Public Sub New(parameter As IParameter, alarm As Alarm)
            ParameterName = parameter.Name
            CurrentValue = parameter.Value.Average
            IsAcknowledged = alarm.IsAcknowledged
            Limit = alarm.Limit
            MinimumMaximum = alarm.MinimumMaximum
            Select Case alarm.Type
                Case Models.AlarmType.High
                    Type = AlarmType.High
                Case Models.AlarmType.HighCritical
                    Type = AlarmType.HighCritical
                Case Models.AlarmType.Low
                    Type = AlarmType.Low
                Case Models.AlarmType.LowCritical
                    Type = AlarmType.LowCritical
            End Select
        End Sub

    End Class

End Namespace