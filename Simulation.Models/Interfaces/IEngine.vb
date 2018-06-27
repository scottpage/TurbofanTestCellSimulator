Public Interface IEngine

    Property HighIdleThrottleLeverAngle As Double
    Property AirInletTemperature As Parameter
    Property Shaft1 As Shaft
    Property Shaft2 As Shaft
    Property Type As String
    Property SerialNumber As String
    Property Build As String
    Property PartTest As String
    Property Shafts As List(Of Shaft)
    Property LaneABroadbandVibration As Parameter
    Property LaneBBroadbandVibration As Parameter
    Property TurbineGasTemperature As Parameter
    Property Oil As OilSystem
    Property HighPressureCompressor As Compressor
    Property Fuel As FuelSystem
    Property Throttle As ThrottleSystem
    Property Starters As List(Of Starter)
    Property Ignition As IgnitionSystem
    Property EEC As ElectronicEngineController
    Property EMU As EngineMonitoringUnit
    Property Mode As EngineMode
    Property Thrust As Parameter
    Property StartDuration As Integer
    Property ShutdownDuration As Integer
    Property StartVideoLeadTime As Integer
    Property StarterCutTime As Integer
    ReadOnly Property Parameters As ParameterListViewModel
    Sub Update()
    Sub Update(value As Double)

End Interface
