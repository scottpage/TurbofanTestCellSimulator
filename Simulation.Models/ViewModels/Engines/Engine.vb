<DataContract(), XmlInclude(GetType(Trent)), KnownType(GetType(Trent))>
Public MustInherit Class Engine
    Inherits ViewModelBase
    Implements IEngine

#Region "Fields"

    Private _Mode As EngineMode = EngineMode.None

#End Region

#Region "Constructors"

    Protected Sub New(type As String)
        Dim AdditionalShafts = GetAdditionalShafts()
        Shafts = New List(Of Shaft)
        Shaft1 = New Shaft(1, "NLPPERC")
        Shaft2 = New Shaft(2, "NHPPERC")
        Shaft1.LaneATrackedOrderVibration.Name = "A2298ALP"
        Shaft1.LaneATrackedOrderVibration.EngineeringUnits = "IN/S_PK"
        Shaft1.LaneBTrackedOrderVibration.Name = "A2298BLP"
        Shaft1.LaneBTrackedOrderVibration.EngineeringUnits = "IN/S_PK"
        Shaft2.LaneATrackedOrderVibration.Name = "A2298AHP"
        Shaft2.LaneATrackedOrderVibration.EngineeringUnits = "IN/S_PK"
        Shaft2.LaneBTrackedOrderVibration.Name = "A2298BHP"
        Shaft2.LaneBTrackedOrderVibration.EngineeringUnits = "IN/S_PK"
        Shafts.Add(Shaft1)
        Shafts.Add(Shaft2)
        If AdditionalShafts IsNot Nothing AndAlso AdditionalShafts.Count > 0 Then AdditionalShafts.ForEach(Sub(s) Shafts.Add(s))
        Starters = GetStarters()
        HighPressureCompressor = New Compressor
        HighPressureCompressor.Pressure.Name = "S0301A1"
        HighPressureCompressor.Pressure.EngineeringUnits = "psia"
        HighPressureCompressor.Temperature.Name = "T0301A1"
        HighPressureCompressor.Temperature.EngineeringUnits = "degC"
        Fuel = New FuelSystem
        Fuel.Pressure.Name = "S0985A1"
        Fuel.Pressure.EngineeringUnits = "psig"
        Fuel.Temperature.Name = "T0985A1"
        Fuel.Temperature.EngineeringUnits = "degC"
        Ignition = New IgnitionSystem
        LaneABroadbandVibration = New Parameter("A2297A1", "IN/S_PK")
        LaneBBroadbandVibration = New Parameter("A2297B1", "IN/S_PK")
        Oil = New OilSystem
        Oil.Pressure.Name = "UPOILVZ"
        Oil.Pressure.EngineeringUnits = "psig"
        Oil.Temperature.Name = "T0746A1"
        Oil.Temperature.EngineeringUnits = "degC"
        Oil.Quantity.Name = "DOILQTY"
        Oil.Quantity.EngineeringUnits = "quarts"
        Throttle = New ThrottleSystem
        Throttle.LeverAngle.Name = "DTLA"
        Throttle.LeverAngle.EngineeringUnits = "deg"
        Throttle.ResolverAngle.Name = "DTRA"
        Throttle.ResolverAngle.EngineeringUnits = "deg"
        TurbineGasTemperature = New Parameter("TGT", "degC")
        EEC = New ElectronicEngineController
        EMU = New EngineMonitoringUnit
        Thrust = New Parameter("FXG2", "lbf")
        AirInletTemperature = New Parameter("T20", "degC")
        _Type = type
        InitializeParameters()
        InitializeConfiguration()
        Parameters.SelectedParameter = Shaft1
        Update()
    End Sub

#End Region

#Region "Properties"

    <DataMember()> _
    Public Property AirInletTemperature As Parameter Implements IEngine.AirInletTemperature
    <DataMember()> _
    Public Property Shaft1 As Shaft Implements IEngine.Shaft1
    <DataMember()> _
    Public Property Shaft2 As Shaft Implements IEngine.Shaft2

    Public MustOverride ReadOnly Property PowerShaft As Shaft

    <DataMember()>
    Public Property Build As String Implements IEngine.Build
    <DataMember()>
    Public Property HighPressureCompressor As Compressor Implements IEngine.HighPressureCompressor
    <DataMember()>
    Public Property Fuel As FuelSystem Implements IEngine.Fuel
    <DataMember()>
    Public Property Ignition As IgnitionSystem Implements IEngine.Ignition
    <DataMember()>
    Public Property LaneABroadbandVibration As Parameter Implements IEngine.LaneABroadbandVibration
    <DataMember()>
    Public Property LaneBBroadbandVibration As Parameter Implements IEngine.LaneBBroadbandVibration
    <DataMember()>
    Public Property Oil As OilSystem Implements IEngine.Oil
    <DataMember()>
    Public Property PartTest As String Implements IEngine.PartTest
    <DataMember()>
    Public Property SerialNumber As String Implements IEngine.SerialNumber
    <DataMember()>
    Public Property Shafts As System.Collections.Generic.List(Of Shaft) Implements IEngine.Shafts
    <DataMember()>
    Public Property Starters As System.Collections.Generic.List(Of Starter) Implements IEngine.Starters
    <DataMember()>
    Public Property Throttle As ThrottleSystem Implements IEngine.Throttle
    <DataMember()>
    Public Property TurbineGasTemperature As Parameter Implements IEngine.TurbineGasTemperature
    <DataMember()>
    Public Property Type As String Implements IEngine.Type
    <DataMember()>
    Public Property EEC As ElectronicEngineController Implements IEngine.EEC
    <DataMember()>
    Public Property EMU As EngineMonitoringUnit Implements IEngine.EMU
    <DataMember()>
    Public Property Thrust As Parameter Implements IEngine.Thrust
    <DataMember()>
    Public MustOverride Property StartDuration As Integer Implements IEngine.StartDuration
    <DataMember()>
    Public MustOverride Property ShutdownDuration As Integer Implements IEngine.ShutdownDuration
    <DataMember()>
    Public MustOverride Property StartVideoLeadTime As Integer Implements IEngine.StartVideoLeadTime
    <DataMember()>
    Public MustOverride Property StarterCutTime As Integer Implements IEngine.StarterCutTime
    <DataMember()> _
    Public MustOverride Property HighIdleThrottleLeverAngle As Double Implements IEngine.HighIdleThrottleLeverAngle

    Private _Parameters As New ParameterListViewModel
    Public ReadOnly Property Parameters As ParameterListViewModel Implements IEngine.Parameters
        Get
            Return _Parameters
        End Get
    End Property

    <DataMember()>
    Public Property Mode As EngineMode Implements IEngine.Mode
        Get
            Return _Mode
        End Get
        Set(value As EngineMode)
            If _Mode = value Then Return
            OnPropertyChanging(Function() Mode)
            If Not value = EngineMode.AbortingStart Then
                Parameters.ToList.ForEach(Sub(p) p.Value.Mode = value)
            Else
                Parameters.ToList.ForEach(Sub(p) p.Value.Mode = EngineMode.Starting)
            End If
            _Mode = value
            OnPropertyChanged(Function() Mode)
        End Set
    End Property

#End Region

#Region "Initialization"

    Protected MustOverride Function GetAdditionalShafts() As List(Of Shaft)
    Protected MustOverride Function GetStarters() As List(Of Starter)

    Private Sub InitializeParameters()
        _Parameters.Add(HighPressureCompressor.Pressure)
        _Parameters.Add(HighPressureCompressor.Temperature)

        _Parameters.Add(Fuel.Temperature)
        _Parameters.Add(Fuel.Pressure)

        _Parameters.Add(LaneABroadbandVibration)
        _Parameters.Add(LaneBBroadbandVibration)

        _Parameters.Add(Oil.Temperature)
        _Parameters.Add(Oil.Pressure)
        _Parameters.Add(Oil.Quantity)

        _Parameters.Add(TurbineGasTemperature)

        _Parameters.Add(Thrust)

        _Parameters.Add(Throttle.LeverAngle)
        _Parameters.Add(Throttle.ResolverAngle)

        _Parameters.Add(AirInletTemperature)

        For Each Shaft In Shafts
            _Parameters.Add(Shaft.LaneATrackedOrderVibration)
            _Parameters.Add(Shaft.LaneBTrackedOrderVibration)
            _Parameters.Add(Shaft)
        Next

        For Each Starter In Starters
            Starter.GetParameters.ToList.ForEach(Sub(s) _Parameters.Add(s))
        Next
    End Sub

    Protected Overridable Sub InitializeConfiguration()
        With AirInletTemperature.Value
            .StoppedPolynomial.Bias = 24.792
            .StartPolynomial.Bias = .StoppedPolynomial.Bias
            .RunningPolynomial.Bias = .StoppedPolynomial.Bias
            .ShutdownPolynomial.Bias = .StoppedPolynomial.Bias
            .WetCrankPolynomial.Bias = .StoppedPolynomial.Bias
            .DryCrankPolynomial.Bias = .StoppedPolynomial.Bias
        End With
        With Throttle
            .LeverAngle.Value.UsePolynomials = False
            .LeverAngle.Value.StoppedPolynomial.IsNoiseEnabled = False
            .LeverAngle.Value.RunningPolynomial = .LeverAngle.Value.StoppedPolynomial
            .LeverAngle.Value.ShutdownPolynomial = .LeverAngle.Value.StoppedPolynomial
            .LeverAngle.Value.StartPolynomial = .LeverAngle.Value.StoppedPolynomial
            .LeverAngle.Value.DryCrankPolynomial = .LeverAngle.Value.StoppedPolynomial
            .LeverAngle.Value.WetCrankPolynomial = .LeverAngle.Value.StoppedPolynomial
            .ResolverAngle.Value.Minimum = .LeverAngle.Value.Minimum
            .ResolverAngle.Value.Maximum = .LeverAngle.Value.Maximum
            .ResolverAngle.Value.StoppedPolynomial.Bias = .LeverAngle.Value.StoppedPolynomial.Bias
            .ResolverAngle.Value.StoppedPolynomial.AddOrder(1, 1)
            .ResolverAngle.Value.StoppedPolynomial.Minimum = .LeverAngle.Value.StoppedPolynomial.Minimum
            .ResolverAngle.Value.StoppedPolynomial.Maximum = .LeverAngle.Value.StoppedPolynomial.Maximum
            .ResolverAngle.Value.RunningPolynomial = .ResolverAngle.Value.StoppedPolynomial
            .ResolverAngle.Value.ShutdownPolynomial = .ResolverAngle.Value.StoppedPolynomial
            .ResolverAngle.Value.StartPolynomial = .ResolverAngle.Value.StoppedPolynomial
            .ResolverAngle.Value.DryCrankPolynomial = .ResolverAngle.Value.StoppedPolynomial
            .ResolverAngle.Value.WetCrankPolynomial = .ResolverAngle.Value.StoppedPolynomial
        End With
    End Sub

#End Region

#Region "Updating"

    Public Sub Update() Implements IEngine.Update
        Dim Value = Math.Abs(Throttle.LeverAngle.Value.Current)
        If Double.IsNaN(Value) Then Value = 0
        If Mode = EngineMode.Running And EEC.IsInFlightMode And EEC.IsInHighIdleMode And Value < HighIdleThrottleLeverAngle Then
            'Pilot has lever pulled below HighIdleThrottleLeverAngle so use HighIdleThrottleLeverAngle.
            'Using Value < HighIdleThrottleLeverAngle will allow engine to accel past high idle but limit minimum parameter values to HighIdleThrottleLeverAngle values.
            Value = HighIdleThrottleLeverAngle
        End If
        Update(Value)
    End Sub

    Public Sub Update(value As Double) Implements IEngine.Update
        Shafts.ForEach(Sub(s) s.Update(value))
        AirInletTemperature.Update(value)
        HighPressureCompressor.Pressure.Update(PowerShaft.Value.Current)
        HighPressureCompressor.Temperature.Update(HighPressureCompressor.Pressure.Value.Current)
        Fuel.Update(value)
        LaneABroadbandVibration.Update(value)
        LaneBBroadbandVibration.Update(value)
        Oil.Update(value)
        Starters.ForEach(Sub(s) s.Update(value))
        Throttle.Update()
        TurbineGasTemperature.Update(value)
        Thrust.Update(value)
    End Sub

#End Region

#Region "Serialize/deserialize"

    Public Sub SaveToFile(path As String)
        Dim X As New XmlSerializer(GetType(Engine), My.Settings.DefaultXmlNamespace)
        Using S As New StreamWriter(path, False)
            X.Serialize(S, Me)
        End Using
    End Sub

    Public Shared Function LoadFromFile(path As String) As Engine
        Dim X As New XmlSerializer(GetType(Engine), My.Settings.DefaultXmlNamespace)
        Using S As New StreamReader(path)
            Return DirectCast(X.Deserialize(S), Engine)
        End Using
    End Function

#End Region

End Class
