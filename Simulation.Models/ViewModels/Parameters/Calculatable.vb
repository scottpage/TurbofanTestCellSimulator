Public Class Calculatable
    Inherits ViewModelBase
    Implements ICalculatable

    Public Property Bias As Double Implements ICalculatable.Bias

    Public Property IsNoiseEnabled As Boolean Implements ICalculatable.IsNoiseEnabled

    Public Property Maximum As Double Implements ICalculatable.Maximum

    Public Property Minimum As Double Implements ICalculatable.Minimum

    Public Property NoiseLevel As Double Implements ICalculatable.NoiseLevel

    Public Overridable Function Calculate(value As Double) As Double Implements ICalculatable.Calculate
        Return value
    End Function

End Class
