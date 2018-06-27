Public Delegate Sub ThrottleConnectFailedHandler(sender As Object, e As ThrottleConnectFailedEventArgs)
Public Delegate Sub ThrottlePositionUpdatedHandler(sender As Object, e As ThrottleLeverAngleUpdatedEventArgs)
Public Delegate Sub ThrottleRangeValueChangedHandler(sender As Object, e As ThrottleRangeValueChangedEventArgs)

Public Interface IThrottle

    Event MinimumLeverAngleChanged As ThrottleRangeValueChangedHandler
    Event MaximumLeverAngleChanged As ThrottleRangeValueChangedHandler

    ''' <summary>
    ''' Raised when the position has been updated.
    ''' </summary>
    ''' <remarks></remarks>
    Event LeverAngleUpdated As ThrottlePositionUpdatedHandler

    ''' <summary>
    ''' Raised when a throttle is connected.
    ''' </summary>
    ''' <remarks></remarks>
    Event Connected As EventHandler

    ''' <summary>
    ''' Raised when a communication to a throttle could not be established.
    ''' </summary>
    ''' <remarks></remarks>
    Event ConnectFailed As ThrottleConnectFailedHandler

    ''' <summary>
    ''' Raised when a throttle is disconnected.
    ''' </summary>
    ''' <remarks></remarks>
    Event Disconnected As EventHandler

    ''' <summary>
    ''' Indicates that the throttle requires a homing sequence to reset it's origin.
    ''' </summary>
    ''' <remarks>The homing sequence typically requires the lever be set to
    ''' it's predefined home position.  Once the throttle is positioned correctly call
    ''' the <see cref="IThrottle.Reset"/></remarks>
    Event ResetRequired As EventHandler

    ''' <summary>
    ''' Gets or Sets the current position of the throttle as an angle in degrees.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>The throttle angle is also known as the Pilot Lever Angle or PLA.
    ''' It indicates the position of the throttle lever over it's range of travel.</remarks>
    Property LeverAngle As Double

    Property MinimumLeverAngle As Double
    Property MaximumLeverAngle As Double
    Property MinimumHardwareValue As Double
    Property MaximumHardwareValue As Double
    Property IsDirectionReversed As Boolean
    ReadOnly Property HardwareToLeverAngleSlope As Double
    ReadOnly Property HardwareToLeverAngleIntercept As Double
    ReadOnly Property HardwareValue As Double

    ''' <summary>
    ''' Gets a value indicating that communication has been established with a throttle.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsConnected As Boolean

    Sub UpdateMinimumHardwareValue()
    Sub UpdateMaximumHardwareValue()

    ''' <summary>
    ''' Establishes a connection to a throttle.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Connect()

    ''' <summary>
    ''' Terminates communication with a throttle if a connection has been established.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Disconnect()

    ''' <summary>
    ''' Sets the origin on throttles that require a homing sequence after being powered on.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Reset()

End Interface
