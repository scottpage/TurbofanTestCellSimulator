<DataContract()>
Public Class OrderedPolynomial
    Inherits ViewModelBase
    Implements IOrderedPolynomial

    Public Sub New()
        IsNoiseEnabled = True
        Maximum = 99999.0#
        Minimum = -99999.0#
        Bias = 0
        Orders = New List(Of PolynomialOrder)
    End Sub

    <DataMember()>
    Public Property Bias As Double Implements IOrderedPolynomial.Bias
    <DataMember()>
    Public Property Maximum As Double Implements IOrderedPolynomial.Maximum
    <DataMember()>
    Public Property Minimum As Double Implements IOrderedPolynomial.Minimum
    <DataMember()>
    Public Property IsNoiseEnabled As Boolean Implements IOrderedPolynomial.IsNoiseEnabled

    Property _NoiseLevel As Double = 0.1
    <DataMember()>
    Public Property NoiseLevel As Double Implements IOrderedPolynomial.NoiseLevel
        Get
            Return _NoiseLevel
        End Get
        Set(value As Double)
            SetProperty(Function() NoiseLevel, _NoiseLevel, value)
        End Set
    End Property

    <DataMember()>
    Public Property Orders As List(Of PolynomialOrder) Implements IOrderedPolynomial.Orders

    Public Function Calculate(value As Double) As Double Implements IOrderedPolynomial.Calculate
        Dim Result As Double = Bias
        Orders.ForEach(Sub(o) Result += o.Calculate(value))
        If Result <= Minimum Then Return Minimum
        If Result >= Maximum Then Return Maximum
        Return Result
    End Function

    Public Sub AddOrder(order As Double, power As Double)
        Orders.Add(New PolynomialOrder(order, power))
    End Sub

    Public Sub Copy(polynomial As IOrderedPolynomial) Implements IOrderedPolynomial.Copy
        With polynomial
            IsNoiseEnabled = .IsNoiseEnabled
            Bias = .Bias
            Maximum = .Maximum
            Minimum = .Minimum
            NoiseLevel = .NoiseLevel
            Orders.Clear()
            .Orders.ForEach(Sub(o) Orders.Add(o))
        End With
    End Sub

End Class
