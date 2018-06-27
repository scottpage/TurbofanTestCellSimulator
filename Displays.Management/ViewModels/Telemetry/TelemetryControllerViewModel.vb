Public Class TelemetryControllerViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New()
        _DIGBERTControl = New DIGBERTControlViewModel(1, "GL000007")
        For I = 1 To 3
            CHINCAControl.Devices.Add(New CHINCAControlViewModel(I, "GA00000" & I.ToString))
            STINCAControl.Devices.Add(New STINCAControlViewModel(I, "GZ00000" & I.ToString))
        Next
    End Sub

#End Region

#Region "Properties"

    Private _DIGBERTControl As DIGBERTControlViewModel
    Public ReadOnly Property DIGBERTControl As DIGBERTControlViewModel
        Get
            Return _DIGBERTControl
        End Get
    End Property

    Private _CHINCAControl As New TelemetryDeviceListViewModel
    Public ReadOnly Property CHINCAControl As TelemetryDeviceListViewModel
        Get
            Return _CHINCAControl
        End Get
    End Property

    Private _STINCAControl As New TelemetryDeviceListViewModel
    Public ReadOnly Property STINCAControl As TelemetryDeviceListViewModel
        Get
            Return _STINCAControl
        End Get
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        DIGBERTControl.Reset()
        STINCAControl.Reset()
        CHINCAControl.Reset()
    End Sub

#End Region

End Class
