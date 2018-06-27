<DataContract()>
Public Class OilSystem
    Implements IOilSystem

    Public Sub New()
        Pressure = New Parameter
        Quantity = New Parameter
        Temperature = New Parameter
    End Sub

    <DataMember()>
    Public Property Pressure As Parameter Implements IOilSystem.Pressure
    <DataMember()>
    Public Property Quantity As Parameter Implements IOilSystem.Quantity
    <DataMember()>
    Public Property Temperature As Parameter Implements IOilSystem.Temperature

    Public Sub Update(value As Double) Implements IOilSystem.Update
        Pressure.Update(value)
        Quantity.Update(value)
        Temperature.Update(value)
    End Sub

End Class
