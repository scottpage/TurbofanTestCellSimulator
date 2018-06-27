<DataContract()>
Public Class Alarm
    Inherits ViewModelBase
    Implements IAlarm

    Public Event ConfigurationChanged(sender As Object, e As ConfigurationChangedEventArgs) Implements INotifyConfigurationChanged.ConfigurationChanged

    Public Sub New(type As AlarmType, limit As Double)
        _Type = type
        _Limit = limit
    End Sub

    Private _IsAcknowledged As Boolean = True
    <DataMember()>
    Public ReadOnly Property IsAcknowledged As Boolean Implements IAlarm.IsAcknowledged
        Get
            Return _IsAcknowledged
        End Get
    End Property

    Private _IsAlarming As Boolean
    <DataMember()>
    Public ReadOnly Property IsAlarming As Boolean Implements IAlarm.IsAlarming
        Get
            Return _IsAlarming
        End Get
    End Property

    Private _MinimumMaximum As Double
    <DataMember()>
    Public ReadOnly Property MinimumMaximum As Double Implements IAlarm.MinimumMaximum
        Get
            Return _MinimumMaximum
        End Get
    End Property

    Private _Type As AlarmType
    <DataMember()>
    Public ReadOnly Property Type As AlarmType Implements IAlarm.Type
        Get
            Return _Type
        End Get
    End Property

    Private _Limit As Double
    <DataMember()>
    Public Property Limit As Double Implements IAlarm.Limit
        Get
            Return _Limit
        End Get
        Set(value As Double)
            SetProperty(Function() Limit, _Limit, value)
            OnConfigurationChanged("Limit")
        End Set
    End Property

    Public Sub Update(value As Double) Implements IAlarm.Update
        Dim WasAlarming = IsAlarming
        Select Case Type
            Case AlarmType.High
                SetIsAlarming(value >= Limit)
            Case AlarmType.HighCritical
                SetIsAlarming(value >= Limit)
            Case AlarmType.Low
                SetIsAlarming(value <= Limit)
            Case AlarmType.LowCritical
                SetIsAlarming(value <= Limit)
        End Select
        SetMinimumMaximum(Math.Max(value, MinimumMaximum))
        If Not WasAlarming And IsAcknowledged Then SetIsAcknowledged(False)
    End Sub

    Private Sub OnConfigurationChanged(propertyName As String)
        RaiseEvent ConfigurationChanged(Me, New ConfigurationChangedEventArgs(propertyName))
    End Sub

    Public Sub Acknowledge() Implements IAlarm.Acknowledge
        SetIsAcknowledged(True)
    End Sub

    Private Sub SetIsAcknowledged(value As Boolean)
        SetProperty(Function() IsAcknowledged, _IsAcknowledged, value)
    End Sub

    Private Sub SetIsAlarming(value As Boolean)
        SetProperty(Function() IsAlarming, _IsAlarming, value)
    End Sub

    Private Sub SetMinimumMaximum(value As Double)
        SetProperty(Function() MinimumMaximum, _MinimumMaximum, value)
    End Sub

End Class
