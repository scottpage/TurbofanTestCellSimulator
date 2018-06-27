Imports System.Windows

Public Class TelemetryDeviceChannelGainView

    Public Property Header As String
        Get
            Return CStr(GetValue(HeaderProperty))
        End Get

        Set(ByVal value As String)
            SetValue(HeaderProperty, value)
        End Set
    End Property

    Public Shared ReadOnly HeaderProperty As DependencyProperty = _
                           DependencyProperty.Register("Header", _
                           GetType(String), GetType(TelemetryDeviceChannelGainView), _
                           New FrameworkPropertyMetadata(Nothing))

    Public Property Gain As Integer
        Get
            Return CInt(GetValue(GainProperty))
        End Get

        Set(ByVal value As Integer)
            SetValue(GainProperty, value)
        End Set
    End Property

    Public Shared ReadOnly GainProperty As DependencyProperty = _
                           DependencyProperty.Register("Gain", _
                           GetType(Integer), GetType(TelemetryDeviceChannelGainView), _
                           New FrameworkPropertyMetadata(Nothing))

End Class
