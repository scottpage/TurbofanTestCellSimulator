<Throttle("Scott Page", "Test Cell Simulation Throttle", 1, 0, "FESG-1610-1")> _
Public Class TestCellSimulationThrottle
    Inherits ViewModelBase
    Implements IThrottle

    Public Event MaximumLeverAngleChanged(sender As Object, e As ThrottleRangeValueChangedEventArgs) Implements IThrottle.MaximumLeverAngleChanged
    Public Event MinimumLeverAngleChanged(sender As Object, e As ThrottleRangeValueChangedEventArgs) Implements IThrottle.MinimumLeverAngleChanged
    Public Event LeverAngleUpdated(sender As Object, e As ThrottleLeverAngleUpdatedEventArgs) Implements IThrottle.LeverAngleUpdated
    Public Event Connected(sender As Object, e As System.EventArgs) Implements IThrottle.Connected
    Public Event ConnectFailed(sender As Object, e As ThrottleConnectFailedEventArgs) Implements IThrottle.ConnectFailed
    Public Event Disconnected(sender As Object, e As System.EventArgs) Implements IThrottle.Disconnected
    Public Event ResetRequired(sender As Object, e As System.EventArgs) Implements IThrottle.ResetRequired

    Private WithEvents _DeviceManager As DeviceManager
    Private WithEvents _QSB As QSB_S

    Public ReadOnly Property IsConnected As Boolean Implements IThrottle.IsConnected
        Get
            Return _QSB IsNot Nothing AndAlso _QSB.IsOpen
        End Get
    End Property

    Public Property LeverAngle As Double Implements IThrottle.LeverAngle
        Get
            Return HardwareToLeverAngleSlope * HardwareValue + HardwareToLeverAngleIntercept
        End Get
        Set(value As Double)
            'QSB update event used.
        End Set
    End Property

    Private _MaximumLeverAngle As Double
    Public Property MaximumLeverAngle As Double Implements IThrottle.MaximumLeverAngle
        Get
            Return _MaximumLeverAngle
        End Get
        Set(value As Double)
            SetProperty(Function() MaximumLeverAngle, _MaximumLeverAngle, value)
            OnPropertyChanged(Function() LeverAngle)
            RaiseEvent MinimumLeverAngleChanged(Me, New ThrottleRangeValueChangedEventArgs(value))
            RaiseEvent LeverAngleUpdated(Me, New ThrottleLeverAngleUpdatedEventArgs(LeverAngle))
        End Set
    End Property

    Private _MinimumLeverAngle As Double
    Public Property MinimumLeverAngle As Double Implements IThrottle.MinimumLeverAngle
        Get
            Return _MinimumLeverAngle
        End Get
        Set(value As Double)
            SetProperty(Function() MinimumLeverAngle, _MinimumLeverAngle, value)
            OnPropertyChanged(Function() LeverAngle)
            RaiseEvent MaximumLeverAngleChanged(Me, New ThrottleRangeValueChangedEventArgs(value))
            RaiseEvent LeverAngleUpdated(Me, New ThrottleLeverAngleUpdatedEventArgs(LeverAngle))
        End Set
    End Property

    Private _HardwareValue As Double
    Public ReadOnly Property HardwareValue As Double Implements IThrottle.HardwareValue
        Get
            Return _HardwareValue
        End Get
    End Property

    Public ReadOnly Property HardwareToLeverAngleIntercept As Double Implements IThrottle.HardwareToLeverAngleIntercept
        Get
            Return MinimumLeverAngle
        End Get
    End Property

    Public ReadOnly Property HardwareToLeverAngleSlope As Double Implements IThrottle.HardwareToLeverAngleSlope
        Get
            Return (MaximumLeverAngle - MinimumLeverAngle) / (MaximumHardwareValue - MinimumHardwareValue)
        End Get
    End Property

    Private _MaximumHardwareValue As Double
    Public Property MaximumHardwareValue As Double Implements IThrottle.MaximumHardwareValue
        Get
            Return _MaximumHardwareValue
        End Get
        Set(value As Double)
            SetProperty(Function() MaximumHardwareValue, _MaximumHardwareValue, value)
        End Set
    End Property

    Private _MinimumHardwareValue As Double
    Public Property MinimumHardwareValue As Double Implements IThrottle.MinimumHardwareValue
        Get
            Return _MinimumHardwareValue
        End Get
        Set(value As Double)
            SetProperty(Function() MinimumHardwareValue, _MinimumHardwareValue, value)
        End Set
    End Property

    Public Sub Connect() Implements IThrottle.Connect
        _DeviceManager = New DeviceManager
        _DeviceManager.Initialize()
        _QSB = DirectCast(_DeviceManager.Devices.FirstOrDefault, QSB_S)
        If _QSB IsNot Nothing Then ConfigureQSB()
    End Sub

    Public Sub Disconnect() Implements IThrottle.Disconnect
        If _QSB IsNot Nothing Then
            If _QSB.IsOpen Then _QSB.Close()
            _QSB = Nothing
        End If
        If _DeviceManager IsNot Nothing Then
            _DeviceManager.Shutdown()
            _DeviceManager.Close()
            _DeviceManager = Nothing
        End If
        OnPropertyChanged(Function() IsConnected)
    End Sub

    Public Overrides Sub Reset() Implements IThrottle.Reset
        If IsConnected Then _QSB.ResetCount()
    End Sub

    Private Sub ConfigureQSB()
        _QSB.SetCounterMode(CounterMode.FreeRunningCounter)
        _QSB.SetPreset(360)
        _QSB.StreamEncoderCount(1, 10)
        RaiseEvent Connected(Me, EventArgs.Empty)
        RaiseEvent ResetRequired(Me, EventArgs.Empty)
        OnPropertyChanged(Function() IsConnected)
    End Sub

    Private Sub _DeviceManager_OnConnectionChanged(args As USDigital.DeviceManager.DeviceConnectionEventArgs) Handles _DeviceManager.OnConnectionChanged
        If args.IsConnected And TypeOf args.Device Is QSB_S And _QSB Is Nothing Then
            _QSB = DirectCast(args.Device, QSB_S)
            ConfigureQSB()
        Else
            Disconnect()
        End If
    End Sub

    Private Sub _QSB_OnRegisterValueChanged(sender As Object, args As USDigital.RegisterChangeEventArgs) Handles _QSB.OnRegisterValueChanged
        Update(args.Value)
    End Sub

    Public Sub UpdateMaximumHardwareValue() Implements IThrottle.UpdateMaximumHardwareValue
        MaximumHardwareValue = HardwareValue
        UpdateSlopeAndIntercept()
    End Sub

    Public Sub UpdateMinimumHardwareValue() Implements IThrottle.UpdateMinimumHardwareValue
        Reset()
        MinimumHardwareValue = MinimumHardwareValue
        UpdateSlopeAndIntercept()
    End Sub

    Private Sub UpdateSlopeAndIntercept()
        OnPropertyChanged(Function() HardwareToLeverAngleSlope)
        OnPropertyChanged(Function() HardwareToLeverAngleIntercept)
        RaiseEvent LeverAngleUpdated(Me, New ThrottleLeverAngleUpdatedEventArgs(LeverAngle))
    End Sub

    Private Sub Update(value As Double)
        SetProperty(Function() HardwareValue, _HardwareValue, value)
        RaiseEvent LeverAngleUpdated(Me, New ThrottleLeverAngleUpdatedEventArgs(LeverAngle))
        OnPropertyChanged(Function() LeverAngle)
    End Sub

    Private _IsDirectionReversed As Boolean
    Public Property IsDirectionReversed As Boolean Implements IThrottle.IsDirectionReversed
        Get
            Return _IsDirectionReversed
        End Get
        Set(value As Boolean)
            _IsDirectionReversed = value
            If IsConnected Then
                If value Then
                    _QSB.SetDirection(EncoderDirection.CountingUp)
                Else
                    _QSB.SetDirection(EncoderDirection.CountingDown)
                End If
            End If
            OnPropertyChanged(Function() IsDirectionReversed)
        End Set
    End Property

End Class
