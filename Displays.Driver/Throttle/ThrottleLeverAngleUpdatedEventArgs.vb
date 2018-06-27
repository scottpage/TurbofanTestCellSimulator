Public Class ThrottleLeverAngleUpdatedEventArgs
    Inherits EventArgs

    Public Sub New(leverAngle As Double)
        _LeverAngle = leverAngle
    End Sub

    Private _LeverAngle As Double
    Public ReadOnly Property LeverAngle As Double
        Get
            Return _LeverAngle
        End Get
    End Property

End Class
