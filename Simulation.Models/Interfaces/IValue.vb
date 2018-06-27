Public Interface IValue
    Inherits INotifyConfigurationChanged

    Property Current As Double
    ReadOnly Property Average As Double
    ReadOnly Property StandardDeviation As Double
    Property StabilityThreshold As Double
    ReadOnly Property IsStable As Boolean
    Property Quality As Quality

    Property Minimum As Double
    Property Maximum As Double

    ReadOnly Property IsRedirected As Boolean
    Property RedirectedValue As Double

    Property Mode As EngineMode

    Property UsePolynomials As Boolean
    Property StoppedPolynomial As OrderedPolynomial
    Property DryCrankPolynomial As OrderedPolynomial
    Property WetCrankPolynomial As OrderedPolynomial
    Property StartPolynomial As OrderedPolynomial
    Property RunningPolynomial As OrderedPolynomial
    Property ShutdownPolynomial As OrderedPolynomial
    Property NumberOfSamples As Integer

    Sub Update(value As Double)
    Sub Copy(value As IValue)

End Interface
