Public Class StartReadingsScriptViewModel
    Inherits ScriptViewModel

    Public Sub New()
        Me.Name = "Start Readings"
    End Sub

    Public Overrides Sub Execute()
        Dim Readings = MainViewModel.Instance.TestEngine.SimulationState.EnginePLC.StartReadings
        Log("*** Begin Start Readings ***")
        Log("HP Rotation = {0} Secs", True, Readings.HPRotation.ToString("#0"))
        Log("IP Rotation = {0} Secs", True, Readings.IPRotation.ToString("#0"))
        Log("LP Rotation = {0} Secs", True, Readings.LPRotation.ToString("#0"))
        Log("Start Air Pressure = {0} PSIG", True, Readings.StartingStartAirPressure.ToString("#0.00"))
        Log("Dead Crank Speed = {0} %N3", True, Readings.N3DeadCrankSpeed.ToString("#0.00"))
        Log("Fuel On = {0} Secs", True, Readings.FuelOnSeconds.ToString("#0"))
        Log("Lit = {0} %N3", True, Readings.Lit.ToString("#0.00"))
        Log("Starter Cut = {0} %N3", True, Readings.StarterCut.ToString("#0.00"))
        Log("Max TGT = {0} DegC", True, Readings.MaxTGT.ToString("#0.00"))
        Log("Ground Idle in = {0} Secs", True, Readings.TimeToGI.ToString("#0"))
        Log("Vibration = {0} in/Sec", True, Readings.VibrationAtIdle.ToString("#0.00"))
        Log("Oil Pressure = {0} PSIG @ Idle", True, Readings.OilPressureAtIdle.ToString("#0.00"))
        Log("Air Inlet Temperature = {0} DegC @ Idle", True, Readings.AirIntakeTemperatureAtIdle.ToString("#0.00"))
        Log("*** End Start Readings ***")

    End Sub

End Class
