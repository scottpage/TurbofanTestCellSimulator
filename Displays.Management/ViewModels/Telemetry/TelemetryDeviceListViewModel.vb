Imports System.Collections.Specialized

Public Class TelemetryDeviceListViewModel
    Inherits RealTimeDisplayViewModel

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Private _Devices As ObservableCollection(Of TelemetryDeviceViewModel)
    Public ReadOnly Property Devices As ObservableCollection(Of TelemetryDeviceViewModel)
        Get
            If _Devices Is Nothing Then _
               _Devices = New ObservableCollection(Of TelemetryDeviceViewModel)
            Return _Devices
        End Get
    End Property

    Private _SelectedDevice As TelemetryDeviceViewModel
    Public Property SelectedDevice As TelemetryDeviceViewModel
        Get
            Return _SelectedDevice
        End Get
        Set(ByVal Value As TelemetryDeviceViewModel)
            SetProperty(Function() SelectedDevice, _SelectedDevice, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        For Each D In Devices
            D.Reset()
        Next
    End Sub

#End Region

End Class
