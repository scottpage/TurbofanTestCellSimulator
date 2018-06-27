Imports System.Windows.Threading

Public Class StopReadingsViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Properties"

    Private _Visibility As Visibility = Visibility.Hidden
    Public Property Visibility As Visibility
        Get
            Return _Visibility
        End Get
        Set(value As Visibility)
            SetProperty(Function() Visibility, _Visibility, value)
        End Set
    End Property

    Private _NoHPRotation As Double
    Public Property NoHPRotation() As Double
        Get
            Return _NoHPRotation
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() NoHPRotation, _NoHPRotation, value)
        End Set
    End Property

    Private _NoIPRotation As Double
    Public Property NoIPRotation() As Double
        Get
            Return _NoIPRotation
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() NoIPRotation, _NoIPRotation, value)
        End Set
    End Property

    Private _NoLPRotation As Double
    Public Property NoLPRotation() As Double
        Get
            Return _NoLPRotation
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() NoLPRotation, _NoLPRotation, value)
        End Set
    End Property

    Private _EngineOilContent As Double
    Public Property EngineOilContent() As Double
        Get
            Return _EngineOilContent
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() EngineOilContent, _EngineOilContent, value)
        End Set
    End Property

    Private _EngineOilTemperature As Double
    Public Property EngineOilTemperature() As Double
        Get
            Return _EngineOilTemperature
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() EngineOilTemperature, _EngineOilTemperature, value)
        End Set
    End Property

    Private _MaxVibrationDuringRundown As Double
    Public Property MaxVibrationDuringRundown() As Double
        Get
            Return _MaxVibrationDuringRundown
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() MaxVibrationDuringRundown, _MaxVibrationDuringRundown, value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub Update(readings As EnginePLCStopReadings)
        If Not Visibility = Windows.Visibility.Visible Then Return
        NoHPRotation = readings.NoHPRotation
        NoIPRotation = readings.NoIPRotation
        NoLPRotation = readings.NoLPRotation
        EngineOilContent = readings.EngineOilContent
        EngineOilTemperature = readings.EngineOilTemperature
        MaxVibrationDuringRundown = readings.MaxVibrationDuringRundown
    End Sub

#End Region

#Region "Commands"

    Private _ClearCommand As ICommand
    Public ReadOnly Property ClearCommand As ICommand
        Get
            If _ClearCommand Is Nothing Then _
               _ClearCommand = New RelayCommand(AddressOf Clear)
            Return _ClearCommand
        End Get
    End Property

    Private Sub Clear(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.ClearStopReadingsAsync()
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

    Private _CloseCommand As ICommand
    Public ReadOnly Property CloseCommand As ICommand
        Get
            If _CloseCommand Is Nothing Then _
               _CloseCommand = New RelayCommand(AddressOf Close)
            Return _CloseCommand
        End Get
    End Property

    Private Sub Close(obj As Object)
        Visibility = Visibility.Hidden
    End Sub

#End Region

End Class
