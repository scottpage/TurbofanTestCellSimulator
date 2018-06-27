Namespace SimulationStateService

    Public Class EngineConfiguration

        Public Sub New()
            Shaft1CorrectedSpeed.Name = "NLCORR"
            Shaft1CorrectedSpeed.EngineeringUnits = "%"
            Shaft1CorrectedSpeed.Minimum = 0
            Shaft1CorrectedSpeed.Maximum = 100
            Shaft1CorrectedSpeed.HighAlarmLimit = 99999
            Shaft1CorrectedSpeed.HighCriticalAlarmLimit = 99999
            Shaft1CorrectedSpeed.LowAlarmLimit = -99999
            Shaft1CorrectedSpeed.LowCriticalLimit = -99999
            Shaft1CorrectedSpeedRoot20.Name = "NLRT120"
            Shaft1CorrectedSpeedRoot20.EngineeringUnits = "RPM/SQRT(k)"
            Shaft1CorrectedSpeedRoot20.Minimum = 0
            Shaft1CorrectedSpeedRoot20.Maximum = 99999
            Shaft1CorrectedSpeedRoot20.HighAlarmLimit = 99999
            Shaft1CorrectedSpeedRoot20.HighCriticalAlarmLimit = 99999
            Shaft1CorrectedSpeedRoot20.LowAlarmLimit = -99999
            Shaft1CorrectedSpeedRoot20.LowCriticalLimit = -99999
        End Sub

        <DataMember()>
        Public Property Shaft1 As New ShaftConfiguration
        <DataMember()>
        Public Property Shaft2 As New ShaftConfiguration
        <DataMember()>
        Public Property Shaft1CorrectedSpeed As New ParameterConfiguration
        <DataMember()>
        Public Property Shaft1CorrectedSpeedRoot20 As New ParameterConfiguration
        <DataMember()>
        Public Property TurbineGasTemperature As New ParameterConfiguration
        <DataMember()>
        Public Property OilPressure As New ParameterConfiguration
        <DataMember()>
        Public Property OilTemperature As New ParameterConfiguration
        <DataMember()>
        Public Property OilQuantity As New ParameterConfiguration
        <DataMember()>
        Public Property FuelPressure As New ParameterConfiguration
        <DataMember()>
        Public Property FuelTemperature As New ParameterConfiguration
        <DataMember()>
        Public Property ThrottleLeverAngle As New ParameterConfiguration
        <DataMember()>
        Public Property ThrottleResolverAngle As New ParameterConfiguration
        <DataMember()>
        Public Property HighPressureCompressorPressure As New ParameterConfiguration
        <DataMember()>
        Public Property HighPressureCompressorTemperature As New ParameterConfiguration
        <DataMember()>
        Public Property Thrust As New ParameterConfiguration
        <DataMember()>
        Public Property LaneABroadbandVibration As New ParameterConfiguration
        <DataMember()>
        Public Property LaneBBroadbandVibration As New ParameterConfiguration
        <DataMember()>
        Public Property Shaft1LaneAVibration As New ParameterConfiguration
        <DataMember()>
        Public Property Shaft1LaneBVibration As New ParameterConfiguration
        <DataMember()>
        Public Property Shaft2LaneAVibration As New ParameterConfiguration
        <DataMember()>
        Public Property Shaft2LaneBVibration As New ParameterConfiguration

        Public Overridable Sub Update(engine As IEngine)
            Shaft1.Update(engine.Shaft1)
            Shaft2.Update(engine.Shaft2)
            TurbineGasTemperature.Update(engine.TurbineGasTemperature)
            OilPressure.Update(engine.Oil.Pressure)
            OilTemperature.Update(engine.Oil.Temperature)
            OilQuantity.Update(engine.Oil.Quantity)
            FuelPressure.Update(engine.Fuel.Pressure)
            FuelTemperature.Update(engine.Fuel.Temperature)
            ThrottleLeverAngle.Update(engine.Throttle.LeverAngle)
            ThrottleResolverAngle.Update(engine.Throttle.ResolverAngle)
            HighPressureCompressorPressure.Update(engine.HighPressureCompressor.Pressure)
            HighPressureCompressorTemperature.Update(engine.HighPressureCompressor.Temperature)
            Thrust.Update(engine.Thrust)
            LaneABroadbandVibration.Update(engine.LaneABroadbandVibration)
            LaneBBroadbandVibration.Update(engine.LaneBBroadbandVibration)
            Shaft1LaneAVibration.Update(engine.Shaft1.LaneATrackedOrderVibration)
            Shaft1LaneBVibration.Update(engine.Shaft1.LaneBTrackedOrderVibration)
            Shaft2LaneAVibration.Update(engine.Shaft2.LaneATrackedOrderVibration)
            Shaft2LaneBVibration.Update(engine.Shaft2.LaneBTrackedOrderVibration)
        End Sub

    End Class

End Namespace