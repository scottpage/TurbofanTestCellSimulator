Public Class DriverWindow

    Private _CalibrationWindow As CalibrateThrottleWindow

    Private Sub ThrottleLeverAngleGauge_PreviewMouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles ThrottleLeverAngleGauge.PreviewMouseDoubleClick
        If Not ThrottleViewModel.Instance.Throttle.IsConnected Then Return
        _CalibrationWindow = New CalibrateThrottleWindow
        Dim VM As New CalibrateThrottleViewModel
        AddHandler VM.CalibrationCompleted, AddressOf CalibrationViewModel_CalibrationCompleted
        _CalibrationWindow.DataContext = VM
        _CalibrationWindow.ShowDialog()
    End Sub

    Private Sub CalibrationViewModel_CalibrationCompleted(sender As Object, e As EventArgs)
        If _CalibrationWindow IsNot Nothing Then
            _CalibrationWindow.Close()
            _CalibrationWindow = Nothing
        End If
    End Sub

End Class
