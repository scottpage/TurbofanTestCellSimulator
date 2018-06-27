Public Class SimulationStateUpdatedEventArgs
    Inherits EventArgs

    Private _State As SimulationState

    Public Sub New(state As SimulationState)
        _State = state
    End Sub

    Public ReadOnly Property State As SimulationState
        Get
            Return _State
        End Get
    End Property

End Class
