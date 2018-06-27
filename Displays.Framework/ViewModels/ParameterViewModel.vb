Public Class ParameterViewModel
    Inherits ViewModelBase

    Public Sub New()
        Minimum = 0
        Maximum = 100
        HighAlarmLimit = 99999
        HighCriticalAlarmLimit = 99999
        LowAlarmLimit = -99999
        LowCriticalAlarmLimit = -99999
    End Sub

    Public Sub New(parameter As ParameterValue)
        Me.New()
        Update(parameter)
    End Sub

    Public Sub New(shaft As ShaftValue)
        Me.New()
        Me.Update(shaft)
    End Sub

    Private _Name As String
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() Name, _Name, Value)
        End Set
    End Property

    Private _EngineeringUnits As String
    Public Property EngineeringUnits As String
        Get
            Return _EngineeringUnits
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() EngineeringUnits, _EngineeringUnits, Value)
        End Set
    End Property

    Private _Value As Double
    Public Property Value As Double
        Get
            Return _Value
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() Me.Value, _Value, value)
        End Set
    End Property

    Private _Average As Double
    Public Property Average As Double
        Get
            Return _Average
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() Average, _Average, Value)
        End Set
    End Property

    Private _Quality As Quality
    Public Property Quality As Quality
        Get
            Return _Quality
        End Get
        Set(ByVal Value As Quality)
            SetProperty(Function() Quality, _Quality, Value)
        End Set
    End Property

    Private _HighAlarmLimit As Double
    Public Property HighAlarmLimit As Double
        Get
            Return _HighAlarmLimit
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() HighAlarmLimit, _HighAlarmLimit, Value)
        End Set
    End Property

    Private _HighCriticalAlarmLimit As Double
    Public Property HighCriticalAlarmLimit As Double
        Get
            Return _HighCriticalAlarmLimit
        End Get
        Set(ByVal value As Double)
            SetProperty(Function() HighCriticalAlarmLimit, _HighCriticalAlarmLimit, value)
        End Set
    End Property

    Private _LowAlarmLimit As Double
    Public Property LowAlarmLimit As Double
        Get
            Return _LowAlarmLimit
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() LowAlarmLimit, _LowAlarmLimit, Value)
        End Set
    End Property

    Private _LowCriticalAlarmLimit As Double
    Public Property LowCriticalAlarmLimit As Double
        Get
            Return _LowCriticalAlarmLimit
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() LowCriticalAlarmLimit, _LowCriticalAlarmLimit, Value)
        End Set
    End Property

    Private _Maximum As Double
    Public Property Maximum As Double
        Get
            Return _Maximum
        End Get
        Set(ByVal Value As Double)
            If Value < Minimum Then Throw New ArgumentException("Maximum must be greater than minimum.", "value")
            SetProperty(Function() Maximum, _Maximum, Value)
        End Set
    End Property

    Private _Minimum As Double
    Public Property Minimum As Double
        Get
            Return _Minimum
        End Get
        Set(ByVal Value As Double)
            If Value > Maximum Then Throw New ArgumentException("Minimum must be less than maximum.", "value")
            SetProperty(Function() Minimum, _Minimum, Value)
        End Set
    End Property

    Public Property UseValueAsAverage As Boolean
    Public Property UseCorrectedSpeedAsAverage As Boolean
    Public Property UseCorrectedSpeedRoot20AsAverage As Boolean
    Public Property AllowNameChange As Boolean = True
    Public Property AllowEngineeringUnitsChange As Boolean = True
    Public Property AllowAlarmChange As Boolean = True
    Public Property AllowRangeChange As Boolean = True

    Public Sub Update(parameter As ParameterValue)
        If AllowNameChange Then Name = parameter.Name
        If AllowEngineeringUnitsChange Then EngineeringUnits = parameter.EngineeringUnits
        If AllowAlarmChange Then
            HighAlarmLimit = parameter.HighAlarmLimit
            HighCriticalAlarmLimit = parameter.HighCriticalAlarmLimit
            LowAlarmLimit = parameter.LowAlarmLimit
            LowCriticalAlarmLimit = parameter.LowCriticalAlarmLimit
        End If
        If AllowRangeChange Then
            Maximum = parameter.Maximum
            Minimum = parameter.Minimum
        End If
        Value = parameter.Value
        If UseValueAsAverage Then
            Average = Value
        Else
            Average = parameter.Average
        End If
        Quality = parameter.Quality
    End Sub

    Public Sub Update(shaft As ShaftValue)
        If AllowNameChange Then Name = shaft.Name
        If AllowEngineeringUnitsChange Then EngineeringUnits = shaft.EngineeringUnits
        If AllowAlarmChange Then
            HighAlarmLimit = shaft.HighAlarmLimit
            HighCriticalAlarmLimit = shaft.HighCriticalAlarmLimit
            LowAlarmLimit = shaft.LowAlarmLimit
            LowCriticalAlarmLimit = shaft.LowCriticalAlarmLimit
        End If
        If AllowRangeChange Then
            Maximum = shaft.Maximum
            Minimum = shaft.Minimum
        End If
        Value = shaft.Value
        If UseValueAsAverage Then
            Average = Value
        ElseIf UseCorrectedSpeedAsAverage Then
            Average = shaft.CorrectedSpeed
        ElseIf UseCorrectedSpeedRoot20AsAverage Then
            Average = shaft.CorrectedSpeedRoot20
        Else
            Average = shaft.Average
        End If
        Quality = shaft.Quality
    End Sub

End Class
