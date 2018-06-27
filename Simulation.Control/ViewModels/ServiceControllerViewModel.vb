Public Class ServiceControllerViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _Simulator As Simulator
    Private _Services As New ObservableCollection(Of ServiceViewModel)
    Private _SelectedService As ServiceViewModel
    Private _AlarmService As ServiceViewModel
    Private _EnginePLCService As ServiceViewModel
    Private _ManagementService As ServiceViewModel
    Private _ThrottleService As ServiceViewModel
    Private _StartAllCommand As New RelayCommand(AddressOf StartAll)
    Private _StopAllCommand As New RelayCommand(AddressOf StopAll)

#End Region

#Region "Constructors"

    Public Sub New(simulator As Simulator)
        _Simulator = simulator
        _AlarmService = New ServiceViewModel("Alarms", New AlarmService(simulator))
        _EnginePLCService = New ServiceViewModel("Engine PLC", New EnginePLCService(simulator))
        _ManagementService = New ServiceViewModel("Management (Bookman)", New ManagementService(simulator))
        _ThrottleService = New ServiceViewModel("Throttle", New ThrottleService(simulator))
        _Services.Add(_AlarmService)
        _Services.Add(_EnginePLCService)
        _Services.Add(_ManagementService)
        _Services.Add(_ThrottleService)
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property StartAllCommand As ICommand
        Get
            Return _StartAllCommand
        End Get
    End Property

    Public ReadOnly Property StopAllCommand As ICommand
        Get
            Return _StopAllCommand
        End Get
    End Property

    Public ReadOnly Property Services As ObservableCollection(Of ServiceViewModel)
        Get
            Return _Services
        End Get
    End Property

    Public Property SelectedService As ServiceViewModel
        Get
            Return _SelectedService
        End Get
        Set(ByVal Value As ServiceViewModel)
            SetProperty(Function() SelectedService, _SelectedService, Value)
        End Set
    End Property

    Public ReadOnly Property AlarmService As ServiceViewModel
        Get
            Return _AlarmService
        End Get
    End Property

    Public ReadOnly Property EnginePLCService As ServiceViewModel
        Get
            Return _EnginePLCService
        End Get
    End Property

    Public ReadOnly Property ManagementService As ServiceViewModel
        Get
            Return _ManagementService
        End Get
    End Property

    Public ReadOnly Property ThrottleService As ServiceViewModel
        Get
            Return _ThrottleService
        End Get
    End Property

#End Region

#Region "Methods"

    Private Sub StartAll(obj As Object)
        StartAll()
    End Sub

    Private Sub StopAll(obj As Object)
        StopAll()
    End Sub

    Public Sub StartAll()
        Services.ToList.ForEach(Sub(s) If s.StartCommand.CanExecute(Nothing) Then s.StartCommand.Execute(Nothing))
    End Sub

    Public Sub StopAll()
        Services.ToList.ForEach(Sub(s) If s.StopCommand.CanExecute(Nothing) Then s.StopCommand.Execute(Nothing))
    End Sub

#End Region

End Class
