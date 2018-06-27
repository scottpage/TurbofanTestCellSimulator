Public Class TrentXWBEngineStartSystemSelectorViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _State As StartSelectorState = StartSelectorState.Normal
    Private _CrankCommand As RelayCommand
    Private _NormalCommand As RelayCommand
    Private _IgnitionStartCommand As RelayCommand

#End Region

#Region "Properties"

    Public Property State As StartSelectorState
        Get
            Return _State
        End Get
        Set(value As StartSelectorState)
            SetProperty(Function() State, _State, value)
            CommandManager.InvalidateRequerySuggested()
        End Set
    End Property

    Public ReadOnly Property CrankCommand As ICommand
        Get
            If _CrankCommand Is Nothing Then _CrankCommand = New RelayCommand(AddressOf Crank, AddressOf CanCrank)
            Return _CrankCommand
        End Get
    End Property

    Public ReadOnly Property NormalCommand As ICommand
        Get
            If _NormalCommand Is Nothing Then _NormalCommand = New RelayCommand(AddressOf Normal)
            Return _NormalCommand
        End Get
    End Property

    Public ReadOnly Property IgnitionStartCommand As ICommand
        Get
            If _IgnitionStartCommand Is Nothing Then _IgnitionStartCommand = New RelayCommand(AddressOf IgnitionStart, AddressOf CanIgnitionStart)
            Return _IgnitionStartCommand
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub Update(state As EnginePLCState)
        Me.State = state.StartSelectorState
    End Sub

    Protected Overrides Sub RefreshCommandsSync()
        _CrankCommand.InvalidRequerySuggested()
        _IgnitionStartCommand.InvalidRequerySuggested()
        _NormalCommand.InvalidRequerySuggested()
        MyBase.RefreshCommandsSync()
    End Sub

#End Region

#Region "Command Methods"

    Private Function CanCrank(obj As Object) As Boolean
        Return MainViewModel.Instance.EngineStartShutdownAndEec.FacilityStartSystem.IsStartMasterOn And Not State = StartSelectorState.IgnitionStart
    End Function

    Private Sub Crank(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateStartSelectorCrankAsync()
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

    Private Sub Normal(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateStartSelectorNormalAsync()
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

    Private Function CanIgnitionStart(obj As Object) As Boolean
        Return MainViewModel.Instance.EngineStartShutdownAndEec.FacilityStartSystem.IsStartMasterOn And Not State = StartSelectorState.Crank
    End Function

    Private Sub IgnitionStart(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateStartSelectorIgnitionStartAsync()
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
