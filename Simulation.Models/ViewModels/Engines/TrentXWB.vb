<DataContract()>
Public NotInheritable Class TrentXWB
    Inherits Trent

    Public Sub New()
        MyBase.New("Trent XWB")
    End Sub

    <DataMember()>
    Public Property Starter As PneumaticStarter

    <DataMember()>
    Public Overrides Property StartDuration As Integer
        Get
            Return 58
        End Get
        Set(value As Integer)
        End Set
    End Property

    <DataMember()>
    Public Overrides Property ShutdownDuration As Integer
        Get
            Return 145
        End Get
        Set(value As Integer)
        End Set
    End Property

    <DataMember()>
    Public Overrides Property StartVideoLeadTime As Integer
        Get
            Return 5
        End Get
        Set(value As Integer)
        End Set
    End Property

    <DataMember()>
    Public Overrides Property StarterCutTime As Integer
        Get
            Return 30
        End Get
        Set(value As Integer)
        End Set
    End Property

    Public Overrides Property HighIdleThrottleLeverAngle As Double
        Get
            Return 50
        End Get
        Set(value As Double)
        End Set
    End Property

    Private _PowerShaft As Shaft
    Public Overrides ReadOnly Property PowerShaft As Shaft
        Get
            Return _PowerShaft
        End Get
    End Property

    Protected Overrides Function GetStarters() As System.Collections.Generic.List(Of Starter)
        Dim Starters As New List(Of Starter)
        Starter = New PneumaticStarter
        Starters.Add(Starter)
        Return Starters
    End Function

#Region "Configuration"

    Protected Overrides Sub InitializeConfiguration()
        MyBase.InitializeConfiguration()
        Dim VibrationAverageSamples = 10
        Oil.Quantity.Value.NumberOfSamples = 500
        LaneABroadbandVibration.Value.NumberOfSamples = VibrationAverageSamples
        LaneBBroadbandVibration.Value.NumberOfSamples = VibrationAverageSamples
        For Each Shaft In Shafts
            Shaft.LaneATrackedOrderVibration.Value.NumberOfSamples = VibrationAverageSamples
            Shaft.LaneBTrackedOrderVibration.Value.NumberOfSamples = VibrationAverageSamples
        Next
        Shaft1.MinimumRPM = 0
        Shaft1.MaximumRPM = 2700
        Throttle.LeverAngle.Value.Minimum = 0 '-103.337
        Throttle.LeverAngle.Value.Maximum = 157.8
        Throttle.ResolverAngle.Value.Minimum = Throttle.LeverAngle.Value.Minimum
        Throttle.ResolverAngle.Value.Maximum = Throttle.LeverAngle.Value.Maximum
        InitializeShafts()
        InitializeVibration()
        InitializeTurbineGasTemperature()
        InitializeOilSystem()
        InitializeThrust()
        InitializeP30()
        InitializeT30()
        InitializeFuelPressure()
        InitializeStartAirPressure()
    End Sub

#Region "Shafts"

    Private Sub InitializeShafts()
        InitializeShaft1()
        InitializeShaft2()
        InitializeShaft3()
        _PowerShaft = Shaft3
    End Sub

    Private Sub InitializeShaft1()
        With Shaft1
            .Name = "NLPPERC"
            With .Alarms
                .High.Limit = 96.8
                .HighCritical.Limit = 97
            End With
            With .Value
                .Minimum = 0
                .Maximum = 110
                With .StoppedPolynomial
                    .IsNoiseEnabled = False
                    .Bias = 0
                End With
                With .StartPolynomial
                    .Minimum = 0
                    .Maximum = 18.893
                    .Bias = .Minimum
                    .AddOrder(0.0001280114, 2.965025)
                End With
                With .RunningPolynomial
                    .Bias = 18.893
                    .Minimum = .Bias
                    .Maximum = 110
                    .AddOrder(-0.25463, 1)
                    .AddOrder(0.025515, 2)
                    .AddOrder(-0.00026895, 3)
                    .AddOrder(0.00000095809, 4)
                End With
                With .ShutdownPolynomial
                    .Minimum = 0
                    .Maximum = 18.893
                    .Bias = .Maximum
                    .AddOrder(-4.124, 1)
                    .AddOrder(0.1496, 2)
                    .AddOrder(-0.0027, 3)
                    .AddOrder(0.00002, 4)
                    .AddOrder(-0.00000008, 5)
                End With
            End With
        End With
    End Sub

    Private Sub InitializeShaft2()
        With Shaft2
            .Name = "NIPPERC"
            With .Alarms
                .High.Limit = 99.3
                .HighCritical.Limit = 101.18
            End With
            With .Value
                .Minimum = 0
                .Maximum = 110
                With .StoppedPolynomial
                    .IsNoiseEnabled = False
                    .Bias = 0
                End With
                With .StartPolynomial
                    .Minimum = 0
                    .Maximum = 57.551
                    .Bias = .Minimum
                    .AddOrder(0.856475, 1)
                    .AddOrder(-0.1489679, 2)
                    .AddOrder(0.01336216, 3)
                    .AddOrder(-0.0005159935, 4)
                    .AddOrder(0.000009147895, 5)
                    .AddOrder(-0.00000005943873, 6)
                End With
                With .RunningPolynomial
                    .Minimum = 57.551
                    .Maximum = 110
                    .Bias = .Minimum
                    .AddOrder(0.042323, 1)
                    .AddOrder(0.01873, 2)
                    .AddOrder(-0.00027078, 3)
                    .AddOrder(0.0000011097, 4)
                End With
                With .ShutdownPolynomial
                    .Minimum = 0
                    .Maximum = 57.551
                    .Bias = .Maximum
                    .AddOrder(-4.124, 1)
                    .AddOrder(0.1496, 2)
                    .AddOrder(-0.0027, 3)
                    .AddOrder(0.00002, 4)
                    .AddOrder(-0.00000008, 5)
                End With
            End With
        End With
    End Sub

    Private Sub InitializeShaft3()
        With Shaft3
            .Name = "NHPPERC"
            With .Alarms
                .High.Limit = 97.1
                .HighCritical.Limit = 99.4
            End With
            With .Value
                .Minimum = 0
                .Maximum = 110
                With .StoppedPolynomial
                    .IsNoiseEnabled = False
                    .Bias = 0
                End With
                With .StartPolynomial
                    .IsNoiseEnabled = False
                    .Minimum = 0
                    .Maximum = 61.9
                    .Bias = .Minimum
                    .AddOrder(3.1609, 1)
                    .AddOrder(-0.17885, 2)
                    .AddOrder(0.0046601, 3)
                    .AddOrder(-0.000038214, 4)
                End With
                With .RunningPolynomial
                    .Minimum = 61.9
                    .Maximum = 110
                    .Bias = .Minimum
                    .AddOrder(0.088118, 1)
                    .AddOrder(0.0066486, 2)
                    .AddOrder(-0.000088049, 3)
                    .AddOrder(0.00000035167, 4)
                End With
                With .ShutdownPolynomial
                    .Minimum = 0
                    .Maximum = 61.9
                    .Bias = .Maximum
                    .AddOrder(-4.124, 1)
                    .AddOrder(0.1496, 2)
                    .AddOrder(-0.0027, 3)
                    .AddOrder(0.00002, 4)
                    .AddOrder(-0.00000008, 5)
                End With
            End With
        End With
    End Sub

#End Region

#Region "Vibration"

    Private Const _VibrationNoiseLevel As Double = 0.03

    Private Sub InitializeVibration()
        InitializeBroadbandVibration()
        InitializeShaft1Vibration()
        InitializeShaft2Vibration()
        InitializeShaft3Vibration()
    End Sub

    Private Sub InitializeBroadbandVibration()
        With LaneABroadbandVibration
            With .Alarms
                .High.Limit = 2.2
                .HighCritical.Limit = 4.4
            End With
            With .Value
                .Minimum = 0
                .Maximum = 5
                With .StoppedPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                End With
                With .StartPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.14401
                End With
                With .RunningPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.14401
                    .AddOrder(0.0000043091, 1)
                    .AddOrder(0.000034682, 2)
                    .AddOrder(0.00000038092, 3)
                    .AddOrder(-0.0000000091353, 4)
                    .AddOrder(0.00000000003683, 5)
                End With
                With .ShutdownPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Maximum = 0.14401
                    .Bias = .Maximum
                    .AddOrder(-0.0112, 1)
                    .AddOrder(0.0003, 2)
                    .AddOrder(-0.000003, 3)
                    .AddOrder(-0.00000001, 4)
                    .AddOrder(0.0000000005, 5)
                    .AddOrder(-0.000000000003, 6)
                End With
            End With
        End With
        With LaneBBroadbandVibration
            .Alarms = LaneABroadbandVibration.Alarms
            .Value.Copy(LaneABroadbandVibration.Value)
        End With
    End Sub

    Private Sub InitializeShaft1Vibration()
        With Shaft1.LaneATrackedOrderVibration
            With .Alarms
                .High.Limit = 1.5
                .HighCritical.Limit = 2
            End With
            With .Value
                .Minimum = 0
                .Maximum = 2.5
                With .StoppedPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                End With
                With .StartPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.0071366
                End With
                With .RunningPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.0071366
                    .AddOrder(0.0026737, 1)
                    .AddOrder(-0.000048373, 2)
                    .AddOrder(0.00000025236, 3)
                    .AddOrder(0.00000000051306, 4)
                End With
                With .ShutdownPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.0071366
                End With
            End With
        End With
        With Shaft1.LaneBTrackedOrderVibration
            .Alarms = Shaft1.LaneATrackedOrderVibration.Alarms
            .Value.Copy(Shaft1.LaneATrackedOrderVibration.Value)
        End With
    End Sub

    Private Sub InitializeShaft2Vibration()
        With Shaft2.LaneATrackedOrderVibration
            With .Alarms
                .High.Limit = 1.2
                .HighCritical.Limit = 2.4
            End With
            With .Value
                .Minimum = 0
                .Maximum = 3
                With .StoppedPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                End With
                With .StartPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.045884
                End With
                With .RunningPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.045884
                    .AddOrder(0.0026737, 1)
                    .AddOrder(-0.000048373, 2)
                    .AddOrder(0.00000025236, 3)
                    .AddOrder(0.00000000051306, 4)
                End With
                With .ShutdownPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.045884
                End With
            End With
        End With
        With Shaft2.LaneBTrackedOrderVibration
            .Alarms = Shaft2.LaneATrackedOrderVibration.Alarms
            .Value.Copy(Shaft2.LaneATrackedOrderVibration.Value)
        End With
    End Sub

    Private Sub InitializeShaft3Vibration()
        With Shaft3.LaneATrackedOrderVibration
            With .Alarms
                .High.Limit = 1
                .HighCritical.Limit = 3
            End With
            With .Value
                .Minimum = 0
                .Maximum = 3.5
                With .StoppedPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0
                End With
                With .StartPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.087166
                End With
                With .RunningPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.087166
                    .AddOrder(0.00045307, 1)
                    .AddOrder(0.000004801, 2)
                End With
                With .ShutdownPolynomial
                    .NoiseLevel = _VibrationNoiseLevel
                    .Bias = 0.087166
                End With
            End With
        End With
        With Shaft3.LaneBTrackedOrderVibration
            .Alarms = Shaft3.LaneATrackedOrderVibration.Alarms
            .Value.Copy(Shaft3.LaneATrackedOrderVibration.Value)
        End With
    End Sub

#End Region

#Region "Oil System"

    Private Sub InitializeOilSystem()
        InitializeOilPressure()
        InitializeOilTemperature()
        InitializeOilQuantity()
    End Sub

    Private Sub InitializeOilPressure()
        With Oil.Pressure
            With .Alarms
                .High.Limit = 275
                .HighCritical.Limit = 250
            End With
            With .Value
                .Minimum = 0
                .Maximum = 300
                With .StoppedPolynomial
                    .Bias = 0
                End With
                With .StartPolynomial
                    .Minimum = 5
                    .Maximum = 122.65
                    .Bias = .Minimum
                    .AddOrder(0.1, 1)
                End With
                With .RunningPolynomial
                    .Bias = 122.65
                    .AddOrder(1.0742, 1)
                    .AddOrder(-0.0058629, 2)
                End With
                With .ShutdownPolynomial
                    .Minimum = 0
                    .Maximum = 122.65
                    .Bias = .Maximum
                    .AddOrder(-12.85, 1)
                    .AddOrder(0.6645, 2)
                    .AddOrder(-0.0186, 3)
                    .AddOrder(0.0003, 4)
                    .AddOrder(-0.000002, 5)
                    .AddOrder(0.000000007, 6)
                End With
            End With
        End With
    End Sub

    Private Sub InitializeOilTemperature()
        With Oil.Temperature
            With .Alarms
                .High.Limit = 225
                .HighCritical.Limit = 200
            End With
            With .Value
                .Minimum = 0
                .Maximum = 275
                With .StoppedPolynomial
                    .Bias = 26.9
                End With
                With .StartPolynomial
                    .Minimum = 27
                    .Maximum = 80
                    .Bias = .Minimum
                End With
                With .RunningPolynomial
                    .Bias = 80
                    .AddOrder(1.0742, 1)
                    .AddOrder(-0.0058629, 2)
                End With
                With .ShutdownPolynomial
                    .Minimum = 27
                    .Maximum = 80
                    .Bias = .Maximum
                    .AddOrder(-12.85, 1)
                    .AddOrder(0.6645, 2)
                    .AddOrder(-0.0186, 3)
                    .AddOrder(0.0003, 4)
                    .AddOrder(-0.000002, 5)
                    .AddOrder(0.000000007, 6)
                End With
            End With
        End With
    End Sub

    Private Sub InitializeOilQuantity()
        With Oil.Quantity
            With .Alarms
                .Low.Limit = 6
                .LowCritical.Limit = 3
            End With
            With .Value
                .Minimum = 0
                .Maximum = 20
                With .StoppedPolynomial
                    .Bias = 17.5
                End With
                With .RunningPolynomial
                    .Bias = 6.2644
                    .AddOrder(-0.027957, 1)
                    .AddOrder(0.00003241, 2)
                End With
                With .ShutdownPolynomial
                    .Minimum = 6.2644
                    .Maximum = 17.5
                    .Bias = .Minimum
                    .AddOrder(0.3264, 1)
                    .AddOrder(-0.0082, 2)
                    .AddOrder(0.0002, 3)
                    .AddOrder(-0.000002, 4)
                    .AddOrder(0.00000001, 5)
                    .AddOrder(-0.00000000002, 6)
                End With
            End With
        End With
    End Sub

#End Region

    Private Sub InitializeTurbineGasTemperature()
        With TurbineGasTemperature
            With .Alarms
                .High.Limit = 912
                .HighCritical.Limit = 924
            End With
            With .Value
                .Minimum = 0
                .Maximum = 1100
                With .StoppedPolynomial
                    .Bias = 67.617
                End With
                With .StartPolynomial
                    .IsNoiseEnabled = False
                    .Minimum = 67.617
                    .Maximum = 99999
                    .Bias = .Minimum
                    .AddOrder(2.5682, 1)
                    .AddOrder(-0.7359, 2)
                    .AddOrder(0.040961, 3)
                    .AddOrder(-0.0004717, 4)
                End With
                With .RunningPolynomial
                    .NoiseLevel = 2
                    .Minimum = 461.89
                    .Maximum = 1100
                    .Bias = .Minimum
                    .AddOrder(2.7781, 1)
                    .AddOrder(-0.055269, 2)
                    .AddOrder(0.00079812, 3)
                    .AddOrder(-0.0000023491, 4)
                End With
                With .ShutdownPolynomial
                    .Minimum = 67.617
                    .Maximum = 461.89
                    .Bias = .Maximum
                    .AddOrder(-4.4969, 1)
                    .AddOrder(0.0384, 2)
                    .AddOrder(-0.0002, 3)
                End With
            End With
        End With
    End Sub

    Private Sub InitializeThrust()
        With Thrust
            With .Alarms
                .High.Limit = 200000
                .HighCritical.Limit = 200000
            End With
            With .Value
                .Minimum = 0
                .Maximum = 100000
                With .StoppedPolynomial
                    .NoiseLevel = 5
                    .Bias = 0
                End With
                With .StartPolynomial
                    .Minimum = -25.5
                    .Maximum = 3045.1
                    .Bias = .Minimum
                    .AddOrder(18.456, 1)
                    .AddOrder(-2.5915, 2)
                    .AddOrder(0.16248, 3)
                    .AddOrder(-0.0045182, 4)
                    .AddOrder(0.000051222, 5)
                End With
                With .RunningPolynomial
                    .NoiseLevel = 25
                    .Minimum = 3045.1
                    .Maximum = 100000
                    .Bias = .Minimum
                    .AddOrder(-60.116, 1)
                    .AddOrder(5.6316, 2)
                End With
                With .ShutdownPolynomial
                    .Minimum = -25.5
                    .Maximum = 3045.1
                    .Bias = .Maximum
                    .AddOrder(-420.7, 1)
                    .AddOrder(26.112, 2)
                    .AddOrder(-0.8008, 3)
                    .AddOrder(0.0128, 4)
                    .AddOrder(-0.0001, 5)
                    .AddOrder(0.0000003, 6)
                End With
            End With
        End With
    End Sub

    Private Sub InitializeT30()
        With HighPressureCompressor.Temperature
            With .Alarms
                .HighCritical.Limit = 714
            End With
            With .Value
                .Minimum = 0
                .Maximum = 750
                With .StoppedPolynomial
                    .Bias = 24.2
                End With
                With .StartPolynomial
                    .IsNoiseEnabled = False
                    .Minimum = 24.2
                    .Maximum = 204.89
                    .Bias = .Minimum
                    .AddOrder(0.16732, 1)
                    .AddOrder(0.034702, 2)
                    .AddOrder(-0.0019889, 3)
                    .AddOrder(0.00004665, 4)
                End With
                'T30 vs TLA
                'With .RunningPolynomial
                '    .Minimum = 204.89
                '    .Maximum = 99999
                '    .Bias = .Minimum
                '    .AddOrder(2.8054, 1)
                '    .AddOrder(0.0048941, 2)
                'End With
                'T30 vs P30
                With .RunningPolynomial
                    .Minimum = 106.02165486
                    .Maximum = 99999
                    .Bias = .Minimum
                    .AddOrder(2.0900468, 1)
                    .AddOrder(-0.00391104, 2)
                    .AddOrder(0.00000317, 3)
                End With
                With .ShutdownPolynomial
                    .Minimum = 25.8
                    .Maximum = 204.89
                    .Bias = .Maximum
                    .AddOrder(-19.336, 1)
                    .AddOrder(0.9034, 2)
                    .AddOrder(-0.0222, 3)
                    .AddOrder(0.0003, 4)
                    .AddOrder(-0.000002, 5)
                    .AddOrder(0.000000006, 6)
                End With
            End With
        End With
    End Sub

    Private Sub InitializeP30()
        With HighPressureCompressor.Pressure
            With .Alarms
                .HighCritical.Limit = 792
            End With
            With .Value
                .Minimum = 0
                .Maximum = 810
                With .StoppedPolynomial
                    .Bias = 14.632
                End With
                With .StartPolynomial
                    .IsNoiseEnabled = False
                    .Bias = 14.632
                    .AddOrder(0.26356, 1)
                    .AddOrder(-0.0093996, 2)
                    .AddOrder(0.000081985, 3)
                    .AddOrder(0.0000076977, 4)
                End With
                'P30 vs TLA
                'With .RunningPolynomial
                '    .NoiseLevel = 2
                '    .Bias = 54.428
                '    .AddOrder(0.62956, 1)
                '    .AddOrder(0.031407, 2)
                '    .AddOrder(-0.0000062477, 3)
                'End With
                'P30 vs NH
                With .RunningPolynomial
                    .NoiseLevel = 5
                    .Bias = 2856.1263
                    .AddOrder(-89.64336, 1)
                    .AddOrder(0.71945, 2)
                End With
                With .ShutdownPolynomial
                    .Minimum = 14.632
                    .Maximum = 54.428
                    .Bias = .Maximum
                    .AddOrder(-5.9815, 1)
                    .AddOrder(0.3955, 2)
                    .AddOrder(-0.0126, 3)
                    .AddOrder(0.0002, 4)
                    .AddOrder(-0.000002, 5)
                    .AddOrder(0.000000005, 6)
                End With
            End With
        End With
    End Sub

    Private Sub InitializeFuelPressure()
        With Fuel.Pressure
            With .Alarms
                .HighCritical.Limit = 55
                .LowCritical.Limit = 5
            End With
            With .Value
                .Minimum = 0
                .Maximum = 70
                With .StoppedPolynomial
                    .Bias = 50.35
                End With
                With .StartPolynomial
                    .NoiseLevel = 0.2
                    .Bias = 48.35
                End With
                With .RunningPolynomial
                    .Minimum = 0
                    .Bias = 48.231
                    .AddOrder(-0.075458, 1)
                    .AddOrder(0.002046, 2)
                    .AddOrder(-0.000022855, 3)
                End With
                With .ShutdownPolynomial
                    .Minimum = 0
                    .Maximum = 48.231
                    .Bias = .Maximum
                End With
            End With
        End With
    End Sub

    Private Sub InitializeStartAirPressure()
        With Starter.DeliveryPressure
            .Name = "S1907"
            .EngineeringUnits = "psia"
            With .Value
                With .StoppedPolynomial
                    .Bias = 14.6878
                End With
                With .StartPolynomial
                    .Bias = 43.185
                End With
                With .RunningPolynomial
                    .Bias = 14.6878
                End With
                With .ShutdownPolynomial
                    .Bias = 14.6878
                End With
            End With
        End With

    End Sub

#End Region

End Class
