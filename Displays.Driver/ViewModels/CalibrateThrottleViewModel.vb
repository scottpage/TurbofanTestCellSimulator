Public Class CalibrateThrottleViewModel
    Inherits ViewModelBase

#Region "Events"

    Public Event CalibrationCompleted As EventHandler

#End Region

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Commands"

    Private _CalibrateCurrentPositionCommand As ICommand
    Public ReadOnly Property CalibrateCurrentPositionCommand As ICommand
        Get
            If _CalibrateCurrentPositionCommand Is Nothing Then _
               _CalibrateCurrentPositionCommand = New RelayCommand(AddressOf CalibrateCurrentPosition, AddressOf CanCalibrateCurrentPosition)
            Return _CalibrateCurrentPositionCommand
        End Get
    End Property

    Private Function CanCalibrateCurrentPosition(obj As Object) As Boolean
        Return True
    End Function

    Private Sub CalibrateCurrentPosition(obj As Object)
        If IsCalibratingMaximum Then
            ThrottleViewModel.Instance.Throttle.UpdateMaximumHardwareValue()
            RaiseEvent CalibrationCompleted(Me, EventArgs.Empty)
        Else
            ThrottleViewModel.Instance.Throttle.UpdateMinimumHardwareValue()
            IsCalibratingMaximum = True
        End If
    End Sub

#End Region

#Region "Properties"

    Private _IsCalibratingMaximum As Boolean
    Public Property IsCalibratingMaximum As Boolean
        Get
            Return _IsCalibratingMaximum
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsCalibratingMaximum, _IsCalibratingMaximum, Value)
        End Set
    End Property

#End Region

#Region "Methods"



#End Region

End Class
