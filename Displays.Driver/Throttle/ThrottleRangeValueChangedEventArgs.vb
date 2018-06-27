Public Class ThrottleRangeValueChangedEventArgs
    Inherits EventArgs

    Private _Value As Double

    Public Sub New(value As Double)
        _Value = value
    End Sub

    Public ReadOnly Property Value As Double
        Get
            Return _Value
        End Get
    End Property

End Class
