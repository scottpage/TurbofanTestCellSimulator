Public Class PrestartReadingsScriptViewModel
    Inherits ScriptViewModel

    Public Sub New()
        Me.Name = "Prestart Readings"
    End Sub

    Public Overrides Sub Execute()
        System.Windows.MessageBox.Show("Make sure to fill out the DIGBERT battery log!!!")
        With MainViewModel.Instance.TestEngine.SimulationState.Parameters
            Log("*** BEGIN PRE-RUN READINGS ***")
            Log("Pre-Run TGT = {0} DegC", True, .TurbineGasTemperature.Average.ToString("#0.00"))
            Log("Humidity = {0} Percent", True, 73.3)
            Log("Outside Air Temperature = {0} DegC", True, MainViewModel.Instance.TestEngine.SimulationState.AirInletTemperature.ToString("#0.00"))
            Log("Engine Oil Contents = {0} USQTs at {1} DegC", True, .OilQuantity.Average, .OilTemperature.Average.ToString("#0.00"))
            Log("Fuel Farm Contents = {0} US Gallons", True, 95382)
            Log("Day Tank Contents = {0} US Gallons", True, 11589)
            Log("*** PRE-RUN READINGS COMPLETE ***")
            DoPrestartFullset()
        End With
    End Sub

    Private Sub DoPrestartFullset()
        Dim FS As New FullsetViewModel
        FS.ScanType = "T"
        FS.Comment = "Prestart"
        MainViewModel.Instance.TestEngine.FullsetController.StartFullset(FS)
    End Sub

End Class
