Public Class TrentXWBEECFunctionsViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _IsInFlight As Boolean
    Private _IsHighIdle As Boolean
    Private _IsGroundPowerOn As Boolean
    Private _IsEMUPowerOn As Boolean
    Private _ToggleFlightStateCommand As RelayCommand
    Private _ToggleIdleStateCommand As RelayCommand
    Private _ToggleGroundPowerCommand As RelayCommand
    Private _ToggleEMUPowerCommand As RelayCommand

#End Region

#Region "Properties"

    Public ReadOnly Property ToggleFlightStateCommand As RelayCommand
        Get
            If _ToggleFlightStateCommand Is Nothing Then _ToggleFlightStateCommand = New RelayCommand(AddressOf ToggleFlightState)
            Return _ToggleFlightStateCommand
        End Get
    End Property

    Public ReadOnly Property ToggleIdleStateCommand As RelayCommand
        Get
            If _ToggleIdleStateCommand Is Nothing Then _ToggleIdleStateCommand = New RelayCommand(AddressOf ToggleIdleState, AddressOf CanToggleIdleState)
            Return _ToggleIdleStateCommand
        End Get
    End Property

    Public ReadOnly Property ToggleGroundPowerCommand As RelayCommand
        Get
            If _ToggleGroundPowerCommand Is Nothing Then _ToggleGroundPowerCommand = New RelayCommand(AddressOf ToggleGroundPower)
            Return _ToggleGroundPowerCommand
        End Get
    End Property

    Public ReadOnly Property ToggleEMUPowerCommand As RelayCommand
        Get
            If _ToggleEMUPowerCommand Is Nothing Then _ToggleEMUPowerCommand = New RelayCommand(AddressOf ToggleEMUPower)
            Return _ToggleEMUPowerCommand
        End Get
    End Property

    Public Property IsInFlight As Boolean
        Get
            Return _IsInFlight
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsInFlight, _IsInFlight, value)
            ToggleIdleStateCommand.InvalidRequerySuggested()
        End Set
    End Property

    Public Property IsHighIdle As Boolean
        Get
            Return _IsHighIdle
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsHighIdle, _IsHighIdle, value)
        End Set
    End Property

    Public Property IsGroundPowerOn As Boolean
        Get
            Return _IsGroundPowerOn
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsGroundPowerOn, _IsGroundPowerOn, value)
        End Set
    End Property

    Public Property IsEMUPowerOn As Boolean
        Get
            Return _IsEMUPowerOn
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsEMUPowerOn, _IsEMUPowerOn, value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub Update(state As EnginePLCState)
        IsInFlight = state.IsInFlight
        IsHighIdle = state.IsHighIdleSelected
        IsGroundPowerOn = state.IsGroundPowerOn
        IsEMUPowerOn = state.IsEMUPowerOn
    End Sub

#End Region

#Region "Command Methods"

    Private Function CanToggleIdleState(obj As Object) As Boolean
        Return IsInFlight
    End Function

    Private Sub ToggleIdleState(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateHighIdleAsync(Not IsHighIdle)
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        Finally
            Client.Close()
        End Try
    End Sub

    Private Sub ToggleFlightState(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateIsInFlightAsync(Not IsInFlight)
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        Finally
            Client.Close()
        End Try
    End Sub

    Private Sub ToggleGroundPower(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateGroundPowerOnAsync(Not IsGroundPowerOn)
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        Finally
            Client.Close()
        End Try
    End Sub

    Private Sub ToggleEMUPower(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateEMUPowerOn(Not IsEMUPowerOn)
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        Finally
            Client.Close()
        End Try
    End Sub

#End Region

End Class
