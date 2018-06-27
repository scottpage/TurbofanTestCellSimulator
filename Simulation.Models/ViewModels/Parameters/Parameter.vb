Public Class Parameter
    Inherits ViewModelBase
    Implements IParameter
    Implements IComparable

    Public Event ConfigurationChanged(sender As Object, e As ConfigurationChangedEventArgs) Implements INotifyConfigurationChanged.ConfigurationChanged

    Public Sub New()
        ConfigurationVersion = Integer.MinValue
        Alarms = New Alarms
        Value = New Value
        AddHandler Value.ConfigurationChanged, AddressOf Value_ConfigurationChanged
        AddHandler Alarms.High.ConfigurationChanged, AddressOf Alarm_ConfigurationChanged
        AddHandler Alarms.HighCritical.ConfigurationChanged, AddressOf Alarm_ConfigurationChanged
        AddHandler Alarms.Low.ConfigurationChanged, AddressOf Alarm_ConfigurationChanged
        AddHandler Alarms.LowCritical.ConfigurationChanged, AddressOf Alarm_ConfigurationChanged
    End Sub

    Public Sub New(name As String, engineeringUnits As String)
        Me.New()
        _Name = name
        _EngineeringUnits = engineeringUnits
    End Sub

    Public Property Alarms As Alarms Implements IParameter.Alarms

    Public Property IsAlarming As Boolean Implements IParameter.IsAlarming
        Get
            Return Alarms.GetAlarmingAlarm IsNot Nothing
        End Get
        Set(value As Boolean)
        End Set
    End Property

    Private _Name As String
    Public Property Name As String Implements IParameter.Name
        Get
            Return _Name
        End Get
        Set(value As String)
            SetProperty(Function() Name, _Name, value)
        End Set
    End Property

    Private _EngineeringUnits As String
    Public Property EngineeringUnits As String Implements IParameter.EngineeringUnits
        Get
            Return _EngineeringUnits
        End Get
        Set(value As String)
            SetProperty(Function() EngineeringUnits, _EngineeringUnits, value)
        End Set
    End Property

    Private _Value As IValue = New Value
    Public Property Value As IValue Implements IParameter.Value
        Get
            Return _Value
        End Get
        Set(value As IValue)
            SetProperty(Function() value, _Value, value)
        End Set
    End Property

    Public Property ConfigurationVersion As Integer Implements IParameter.ConfigurationVersion

    Public Overridable Sub Update(value As Double) Implements IParameter.Update
        _Value.Update(value)
        Alarms.Update(_Value.Average)
    End Sub

    Private Sub OnConfigurationChanged(propertyName As String)
        UpdateConfigurationVersion()
        RaiseEvent ConfigurationChanged(Me, New ConfigurationChangedEventArgs(propertyName))
    End Sub

    Private Sub Value_ConfigurationChanged(sender As Object, e As ConfigurationChangedEventArgs)
        UpdateConfigurationVersion()
        RaiseEvent ConfigurationChanged(Me, e)
    End Sub

    Private Sub Alarm_ConfigurationChanged(sender As Object, e As ConfigurationChangedEventArgs)
        UpdateConfigurationVersion()
        RaiseEvent ConfigurationChanged(Me, e)
    End Sub

    Private Sub UpdateConfigurationVersion()
        If ConfigurationVersion.Equals(Integer.MaxValue) Then ConfigurationVersion = Integer.MinValue
        ConfigurationVersion += 1
    End Sub

    Public Function CompareTo(obj As Object) As Integer Implements System.IComparable.CompareTo
        If Not TypeOf obj Is IParameter Then Return -1
        Dim P = DirectCast(obj, IParameter)
        If Name Is Nothing Or P.Name Is Nothing Then Return -1
        Return Name.CompareTo(DirectCast(obj, IParameter).Name)
    End Function

End Class
