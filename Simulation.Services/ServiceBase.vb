Public MustInherit Class ServiceBase

    Private _Simulator As Simulator

    Protected Sub New(simulator As Simulator)
        _Simulator = simulator
    End Sub

    Public ReadOnly Property Simulator As Simulator
        Get
            Return _Simulator
        End Get
    End Property

End Class
