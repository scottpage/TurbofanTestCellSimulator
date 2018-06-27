Public Class AlarmValueViewModel
    Inherits ViewModelBase

    Public Sub New()
    End Sub

    Public Sub New(alarm As AlarmValue)
        Update(alarm)
    End Sub

    Private _ParameterName As String
    Public Property ParameterName As String
        Get
            Return _ParameterName
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() ParameterName, _ParameterName, Value)
        End Set
    End Property

    Private _Type As AlarmType
    Public Property Type As AlarmType
        Get
            Return _Type
        End Get
        Set(ByVal Value As AlarmType)
            SetProperty(Function() Type, _Type, Value)
        End Set
    End Property

    Private _Limit As Double
    Public Property Limit As Double
        Get
            Return _Limit
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() Limit, _Limit, Value)
        End Set
    End Property

    Private _CurrentValue As Double
    Public Property CurrentValue As Double
        Get
            Return _CurrentValue
        End Get
        Set(value As Double)
            SetProperty(Function() CurrentValue, _CurrentValue, value)
        End Set
    End Property

    Private _IsAcknowledged As Boolean
    Public Property IsAcknowledged As Boolean
        Get
            Return _IsAcknowledged
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsAcknowledged, _IsAcknowledged, Value)
        End Set
    End Property

    Private _MinimumMaximum As Double
    Public Property MinimumMaximum As Double
        Get
            Return _MinimumMaximum
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() MinimumMaximum, _MinimumMaximum, Value)
        End Set
    End Property

    Private _MaximumDeviation As Double
    Public Property MaximumDeviation As Double
        Get
            Return _MaximumDeviation
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() MaximumDeviation, _MaximumDeviation, Value)
        End Set
    End Property

    Public Sub Update(alarm As AlarmValue)
        ParameterName = alarm.ParameterName
        Type = alarm.Type
        Limit = alarm.Limit
        CurrentValue = alarm.CurrentValue
        MinimumMaximum = alarm.MinimumMaximum
        IsAcknowledged = alarm.IsAcknowledged
    End Sub

End Class
