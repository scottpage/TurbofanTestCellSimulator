Public Interface IParameter
    Inherits INotifyConfigurationChanged

    Property Name As String
    Property EngineeringUnits As String
    Property Value As IValue
    Property IsAlarming As Boolean
    Property Alarms As Alarms
    Property ConfigurationVersion As Integer
    Sub Update(value As Double)

End Interface
