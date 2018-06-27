Public Class ServiceViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _Name As String
    Private _Service As ServiceBase
    Private _Host As ServiceHost
    Private _StartCommand As New RelayCommand(AddressOf Start, AddressOf CanStart)
    Private _StopCommand As New RelayCommand(AddressOf [Stop], AddressOf CanStop)

#End Region

#Region "Constructors"

    Public Sub New(name As String, service As ServiceBase)
        _Name = name
        _Service = service
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property StartCommand As ICommand
        Get
            Return _StartCommand
        End Get
    End Property

    Public ReadOnly Property StopCommand As ICommand
        Get
            Return _StopCommand
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    Private _IsStarted As Boolean
    Public Property IsStarted As Boolean
        Get
            Return _IsStarted
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsStarted, _IsStarted, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Private Function CanStart(obj As Object) As Boolean
        Return Not IsStarted
    End Function

    Private Sub Start(obj As Object)
        _Host = New ServiceHost(_Service)
        Try
            _Host.Open()
            IsStarted = True
        Catch ex As Exception
            If Not _Host.State = CommunicationState.Closed Then _Host.Abort()
        End Try
    End Sub

    Private Function CanStop(obj As Object) As Boolean
        Return IsStarted
    End Function

    Private Sub [Stop](obj As Object)
        _Host = New ServiceHost(_Service)
        Try
            _Host.Close()
            IsStarted = False
        Catch ex As Exception
            If Not _Host.State = CommunicationState.Closed Then _Host.Abort()
        End Try
    End Sub

#End Region

End Class
