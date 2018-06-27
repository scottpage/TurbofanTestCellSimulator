Namespace SimulationStateService

    Partial Public Class ParameterConfiguration

        Public Sub New()
        End Sub

        Public Sub New(parameter As IParameter)
            Initialize(parameter)
        End Sub

        <DataMember()>
        Public Property Name As String
        <DataMember()>
        Public Property EngineeringUnits As String
        <DataMember()>
        Public Property Version As Integer
        <DataMember()>
        Public Property Minimum As Double
        <DataMember()>
        Public Property Maximum As Double
        <DataMember()>
        Public Property HighAlarmLimit As Double
        <DataMember()>
        Public Property HighCriticalAlarmLimit As Double
        <DataMember()>
        Public Property LowAlarmLimit As Double
        <DataMember()>
        Public Property LowCriticalLimit As Double

        Private Sub Initialize(parameter As IParameter)
            Name = parameter.Name
            EngineeringUnits = parameter.EngineeringUnits
            Version = parameter.ConfigurationVersion
            Minimum = parameter.Value.Minimum
            Maximum = parameter.Value.Maximum
            HighAlarmLimit = parameter.Alarms.High.Limit
            HighCriticalAlarmLimit = parameter.Alarms.HighCritical.Limit
            LowAlarmLimit = parameter.Alarms.Low.Limit
            LowCriticalLimit = parameter.Alarms.LowCritical.Limit
        End Sub

        Public Overridable Sub Update(parameter As IParameter)
            Name = parameter.Name
            EngineeringUnits = parameter.EngineeringUnits
            Version = parameter.ConfigurationVersion
            Minimum = parameter.Value.Minimum
            Maximum = parameter.Value.Maximum
            HighAlarmLimit = parameter.Alarms.High.Limit
            HighCriticalAlarmLimit = parameter.Alarms.HighCritical.Limit
            LowAlarmLimit = parameter.Alarms.Low.Limit
            LowCriticalLimit = parameter.Alarms.LowCritical.Limit
        End Sub

    End Class

End Namespace