Public Interface IOrderedPolynomial
    Inherits ICalculatable

    Property Orders As List(Of PolynomialOrder)
    Sub Copy(polynomial As IOrderedPolynomial)

End Interface
