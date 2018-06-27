Public Class TrentXWBFacilityStartSystemViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _IsStartAirValveOpened As Boolean
    Private _IsStartMasterOn As Boolean
    Private _IsIgnitionSystem1Commanded As Boolean
    Private _IsIgnitionSystem2Commanded As Boolean
    Private _IgnitionState As IgnitionState = IgnitionState.Off
    Private _ToggleStartMasterCommand As RelayCommand
    Private _ToggleIgnitorsCommand As RelayCommand

#End Region

#Region "Properties"

    Public ReadOnly Property ToggleStartMasterCommand As ICommand
        Get
            If _ToggleStartMasterCommand Is Nothing Then _ToggleStartMasterCommand = New RelayCommand(AddressOf ToggleStartMaster, AddressOf CanToggleStartMaster)
            Return _ToggleStartMasterCommand
        End Get
    End Property

    Public ReadOnly Property ToggleIgnitorsCommand As ICommand
        Get
            If _ToggleIgnitorsCommand Is Nothing Then _ToggleIgnitorsCommand = New RelayCommand(AddressOf ToggleIgnitors, AddressOf CanToggleIgnitors)
            Return _ToggleIgnitorsCommand
        End Get
    End Property

    Public Property IsStartAirValveOpened As Boolean
        Get
            Return _IsStartAirValveOpened
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsStartAirValveOpened, _IsStartAirValveOpened, value)
        End Set
    End Property

    Public Property IsStartMasterOn As Boolean
        Get
            Return _IsStartMasterOn
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsStartMasterOn, _IsStartMasterOn, value)
            MainViewModel.Instance.EngineStartShutdownAndEec.EngineStartSystem.StartSelector.RefreshCommands()
        End Set
    End Property

    Public Property IsIgnitionSystem1Commanded As Boolean
        Get
            Return _IsIgnitionSystem1Commanded
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsIgnitionSystem1Commanded, _IsIgnitionSystem1Commanded, value)
        End Set
    End Property

    Public Property IsIgnitionSystem2Commanded As Boolean
        Get
            Return _IsIgnitionSystem2Commanded
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsIgnitionSystem2Commanded, _IsIgnitionSystem2Commanded, value)
        End Set
    End Property

    Public Property IgnitionState As IgnitionState
        Get
            Return _IgnitionState
        End Get
        Set(value As IgnitionState)
            SetProperty(Function() IgnitionState, _IgnitionState, value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub Update(state As EnginePLCState)
        IsStartMasterOn = state.IsStartMasterOn
        IgnitionState = state.IgnitionState
        IsStartAirValveOpened = state.IsStartAirValveOpened
        IsIgnitionSystem1Commanded = state.IsIgnitor1Commanded
        IsIgnitionSystem2Commanded = state.IsIgnitor2Commanded
    End Sub

    Protected Overrides Sub RefreshCommandsSync()
        MyBase.RefreshCommandsSync()
        _ToggleStartMasterCommand.InvalidRequerySuggested()
    End Sub

#End Region

#Region "Command Methods"

    Private Function CanToggleStartMaster(obj As Object) As Boolean
        With MainViewModel.Instance.EngineStartShutdownAndEec.Hydraulics
            'Inverse result because HeaderedTwoState control uses True for StateOn (Left - Yes).
            'Return ((.IsHydraulicPump1Detected And .IsHydraulicPump1Fitted) And (.IsHydraulicPump2Detected And .IsHydraulicPump2Fitted)) Or _
            '((Not .IsHydraulicPump1Detected And Not .IsHydraulicPump1Fitted) And (Not .IsHydraulicPump2Detected And Not .IsHydraulicPump2Fitted))
        End With
        Return True
    End Function

    Private Sub ToggleStartMaster(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateStartMasterOnAsync(Not IsStartMasterOn)
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

    Private Function CanToggleIgnitors(obj As Object) As Boolean
        Return True
    End Function

    Private Sub ToggleIgnitors(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Select Case IgnitionState
                Case IgnitionState.Off
                    Client.UpdateIgnitionArmedAsync()
                Case Services.IgnitionState.Armed
                    Client.UpdateIgnitionOffAsync()
                Case Services.IgnitionState.On
                    Client.UpdateIgnitionOffAsync()
            End Select
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
