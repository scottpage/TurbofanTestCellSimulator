Public Class ThrottleConnectFailedEventArgs
    Inherits EventArgs

    Dim _Exception As Exception

    Public Sub New()
    End Sub

    Public Sub New(ex As Exception)
        _Exception = ex
    End Sub

    Public ReadOnly Property Exception As Exception
        Get
            Return _Exception
        End Get
    End Property

End Class
