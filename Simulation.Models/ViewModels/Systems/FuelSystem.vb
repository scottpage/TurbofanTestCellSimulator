<DataContract()>
Public Class FuelSystem
    Implements IFuelSystem

    Public Sub New()
        Pressure = New Parameter
        Temperature = New Parameter
        Valve = New Valve
    End Sub

    <DataMember()>
    Public Property Pressure As Parameter Implements IFuelSystem.Pressure
    <DataMember()>
    Public Property Temperature As Parameter Implements IFuelSystem.Temperature
    <DataMember()>
    Public Property Valve As Valve Implements IFuelSystem.Valve

    Public Sub Update(value As Double) Implements IFuelSystem.Update
        Pressure.Update(value)
        Temperature.Update(value)
    End Sub

End Class
