Namespace SimulationStateService

    Public Class SimulationParameters

        Public Sub New()
            AirInletTemperature = New ParameterValue
            Shaft1 = New ShaftValue
            Shaft2 = New ShaftValue
            TurbineGasTemperature = New ParameterValue
            OilPressure = New ParameterValue
            OilTemperature = New ParameterValue
            OilQuantity = New ParameterValue
            FuelPressure = New ParameterValue
            FuelTemperature = New ParameterValue
            ThrottleLeverAngle = New ParameterValue
            ThrottleResolverAngle = New ParameterValue
            HighPressureCompressorPressure = New ParameterValue
            HighPressureCompressorTemperature = New ParameterValue
            Thrust = New ParameterValue
            LaneABroadbandVibration = New ParameterValue
            LaneBBroadbandVibration = New ParameterValue
            Shaft1LaneAVibration = New ParameterValue
            Shaft1LaneBVibration = New ParameterValue
            Shaft2LaneAVibration = New ParameterValue
            Shaft2LaneBVibration = New ParameterValue
        End Sub

        Public Overridable Sub Update(engine As IEngine)
            AirInletTemperature.Update(engine.AirInletTemperature)
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