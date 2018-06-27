Imports System.Threading

Public Class DriverViewModel
    Inherits RealTimeDisplayViewModel

#Region "Fields"

    Private Shared _Instance As DriverViewModel
    Private _AirInletTemperature As ParameterViewModel
    Private _Speed1 As ParameterViewModel
    Private _Speed2 As ParameterViewModel
    Private _Speed3 As ParameterViewModel
    Private _LaneABroadbandVibration As ParameterViewModel
    Private _LaneBBroadbandVibration As ParameterViewModel
    Private _TurbineGasTemperature As ParameterViewModel
    Private _T20 As ParameterViewModel
    Private _P20 As ParameterViewModel
    Private _T30 As ParameterViewModel
    Private _P30 As ParameterViewModel
    Private _OilTemperature As ParameterViewModel
    Private _OilPressure As ParameterViewModel
    Private _OilQuantity As ParameterViewModel
    Private _FuelPressure As ParameterViewModel
    Private _ThrottleLeverAngle As ParameterViewModel
    Private _ThrottleResolverAngle As ParameterViewModel
    Private _Thrust As ParameterViewModel
    Private _Speed1Corrected As ParameterViewModel
    Private _Speed1Root20 As ParameterViewModel
    Private _StartAirPressure As ParameterViewModel

#End Region

#Region "Constructors"

    Private Sub New()
        Title = My.Settings.DriverTitle
        ThrottleLeverAngle.UseValueAsAverage = True
        ThrottleResolverAngle.UseValueAsAverage = True
        With Speed1Corrected
            .UseCorrectedSpeedAsAverage = True
            .AllowNameChange = False
            .AllowAlarmChange = False
            .AllowEngineeringUnitsChange = False
            .AllowRangeChange = False
            .Name = "NLCORR"
            .EngineeringUnits = "%"
        End With
        With Speed1Root20
            .UseCorrectedSpeedRoot20AsAverage = True
            .AllowNameChange = False
            .AllowAlarmChange = False
            .AllowEngineeringUnitsChange = False
            .AllowRangeChange = False
            .Name = "NLRT120"
            .EngineeringUnits = "RPM/SQRT(K)"
        End With
    End Sub

#End Region

#Region "Properties"

    Public Shared ReadOnly Property Instance As DriverViewModel
        Get
            If _Instance Is Nothing Then _Instance = New DriverViewModel
            Return _Instance
        End Get
    End Property

    Private _IsFuelOn As Boolean
    Public Property IsFuelOn As Boolean
        Get
            Return _IsFuelOn
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsFuelOn, _IsFuelOn, Value)
        End Set
    End Property

    Private _IsShaft1Rotating As Boolean
    Public Property IsShaft1Rotating As Boolean
        Get
            Return _IsShaft1Rotating
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsShaft1Rotating, _IsShaft1Rotating, Value)
        End Set
    End Property

    Private _IsShaft2Rotating As Boolean
    Public Property IsShaft2Rotating As Boolean
        Get
            Return _IsShaft2Rotating
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsShaft2Rotating, _IsShaft2Rotating, Value)
        End Set
    End Property

    Private _IsShaft3Rotating As Boolean
    Public Property IsShaft3Rotating As Boolean
        Get
            Return _IsShaft3Rotating
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsShaft3Rotating, _IsShaft3Rotating, Value)
        End Set
    End Property

    Public ReadOnly Property AirInletTemperature As ParameterViewModel
        Get
            If _AirInletTemperature Is Nothing Then _AirInletTemperature = New ParameterViewModel
            Return _AirInletTemperature
        End Get
    End Property

    Public ReadOnly Property LaneABroadbandVibration As ParameterViewModel
        Get
            If _LaneABroadbandVibration Is Nothing Then _LaneABroadbandVibration = New ParameterViewModel
            Return _LaneABroadbandVibration
        End Get
    End Property

    Public ReadOnly Property LaneBBroadbandVibration As ParameterViewModel
        Get
            If _LaneBBroadbandVibration Is Nothing Then _LaneBBroadbandVibration = New ParameterViewModel
            Return _LaneBBroadbandVibration
        End Get
    End Property

    Public ReadOnly Property Speed1 As ParameterViewModel
        Get
            If _Speed1 Is Nothing Then _Speed1 = New ParameterViewModel
            Return _Speed1
        End Get
    End Property

    Public ReadOnly Property Speed2 As ParameterViewModel
        Get
            If _Speed2 Is Nothing Then _Speed2 = New ParameterViewModel
            Return _Speed2
        End Get
    End Property

    Public ReadOnly Property Speed3 As ParameterViewModel
        Get
            If _Speed3 Is Nothing Then _Speed3 = New ParameterViewModel
            Return _Speed3
        End Get
    End Property

    Public ReadOnly Property TurbineGasTemperature As ParameterViewModel
        Get
            If _TurbineGasTemperature Is Nothing Then _TurbineGasTemperature = New ParameterViewModel
            Return _TurbineGasTemperature
        End Get
    End Property

    Public ReadOnly Property T20 As ParameterViewModel
        Get
            If _T20 Is Nothing Then _T20 = New ParameterViewModel
            Return _T20
        End Get
    End Property

    Public ReadOnly Property P20 As ParameterViewModel
        Get
            If _P20 Is Nothing Then _P20 = New ParameterViewModel
            Return _P20
        End Get
    End Property

    Public ReadOnly Property T30 As ParameterViewModel
        Get
            If _T30 Is Nothing Then _T30 = New ParameterViewModel
            Return _T30
        End Get
    End Property

    Public ReadOnly Property P30 As ParameterViewModel
        Get
            If _P30 Is Nothing Then _P30 = New ParameterViewModel
            Return _P30
        End Get
    End Property

    Public ReadOnly Property OilTemperature As ParameterViewModel
        Get
            If _OilTemperature Is Nothing Then _OilTemperature = New ParameterViewModel
            Return _OilTemperature
        End Get
    End Property

    Public ReadOnly Property OilPressure As ParameterViewModel
        Get
            If _OilPressure Is Nothing Then _OilPressure = New ParameterViewModel
            Return _OilPressure
        End Get
    End Property

    Public ReadOnly Property OilQuantity As ParameterViewModel
        Get
            If _OilQuantity Is Nothing Then _OilQuantity = New ParameterViewModel
            Return _OilQuantity
        End Get
    End Property

    Public ReadOnly Property FuelPressure As ParameterViewModel
        Get
            If _FuelPressure Is Nothing Then _FuelPressure = New ParameterViewModel
            Return _FuelPressure
        End Get
    End Property

    Public ReadOnly Property ThrottleLeverAngle As ParameterViewModel
        Get
            If _ThrottleLeverAngle Is Nothing Then _ThrottleLeverAngle = New ParameterViewModel
            Return _ThrottleLeverAngle
        End Get
    End Property

    Public ReadOnly Property ThrottleResolverAngle As ParameterViewModel
        Get
            If _ThrottleResolverAngle Is Nothing Then _ThrottleResolverAngle = New ParameterViewModel
            Return _ThrottleResolverAngle
        End Get
    End Property

    Public ReadOnly Property Thrust As ParameterViewModel
        Get
            If _Thrust Is Nothing Then _Thrust = New ParameterViewModel
            Return _Thrust
        End Get
    End Property

    Public ReadOnly Property Speed1Corrected As ParameterViewModel
        Get
            If _Speed1Corrected Is Nothing Then _Speed1Corrected = New ParameterViewModel
            Return _Speed1Corrected
        End Get
    End Property

    Public ReadOnly Property Speed1Root20 As ParameterViewModel
        Get
            If _Speed1Root20 Is Nothing Then _Speed1Root20 = New ParameterViewModel
            Return _Speed1Root20
        End Get
    End Property

    Public ReadOnly Property StartAirPressure As ParameterViewModel
        Get
            If _StartAirPressure Is Nothing Then _StartAirPressure = New ParameterViewModel
            Return _StartAirPressure
        End Get
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

    Private _IsFullsetRunning As Boolean
    Public Property IsFullsetRunning As Boolean
        Get
            Return _IsFullsetRunning
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsFullsetRunning, _IsFullsetRunning, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Update(values As SimulationParameters)
        AirInletTemperature.Update(values.AirInletTemperature)
        FuelPressure.Update(values.FuelPressure)
        LaneABroadbandVibration.Update(values.LaneABroadbandVibration)
        LaneBBroadbandVibration.Update(values.LaneBBroadbandVibration)
        OilPressure.Update(values.OilPressure)
        OilQuantity.Update(values.OilQuantity)
        OilTemperature.Update(values.OilTemperature)
        TurbineGasTemperature.Update(values.TurbineGasTemperature)
        P30.Update(values.HighPressureCompressorPressure)
        T30.Update(values.HighPressureCompressorTemperature)
        ThrottleLeverAngle.Update(values.ThrottleLeverAngle)
        ThrottleResolverAngle.Update(values.ThrottleResolverAngle)
        Thrust.Update(values.Thrust)
        Speed1.Update(values.Shaft1)
        Speed2.Update(values.Shaft2)
        IsShaft1Rotating = Speed1.Average > 0.5
        IsShaft2Rotating = Speed2.Average > 0.5
        Speed1Corrected.Update(values.Shaft1)
        Speed1Root20.Update(values.Shaft1)
        If TypeOf values Is TrentXWBParameters Then
            Dim XWBConfig = DirectCast(values, TrentXWBParameters)
            Speed3.Update(XWBConfig.Shaft3)
            IsFuelOn = Speed3.Average >= 25
            IsShaft3Rotating = Speed3.Average > 0.5
            StartAirPressure.Update(XWBConfig.StarterPressure)
        End If
    End Sub

#End Region

End Class
