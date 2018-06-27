Public Interface ICalculatable

    Property Minimum As Double
    Property Maximum As Double
    Property Bias As Double
    Property NoiseLevel As Double
    Property IsNoiseEnabled As Boolean
    Function Calculate(value As Double) As Double

End Interface
