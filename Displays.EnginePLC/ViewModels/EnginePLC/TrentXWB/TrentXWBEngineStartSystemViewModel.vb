Imports System.Windows.Threading

Public Class TrentXWBEngineStartSystemViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _IsFuelValveOpened As Boolean
    Private _IsManualStartOn As Boolean
    Private _WasFuelValveOpenedWithMasterStartOff As Boolean
    Private _ToggleFuelValveCommand As ICommand
    Private _ToggleManualStartCommand As ICommand
    Private _StartSelector As New TrentXWBEngineStartSystemSelectorViewModel

#End Region

#Region "Properties"

    Public ReadOnly Property ToggleFuelValveCommand As ICommand
        Get
            If _ToggleFuelValveCommand Is Nothing Then _ToggleFuelValveCommand = New RelayCommand(AddressOf ToggleFuelValve, AddressOf CanToggleFuelValve)
            Return _ToggleFuelValveCommand
        End Get
    End Property

    Public ReadOnly Property ToggleManualStartCommand As ICommand
        Get
            If _ToggleManualStartCommand Is Nothing Then _ToggleManualStartCommand = New RelayCommand(AddressOf ToggleManualStart, AddressOf CanToggleManualStart)
            Return _ToggleManualStartCommand
        End Get
    End Property

    Public Property IsManualStartOn As Boolean
        Get
            Return _IsManualStartOn
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsManualStartOn, _IsManualStartOn, value)
        End Set
    End Property

    Public Property IsFuelValveOpened As Boolean
        Get
            Return _IsFuelValveOpened
        End Get
        Set(value As Boolean)
            SetProperty(Function() IsFuelValveOpened, _IsFuelValveOpened, value)
        End Set
    End Property

    Public ReadOnly Property WasFuelValveOpenedWithMasterStartOff As Boolean
        Get
            Return _WasFuelValveOpenedWithMasterStartOff
        End Get
    End Property

    Public ReadOnly Property StartSelector As TrentXWBEngineStartSystemSelectorViewModel
        Get
            Return _StartSelector
        End Get
    End Property

#End Region

#Region "Command Methods"

    Public Sub Update(state As EnginePLCState)
        IsFuelValveOpened = state.IsFuelOn
        IsManualStartOn = state.IsManualStartOn
        StartSelector.Update(state)
    End Sub

    Private Function CanToggleFuelValve(obj As Object) As Boolean
        Return True
    End Function

    Private Sub ToggleFuelValve(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateFuelOnAsync(Not IsFuelValveOpened)
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

    Private Function CanToggleManualStart(obj As Object) As Boolean
        Return MainViewModel.Instance.EngineStartShutdownAndEEC.FacilityStartSystem.IsStartMasterOn
    End Function

    Private Sub ToggleManualStart(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.UpdateManualStartAsync(Not IsManualStartOn)
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
