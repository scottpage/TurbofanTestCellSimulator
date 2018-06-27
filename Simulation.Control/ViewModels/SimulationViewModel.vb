Public Class SimulationViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _ResetCommand As ICommand = New RelayCommand(AddressOf Reset, AddressOf CanReset)
    Private _EngineMode As EngineMode = EngineMode.None

#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Public Property EngineMode As EngineMode
        Get
            Return _EngineMode
        End Get
        Set(ByVal Value As EngineMode)
            SetProperty(Function() EngineMode, _EngineMode, Value)
        End Set
    End Property

#End Region

#Region "Methods"


#End Region

#Region "Commands"

#Region "Reset Command"

    Public ReadOnly Property ResetCommand As ICommand
        Get
            Return _ResetCommand
        End Get
    End Property

    Private Function CanReset(obj As Object) As Boolean
        Return True
    End Function

    Private Overloads Sub Reset(obj As Object)
    End Sub

#End Region

#End Region

End Class
