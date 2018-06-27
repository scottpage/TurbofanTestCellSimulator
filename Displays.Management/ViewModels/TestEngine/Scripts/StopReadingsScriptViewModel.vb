Public Class StopReadingsScriptViewModel
    Inherits ScriptViewModel

    Public Sub New()
        Me.Name = "Stop Readings"
    End Sub

    Public Overrides Sub Execute()
        Dim Readings = MainViewModel.Instance.TestEngine.SimulationState.EnginePLC.StopReadings
        Xceed.Wpf.Toolkit.MessageBox.Show("Turn ground and EMU power on", "Script", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK)
        Log("*** Begin Shutdown Readings ***")
        Log("No HP Rotation = {0} Secs", True, Readings.NoHPRotation.ToString("#0"))
        Log("No IP Rotation = {0} Secs", True, Readings.NoIPRotation.ToString("#0"))
        Log("No LP Rotation = {0} Secs", True, Readings.NoLPRotation.ToString("#0"))
        Log("Engine Oil Contents = {0} USQT at {1} DegC", True, Readings.EngineOilContent.ToString("#0.00"), Readings.EngineOilTemperature.ToString("#0.00"))
        Log("Max Vibration During Rundown = {0} %N3", True, Readings.MaxVibrationDuringRundown.ToString("#0.00"))
        Log("Fuel Farm Contents = {0} US Gallons", True, 89862)
        Log("Fuel consumed during test = {0} US Gallons", True, 5520)
        Log("*** End Shutdown Readings ***")
        MainViewModel.Instance.TestEngine.FullsetController.StartFullset(New FullsetViewModel With { _
                                                                         .Comment = "Engine shutdown", _
                                                                         .ScanType = "T"c, _
                                                                         .MajorScanNumber = 0, _
                                                                         .MinorScanNumber = 0})
    End Sub

End Class
