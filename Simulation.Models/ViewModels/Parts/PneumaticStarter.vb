<DataContract()>
Public Class PneumaticStarter
    Inherits Starter
    Implements IPneumaticStarter

    Public Sub New()
        MyBase.New(StarterType.Pneumatic)
        DeliveryPressure = New Parameter
    End Sub

    <DataMember()>
    Public Property DeliveryPressure As Parameter Implements IPneumaticStarter.DeliveryPressure

    Public Overrides Function GetParameters() As System.Collections.Generic.IEnumerable(Of IParameter)
        Return New IParameter() {DeliveryPressure}
    End Function

    Public Overrides Sub Update(value As Double)
        DeliveryPressure.Update(value)
    End Sub

End Class
