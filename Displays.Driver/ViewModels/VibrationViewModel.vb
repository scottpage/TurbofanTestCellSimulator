Imports System.Threading

Public Class VibrationViewModel
    Inherits RealTimeDisplayViewModel

#Region "Fields"

    Private Shared _Instance As VibrationViewModel
    Private _LaneABroadbandVibration As ParameterViewModel
    Private _LaneBBroadbandVibration As ParameterViewModel
    Private _LaneAShaft1Vibration As ParameterViewModel
    Private _LaneBShaft1Vibration As ParameterViewModel
    Private _LaneAShaft2Vibration As ParameterViewModel
    Private _LaneBShaft2Vibration As ParameterViewModel
    Private _LaneAShaft3Vibration As ParameterViewModel
    Private _LaneBShaft3Vibration As ParameterViewModel

#End Region

#Region "Constructors"

    Private Sub New()
        Title = My.Settings.VibrationTitle
    End Sub

#End Region

#Region "Properties"

    Public Shared ReadOnly Property Instance As VibrationViewModel
        Get
            If _Instance Is Nothing Then _Instance = New VibrationViewModel
            Return _Instance
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

    Public ReadOnly Property LaneAShaft1Vibration As ParameterViewModel
        Get
            If _LaneAShaft1Vibration Is Nothing Then _LaneAShaft1Vibration = New ParameterViewModel
            Return _LaneAShaft1Vibration
        End Get
    End Property

    Public ReadOnly Property LaneBShaft1Vibration As ParameterViewModel
        Get
            If _LaneBShaft1Vibration Is Nothing Then _LaneBShaft1Vibration = New ParameterViewModel
            Return _LaneBShaft1Vibration
        End Get
    End Property

    Public ReadOnly Property LaneAShaft2Vibration As ParameterViewModel
        Get
            If _LaneAShaft2Vibration Is Nothing Then _LaneAShaft2Vibration = New ParameterViewModel
            Return _LaneAShaft2Vibration
        End Get
    End Property

    Public ReadOnly Property LaneBShaft2Vibration As ParameterViewModel
        Get
            If _LaneBShaft2Vibration Is Nothing Then _LaneBShaft2Vibration = New ParameterViewModel
            Return _LaneBShaft2Vibration
        End Get
    End Property

    Public ReadOnly Property LaneAShaft3Vibration As ParameterViewModel
        Get
            If _LaneAShaft3Vibration Is Nothing Then _LaneAShaft3Vibration = New ParameterViewModel
            Return _LaneAShaft3Vibration
        End Get
    End Property

    Public ReadOnly Property LaneBShaft3Vibration As ParameterViewModel
        Get
            If _LaneBShaft3Vibration Is Nothing Then _LaneBShaft3Vibration = New ParameterViewModel
            Return _LaneBShaft3Vibration
        End Get
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Update(values As SimulationParameters)
        LaneABroadbandVibration.Update(values.LaneABroadbandVibration)
        LaneBBroadbandVibration.Update(values.LaneBBroadbandVibration)

        LaneAShaft1Vibration.Update(values.Shaft1LaneAVibration)
        LaneBShaft1Vibration.Update(values.Shaft1LaneBVibration)

        LaneAShaft2Vibration.Update(values.Shaft2LaneAVibration)
        LaneBShaft2Vibration.Update(values.Shaft2LaneBVibration)

        If TypeOf values Is TrentXWBParameters Then
            Dim XWBConfig = DirectCast(values, TrentXWBParameters)
            LaneAShaft3Vibration.Update(XWBConfig.Shaft3LaneAVibration)
            LaneBShaft3Vibration.Update(XWBConfig.Shaft3LaneBVibration)
        End If
    End Sub

#End Region

End Class
