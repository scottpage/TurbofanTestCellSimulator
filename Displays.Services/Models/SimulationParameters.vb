<DataContract(), KnownType(GetType(TrentParameters))>
Public Class SimulationParameters

    <DataMember()>
    Public Property AirInletTemperature As New ParameterValue
    <DataMember()>
    Public Property Shaft1 As New ShaftValue
    <DataMember()>
    Public Property Shaft2 As New ShaftValue
    <DataMember()>
    Public Property TurbineGasTemperature As New ParameterValue
    <DataMember()>
    Public Property OilPressure As New ParameterValue
    <DataMember()>
    Public Property OilTemperature As New ParameterValue
    <DataMember()>
    Public Property OilQuantity As New ParameterValue
    <DataMember()>
    Public Property FuelPressure As New ParameterValue
    <DataMember()>
    Public Property FuelTemperature As New ParameterValue
    <DataMember()>
    Public Property ThrottleLeverAngle As New ParameterValue
    <DataMember()>
    Public Property ThrottleResolverAngle As New ParameterValue
    <DataMember()>
    Public Property HighPressureCompressorPressure As New ParameterValue
    <DataMember()>
    Public Property HighPressureCompressorTemperature As New ParameterValue
    <DataMember()>
    Public Property Thrust As New ParameterValue
    <DataMember()>
    Public Property LaneABroadbandVibration As New ParameterValue
    <DataMember()>
    Public Property LaneBBroadbandVibration As New ParameterValue
    <DataMember()>
    Public Property Shaft1LaneAVibration As New ParameterValue
    <DataMember()>
    Public Property Shaft1LaneBVibration As New ParameterValue
    <DataMember()>
    Public Property Shaft2LaneAVibration As New ParameterValue
    <DataMember()>
    Public Property Shaft2LaneBVibration As New ParameterValue

    Public Overridable Sub Update(values As SimulationParameters)
        AirInletTemperature.Update(values.AirInletTemperature)
        Shaft1.Update(values.Shaft1)
        Shaft2.Update(values.Shaft2)
        TurbineGasTemperature.Update(values.TurbineGasTemperature)
        OilPressure.Update(values.OilPressure)
        OilTemperature.Update(values.OilTemperature)
        OilQuantity.Update(values.OilQuantity)
        FuelPressure.Update(values.FuelPressure)
        FuelTemperature.Update(values.FuelTemperature)
        ThrottleLeverAngle.Update(values.ThrottleLeverAngle)
        ThrottleResolverAngle.Update(values.ThrottleResolverAngle)
        HighPressureCompressorPressure.Update(values.HighPressureCompressorPressure)
        HighPressureCompressorTemperature.Update(values.HighPressureCompressorTemperature)
        Thrust.Update(values.Thrust)
        LaneABroadbandVibration.Update(values.LaneABroadbandVibration)
        LaneBBroadbandVibration.Update(values.LaneBBroadbandVibration)
        Shaft1LaneAVibration.Update(values.Shaft1LaneAVibration)
        Shaft1LaneBVibration.Update(values.Shaft1LaneBVibration)
        Shaft2LaneAVibration.Update(values.Shaft2LaneAVibration)
        Shaft2LaneBVibration.Update(values.Shaft2LaneBVibration)
    End Sub

End Class
