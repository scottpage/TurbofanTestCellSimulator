Imports System.Windows.Threading

Public Class MainViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private Shared _Instance As MainViewModel
    Private _Simulator As Simulator
    Private _Services As ServiceControllerViewModel
    Private _ThrottleProfile As ThrottleProfileViewModel
    Private _ResetSimulationCommand As ICommand = New RelayCommand(AddressOf Reset)

#End Region

#Region "Constructors"

    Private Sub New()
        _Simulator = New Simulator
        _Services = New ServiceControllerViewModel(_Simulator)
        '_ThrottleProfile = New ThrottleProfileViewModel
        '_ThrottleProfile.StartUpdating()
    End Sub

#End Region

#Region "Properties"

    Public Shared ReadOnly Property Instance As MainViewModel
        Get
            If _Instance Is Nothing Then _Instance = New MainViewModel
            Return _Instance
        End Get
    End Property

    Public ReadOnly Property Simulator As Simulator
        Get
            Return _Simulator
        End Get
    End Property

    Public ReadOnly Property ResetSimulationCommand As ICommand
        Get
            Return _ResetSimulationCommand
        End Get
    End Property

    Public ReadOnly Property Services As ServiceControllerViewModel
        Get
            Return _Services
        End Get
    End Property

    Public ReadOnly Property ThrottleProfile As ThrottleProfileViewModel
        Get
            Return _ThrottleProfile
        End Get
    End Property

#End Region

#Region "Commands"

#Region "ImportLogCommand"

    Private _ImportLogWindow As TabFileImportWindow

    Private _ImportLogCommand As RelayCommand
    Public ReadOnly Property ImportLogCommand As ICommand
        Get
            If _ImportLogCommand Is Nothing Then _
               _ImportLogCommand = New RelayCommand(AddressOf ImportLog, AddressOf CanImportLog)
            Return _ImportLogCommand
        End Get
    End Property

    Private Function CanImportLog(obj As Object) As Boolean
        Return _ImportLogWindow Is Nothing
    End Function

    Private Sub ImportLog(obj As Object)
        _ImportLogWindow = New TabFileImportWindow
        _ImportLogWindow.DataContext = New TabFileImportViewModel
        AddHandler _ImportLogWindow.Closed, AddressOf ImportLogWindow_Closed
        _ImportLogWindow.Show()
    End Sub

    Private Sub ImportLogWindow_Closed(sender As Object, e As EventArgs)
        _ImportLogWindow = Nothing
        _ImportLogCommand.InvalidRequerySuggested()
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Overloads Sub Reset(obj As Object)
        Reset()
    End Sub

    Public Overrides Sub Reset()
        Dim Client As ControlServiceClient = Nothing
        Try
            Client = New ControlServiceClient("EnginePLCControlServiceClient")
            Client.ResetAsync()
            Client.Close()
            Client = New ControlServiceClient("DriverControlServiceClient")
            Client.ResetAsync()
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
        Simulator.Reset()
    End Sub

#End Region

End Class
