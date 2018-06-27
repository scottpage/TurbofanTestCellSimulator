Public Class PolynomialOrder
    Implements IPolynomialOrder

    Public Sub New(order As Double, power As Double)
        _Order = order
        _Power = power
    End Sub

    <DataMember()>
    Public Property Order As Double Implements IPolynomialOrder.Order
    <DataMember()>
    Public Property Power As Double Implements IPolynomialOrder.Power

    Public Function Calculate(value As Double) As Double Implements IPolynomialOrder.Calculate
        If Double.IsNaN(Order) Or Order.Equals(0.0#) Then Return value
        Return Order * value ^ Power
    End Function

End Class
