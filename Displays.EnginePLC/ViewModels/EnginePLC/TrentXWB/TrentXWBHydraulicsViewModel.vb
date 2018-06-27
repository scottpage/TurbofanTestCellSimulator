Public Class TrentXWBHydraulicsViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Private _IsHydraulicPump1NotFitted As Boolean = True
    Public Property IsHydraulicPump1NotFitted As Boolean
        Get
            Return _IsHydraulicPump1NotFitted
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsHydraulicPump1NotFitted, _IsHydraulicPump1NotFitted, Value)
            MainViewModel.Instance.EngineStartShutdownAndEec.FacilityStartSystem.RefreshCommands()
        End Set
    End Property

    Private _IsHydraulicPump2NotFitted As Boolean = True
    Public Property IsHydraulicPump2NotFitted As Boolean
        Get
            Return _IsHydraulicPump2NotFitted
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsHydraulicPump2NotFitted, _IsHydraulicPump2NotFitted, Value)
            MainViewModel.Instance.EngineStartShutdownAndEec.FacilityStartSystem.RefreshCommands()
        End Set
    End Property

    Private _IsHydraulicPump1Detected As Boolean
    Public Property IsHydraulicPump1Detected As Boolean
        Get
            Return _IsHydraulicPump1Detected
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsHydraulicPump1Detected, _IsHydraulicPump1Detected, Value)
            MainViewModel.Instance.EngineStartShutdownAndEec.FacilityStartSystem.RefreshCommands()
        End Set
    End Property

    Private _IsHydraulicPump2Detected As Boolean
    Public Property IsHydraulicPump2Detected As Boolean
        Get
            Return _IsHydraulicPump2Detected
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsHydraulicPump2Detected, _IsHydraulicPump2Detected, Value)
            MainViewModel.Instance.EngineStartShutdownAndEec.FacilityStartSystem.RefreshCommands()
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub Update(state As EnginePLCState)
        IsHydraulicPump1Detected = state.IsHydraulicPump1Detected
        IsHydraulicPump1NotFitted = state.IsHydraulicPump1NotFitted
        IsHydraulicPump2Detected = state.IsHydraulicPump2Detected
        IsHydraulicPump2NotFitted = state.IsHydraulicPump2NotFitted
    End Sub

#End Region

#Region "Commands"

    Private _ToggleIsHydraulicPump1FittedNotCommand As ICommand
    Public ReadOnly Property ToggleIsHydraulicPump1NotFittedCommand As ICommand
        Get
            If _ToggleIsHydraulicPump1FittedNotCommand Is Nothing Then _
               _ToggleIsHydraulicPump1FittedNotCommand = New RelayCommand(AddressOf ToggleIsHydraulicPump1NotFitted)
            Return _ToggleIsHydraulicPump1FittedNotCommand
        End Get
    End Property

    Private Sub ToggleIsHydraulicPump1NotFitted(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateHydraulicPump1FittedAsync(Not IsHydraulicPump1NotFitted)
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

    Private _ToggleIsHydraulicPump2NotFittedCommand As ICommand
    Public ReadOnly Property ToggleIsHydraulicPump2NotFittedCommand As ICommand
        Get
            If _ToggleIsHydraulicPump2NotFittedCommand Is Nothing Then _
               _ToggleIsHydraulicPump2NotFittedCommand = New RelayCommand(AddressOf ToggleIsHydraulicPump2NotFitted)
            Return _ToggleIsHydraulicPump2NotFittedCommand
        End Get
    End Property

    Private Sub ToggleIsHydraulicPump2NotFitted(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateHydraulicPump2FittedAsync(Not IsHydraulicPump2NotFitted)
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

#End Region

End Class
