Imports System.Windows.Threading

Public Class MainViewModel
    Inherits RealTimeDisplayViewModel

#Region "Fields"

    Private Shared _Instance As MainViewModel
    Private _StartReadings As New StartReadingsViewModel
    Private _StopReadings As New StopReadingsViewModel
    Private _SystemStatus As New SystemStatusViewModel
    Private _EngineMode As EngineMode = EngineMode.None

#End Region

#Region "Constructors"

    Private Sub New()
    End Sub

#End Region

#Region "Properties"

    Public Shared ReadOnly Property Instance As MainViewModel
        Get
            If _Instance Is Nothing Then _Instance = New MainViewModel
            Return _Instance
        End Get
    End Property

    Public ReadOnly Property StartReadings As StartReadingsViewModel
        Get
            Return _StartReadings
        End Get
    End Property

    Public ReadOnly Property StopReadings As StopReadingsViewModel
        Get
            Return _StopReadings
        End Get
    End Property

    Public ReadOnly Property SystemStatus As SystemStatusViewModel
        Get
            Return _SystemStatus
        End Get
    End Property

    Private ReadOnly _engineStartShutdownAndEec As New TrentXWBEngineStartShutdownAndEECViewModel
    Public ReadOnly Property EngineStartShutdownAndEec As TrentXWBEngineStartShutdownAndEECViewModel
        Get
            Return _engineStartShutdownAndEec
        End Get
    End Property

    Private _MasterWarningPage As New TrentXWBEIFStatusViewModel
    Public ReadOnly Property MasterWarningPage As TrentXWBEIFStatusViewModel
        Get
            Return _MasterWarningPage
        End Get
    End Property

    Public Property EngineMode As EngineMode
        Get
            Return _EngineMode
        End Get
        Set(ByVal Value As EngineMode)
            SetProperty(Function() EngineMode, _EngineMode, Value)
        End Set
    End Property

    Private _IsMasterCautionActive As Boolean
    Public Property IsMasterCautionActive As Boolean
        Get
            Return _IsMasterCautionActive
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsMasterCautionActive, _IsMasterCautionActive, value)
        End Set
    End Property

    Private _IsMasterWarningActive As Boolean
    Public Property IsMasterWarningActive As Boolean
        Get
            Return _IsMasterWarningActive
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsMasterWarningActive, _IsMasterWarningActive, value)
        End Set
    End Property

    Private _CurrentPage As ViewModelBase
    Public Property CurrentPage As ViewModelBase
        Get
            Return _CurrentPage
        End Get
        Set(value As ViewModelBase)
            SetProperty(Function() CurrentPage, _CurrentPage, value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Update(state As SimulationState)
        EngineMode = state.EngineMode
        EngineStartShutdownAndEec.Update(state)
        SystemStatus.Update(state)
        StartReadings.Update(state.EnginePLC.StartReadings)
        StopReadings.Update(state.EnginePLC.StopReadings)
        IsMasterCautionActive = state.EnginePLC.IsMasterCautionActive
        IsMasterWarningActive = state.EnginePLC.IsMasterWarningActive
    End Sub

    Public Sub HideStartAndShutdownReadings()
        StartReadings.Visibility = Visibility.Hidden
        StopReadings.Visibility = Visibility.Hidden
    End Sub

#End Region

#Region "Commands"

#Region "Exit application command"

    Private _ExitCommand As ICommand
    Public ReadOnly Property ExitCommand As ICommand
        Get
            If _ExitCommand Is Nothing Then _ExitCommand = New RelayCommand(AddressOf ExitApplication)
            Return _ExitCommand
        End Get
    End Property

    Private Sub ExitApplication(obj As Object)
        Application.Current.Shutdown()
    End Sub

#End Region

#Region "Select start, shutdown, and EEC page command"

    Private _SelectEngineStartShutdownAndEECPageCommand As ICommand
    Public ReadOnly Property SelectEngineStartShutdownAndEECPageCommand As ICommand
        Get
            If _SelectEngineStartShutdownAndEECPageCommand Is Nothing Then _
               _SelectEngineStartShutdownAndEECPageCommand = New RelayCommand(AddressOf SelectEngineStartShutdownAndEECPage, AddressOf CanSelectEngineStartShutdownAndEECPage)
            Return _SelectEngineStartShutdownAndEECPageCommand
        End Get
    End Property

    Private Function CanSelectEngineStartShutdownAndEECPage(obj As Object) As Boolean
        Return CurrentPage IsNot EngineStartShutdownAndEEC
    End Function

    Private Sub SelectEngineStartShutdownAndEECPage(obj As Object)
        CurrentPage = EngineStartShutdownAndEEC
    End Sub

#End Region

#Region "Select master warning page command"

    Private _SelectMasterWarningPageCommand As ICommand
    Public ReadOnly Property SelectMasterWarningPageCommand As ICommand
        Get
            If _SelectMasterWarningPageCommand Is Nothing Then _
               _SelectMasterWarningPageCommand = New RelayCommand(AddressOf SelectMasterWarningPage, AddressOf CanSelectMasterWarningPage)
            Return _SelectMasterWarningPageCommand
        End Get
    End Property

    Private Function CanSelectMasterWarningPage(obj As Object) As Boolean
        Return CurrentPage IsNot MasterWarningPage
    End Function

    Private Sub SelectMasterWarningPage(obj As Object)
        HideStartAndShutdownReadings()
        CurrentPage = MasterWarningPage
    End Sub

#End Region

#End Region

End Class
