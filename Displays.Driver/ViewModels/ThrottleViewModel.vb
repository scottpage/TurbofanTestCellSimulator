Imports System.Windows.Threading

Public Class ThrottleViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private Shared _Instance As ThrottleViewModel
    Private WithEvents _Throttle As IThrottle = New TestCellSimulationThrottle

#End Region

#Region "Constructors"

    Private Sub New()
    End Sub

#End Region

#Region "Commands"

#Region "CalibrateThrottleCommand"

    Private _CalibrateThrottleCommand As ICommand
    Public ReadOnly Property CalibrateThrottleCommand As ICommand
        Get
            If _CalibrateThrottleCommand Is Nothing Then _
               _CalibrateThrottleCommand = New RelayCommand(AddressOf CalibrateThrottle, AddressOf CanCalibrateThrottle)
            Return _CalibrateThrottleCommand
        End Get
    End Property

    Private Function CanCalibrateThrottle(obj As Object) As Boolean
        Return CanCalibrateMinimumLeverAngle(obj) And CanCalibrateMaximumLeverAngle(obj)
    End Function

    Private Sub CalibrateThrottle(obj As Object)
        CalibrateThrottle()
    End Sub

#End Region

#Region "CalibrateMinimumLeverAngleCommand"

    Private _CalibrateMinimumLeverAngleCommand As ICommand
    Public ReadOnly Property CalibrateMinimumLeverAngleCommand As ICommand
        Get
            If _CalibrateMinimumLeverAngleCommand Is Nothing Then _CalibrateMinimumLeverAngleCommand = New RelayCommand(AddressOf CalibrateMinimumLeverAngle, AddressOf CanCalibrateMinimumLeverAngle)
            Return _CalibrateMinimumLeverAngleCommand
        End Get
    End Property

    Private Sub CalibrateMinimumLeverAngle(obj As Object)
        CalibrateThrottleMinimum()
    End Sub

    Private Function CanCalibrateMinimumLeverAngle(obj As Object) As Boolean
        Return _Throttle.IsConnected
    End Function

#End Region

#Region "CalibrateMaximumLeverAngleCommand"

    Private _CalibrateMaximumLeverAngleCommand As ICommand
    Public ReadOnly Property CalibrateMaximumLeverAngleCommand As ICommand
        Get
            If _CalibrateMaximumLeverAngleCommand Is Nothing Then _CalibrateMaximumLeverAngleCommand = New RelayCommand(AddressOf CalibrateMaximumLeverAngle, AddressOf CanCalibrateMaximumLeverAngle)
            Return _CalibrateMaximumLeverAngleCommand
        End Get
    End Property

    Private Function CanCalibrateMaximumLeverAngle(obj As Object) As Boolean
        Return _Throttle.IsConnected
    End Function

    Private Sub CalibrateMaximumLeverAngle(obj As Object)
        CalibrateThrottleMaximum()
    End Sub

#End Region

#End Region

#Region "Properties"

    Public Shared ReadOnly Property Instance As ThrottleViewModel
        Get
            If _Instance Is Nothing Then _Instance = New ThrottleViewModel
            Return _Instance
        End Get
    End Property

    Public ReadOnly Property Throttle As IThrottle
        Get
            Return _Throttle
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub TerminateThrottle()
        _Throttle.Disconnect()
        _Throttle = Nothing
    End Sub

    Public Sub CalibrateThrottle()
        CalibrateThrottleMinimum()
        CalibrateThrottleMaximum()
    End Sub

    Public Sub CalibrateThrottleMinimum()
        Xceed.Wpf.Toolkit.MessageBox.Show("Pull throttle lever back until it stops.")
        _Throttle.UpdateMinimumHardwareValue()
    End Sub

    Public Sub CalibrateThrottleMaximum()
        Xceed.Wpf.Toolkit.MessageBox.Show("Push throttle lever forward until it stops.")
        Throttle.UpdateMaximumHardwareValue()
    End Sub

    Private Sub _Throttle_ConnectFailed(sender As Object, e As ThrottleConnectFailedEventArgs) Handles _Throttle.ConnectFailed
        TerminateThrottle()
        _Throttle = New TestCellSimulationThrottle
        Throttle.Connect()
    End Sub

    Private Sub _Throttle_Disconnected(sender As Object, e As System.EventArgs) Handles _Throttle.Disconnected
        TerminateThrottle()
        _Throttle = New TestCellSimulationThrottle
        Throttle.Connect()
    End Sub

#End Region

End Class
