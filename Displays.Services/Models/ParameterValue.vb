<DataContract(), KnownType(GetType(ShaftValue))>
Public Class ParameterValue

    <DataMember()>
    Public Property Value As Double
    <DataMember()>
    Public Property Average As Double
    <DataMember()>
    Public Property Quality As Quality
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
    Public Property LowCriticalAlarmLimit As Double

    Public Overridable Sub Update(parameter As ParameterValue)
        With parameter
            Name = .Name
            Value = .Value
            Average = .Average
            Quality = .Quality
            EngineeringUnits = .EngineeringUnits
            Version = .Version
            Minimum = .Minimum
            Maximum = .Maximum
            HighAlarmLimit = .HighAlarmLimit
            HighCriticalAlarmLimit = .HighCriticalAlarmLimit
            LowAlarmLimit = .LowAlarmLimit
            LowCriticalAlarmLimit = .LowCriticalAlarmLimit
        End With
    End Sub

End Class
