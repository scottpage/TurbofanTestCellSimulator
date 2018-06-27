Public Class EngineModeTransitionPlayback

    Private _Values As IEnumerable(Of Double)

    Public Sub New(values As IEnumerable(Of Double))
        _Values = values
    End Sub

    Public Sub New(fromPolynomial As OrderedPolynomial, toPolynomial As OrderedPolynomial, interval As Integer, duration As Integer)
        GenerateValues(fromPolynomial, toPolynomial, interval, duration)
    End Sub

    Public ReadOnly Property Values As IEnumerable(Of Double)
        Get
            Return _Values
        End Get
    End Property

    Private Sub GenerateValues(fromPolynomial As OrderedPolynomial, toPolynomial As OrderedPolynomial, interval As Integer, duration As Integer)
        For Position = 0 To duration Step interval / 100


        Next
    End Sub

End Class
