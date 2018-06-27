Imports System.Windows.Threading

Public Class StartReadingsViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _Visibility As Visibility = Visibility.Hidden
    Private _CloseCommand As ICommand
    Private _ClearCommand As ICommand

#End Region

#Region "Properties"

    Public ReadOnly Property CloseCommand As ICommand
        Get
            If _CloseCommand Is Nothing Then _CloseCommand = New RelayCommand(AddressOf Close)
            Return _CloseCommand
        End Get
    End Property

    Public ReadOnly Property ClearCommand As ICommand
        Get
            If _ClearCommand Is Nothing Then _ClearCommand = New RelayCommand(AddressOf Clear)
            Return _ClearCommand
        End Get
    End Property

    Public Property Visibility As Visibility
        Get
            Return _Visibility
        End Get
        Set(value As Visibility)
            SetProperty(Function() Visibility, _Visibility, value)
        End Set
    End Property

    Private _HPRotation As Double
    Public Property HPRotation() As Double
        Get
            Return _HPRotation
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() HPRotation, _HPRotation, value)
        End Set
    End Property

    Private _IPRotation As Double
    Public Property IPRotation() As Double
        Get
            Return _IPRotation
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() IPRotation, _IPRotation, value)
        End Set
    End Property

    Private _LPRotation As Double
    Public Property LPRotation() As Double
        Get
            Return _LPRotation
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() LPRotation, _LPRotation, value)
        End Set
    End Property

    Private _PrestartTGT As Double
    Public Property PrestartTGT() As Double
        Get
            Return _PrestartTGT
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() PrestartTGT, _PrestartTGT, value)
        End Set
    End Property

    Private _PrestartStartAirPressure As Double
    Public Property PrestartStartAirPressure() As Double
        Get
            Return _PrestartStartAirPressure
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() PrestartStartAirPressure, _PrestartStartAirPressure, value)
        End Set
    End Property

    Private _StartingStartAirPressure As Double
    Public Property StartingStartAirPressure() As Double
        Get
            Return _StartingStartAirPressure
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() StartingStartAirPressure, _StartingStartAirPressure, value)
        End Set
    End Property

    Private _N3DeadCrankSpeed As Double
    Public Property N3DeadCrankSpeed() As Double
        Get
            Return _N3DeadCrankSpeed
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() N3DeadCrankSpeed, _N3DeadCrankSpeed, value)
        End Set
    End Property

    Private _N3DeadCrankSeconds As Double
    Public Property N3DeadCrankSeconds() As Double
        Get
            Return _N3DeadCrankSeconds
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() N3DeadCrankSeconds, _N3DeadCrankSeconds, value)
        End Set
    End Property

    Private _N2DeadCrankSpeed As Double
    Public Property N2DeadCrankSpeed() As Double
        Get
            Return _N2DeadCrankSpeed
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() N2DeadCrankSpeed, _N2DeadCrankSpeed, value)
        End Set
    End Property

    Private _N2DeadCrankSeconds As Double
    Public Property N2DeadCrankSeconds() As Double
        Get
            Return _N2DeadCrankSeconds
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() N2DeadCrankSeconds, _N2DeadCrankSeconds, value)
        End Set
    End Property

    Private _FuelOn As Double
    Public Property FuelOn() As Double
        Get
            Return _FuelOn
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() FuelOn, _FuelOn, value)
        End Set
    End Property

    Private _FuelOnSeconds As Double
    Public Property FuelOnSeconds() As Double
        Get
            Return _FuelOnSeconds
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() FuelOnSeconds, _FuelOnSeconds, value)
        End Set
    End Property

    Private _Lit As Double
    Public Property Lit() As Double
        Get
            Return _Lit
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() Lit, _Lit, value)
        End Set
    End Property

    Private _LitSeconds As Double
    Public Property LitSeconds() As Double
        Get
            Return _LitSeconds
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() LitSeconds, _LitSeconds, value)
        End Set
    End Property

    Private _LightupFuelFlow As Double
    Public Property LightupFuelFlow() As Double
        Get
            Return _LightupFuelFlow
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() LightupFuelFlow, _LightupFuelFlow, value)
        End Set
    End Property

    Private _StarterCut As Double
    Public Property StarterCut() As Double
        Get
            Return _StarterCut
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() StarterCut, _StarterCut, value)
        End Set
    End Property

    Private _StarterCutSeconds As Double
    Public Property StarterCutSeconds() As Double
        Get
            Return _StarterCutSeconds
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() StarterCutSeconds, _StarterCutSeconds, value)
        End Set
    End Property


    Private _MaxTGT As Double
    Public Property MaxTGT() As Double
        Get
            Return _MaxTGT
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() MaxTGT, _MaxTGT, value)
        End Set
    End Property

    Private _TimeToGI As Double
    Public Property TimeToGI() As Double
        Get
            Return _TimeToGI
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() TimeToGI, _TimeToGI, value)
        End Set
    End Property

    Private _VibrationAtIdle As Double
    Public Property VibrationAtIdle() As Double
        Get
            Return _VibrationAtIdle
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() VibrationAtIdle, _VibrationAtIdle, value)
        End Set
    End Property

    Private _OilPressureAtIdle As Double
    Public Property OilPressureAtIdle() As Double
        Get
            Return _OilPressureAtIdle
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() OilPressureAtIdle, _OilPressureAtIdle, value)
        End Set
    End Property

    Private _AirIntakeTemperatureAtIdle As Double
    Public Property AirIntakeTemperatureAtIdle() As Double
        Get
            Return _AirIntakeTemperatureAtIdle
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() AirIntakeTemperatureAtIdle, _AirIntakeTemperatureAtIdle, value)
        End Set
    End Property

#End Region

#Region "Methods"

    Private Sub Close(obj As Object)
        Visibility = Visibility.Hidden
    End Sub

    Private Sub Clear(obj As Object)
        Dim Client As EnginePLCServiceClient = Nothing
        Try
            Client = New EnginePLCServiceClient
            Client.ClearStartReadingsAsync()
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

    Public Sub Update(readings As EnginePLCStartReadings)
        If Not Visibility = Windows.Visibility.Visible Then Return
        AirIntakeTemperatureAtIdle = readings.AirIntakeTemperatureAtIdle
        FuelOn = readings.FuelOn
        FuelOnSeconds = readings.FuelOnSeconds
        LitSeconds = readings.LitSeconds
        N2DeadCrankSeconds = readings.N2DeadCrankSeconds
        StarterCutSeconds = readings.StarterCutSeconds
        HPRotation = readings.HPRotation
        IPRotation = readings.IPRotation
        LightupFuelFlow = readings.LightupFuelFlow
        Lit = readings.Lit
        LPRotation = readings.LPRotation
        MaxTGT = readings.MaxTGT
        N2DeadCrankSpeed = readings.N2DeadCrankSpeed
        N3DeadCrankSpeed = readings.N3DeadCrankSpeed
        OilPressureAtIdle = readings.OilPressureAtIdle
        PrestartStartAirPressure = readings.PrestartStartAirPressure
        PrestartTGT = readings.PrestartTGT
        StarterCut = readings.StarterCut
        StartingStartAirPressure = readings.StartingStartAirPressure
        TimeToGI = readings.TimeToGI
        VibrationAtIdle = readings.VibrationAtIdle
    End Sub

#End Region

End Class
