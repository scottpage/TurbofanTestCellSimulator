Public Class SystemStatusViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _EngineType As String
    Private _EngineSerialNumber As String
    Private _EngineBuild As String
    Private _EnginePartTest As String

#End Region

#Region "Properties"

    Public ReadOnly Property ExitCommand As ICommand
        Get
            Return MainViewModel.Instance.ExitCommand
        End Get
    End Property

    Public Property EngineType As String
        Get
            Return _EngineType
        End Get
        Set(value As String)
            SetProperty(Function() EngineType, _EngineType, value)
        End Set
    End Property

    Public Property EngineSerialNumber As String
        Get
            Return _EngineSerialNumber
        End Get
        Set(value As String)
            SetProperty(Function() EngineSerialNumber, _EngineSerialNumber, value)
        End Set
    End Property

    Public Property EngineBuild As String
        Get
            Return _EngineBuild
        End Get
        Set(value As String)
            SetProperty(Function() EngineBuild, _EngineBuild, value)
        End Set
    End Property

    Public Property EnginePartTest As String
        Get
            Return _EnginePartTest
        End Get
        Set(value As String)
            SetProperty(Function() EnginePartTest, _EnginePartTest, value)
        End Set
    End Property

#End Region

#Region "Update"

    Public Sub Update(state As SimulationState)
        EngineBuild = state.EngineBuild
        EnginePartTest = state.EnginePartTest
        EngineSerialNumber = state.EngineSerialNumber
        EngineType = state.EngineType
    End Sub

#End Region

End Class
