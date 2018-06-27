Imports System.Windows.Threading
Imports System.Collections.ObjectModel
Imports System.ServiceModel

Public Class AlarmPanelViewModel
    Inherits RealTimeDisplayViewModel

#Region "Fields"

    Private Shared _Instance As AlarmPanelViewModel
    Private _UpdateTimer As DispatcherTimer
    Private _Alarms As New ObservableCollection(Of AlarmValueViewModel)
    Private _SelectedAlarm As AlarmValueViewModel
    Private _AcknowledgeSelectedAlarmCommand As ICommand
    Private _AcknowledgeAllAlarmsCommand As ICommand
    Private _AlarmVolume As Double

#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Public Shared ReadOnly Property Instance As AlarmPanelViewModel
        Get
            If _Instance Is Nothing Then _Instance = New AlarmPanelViewModel
            Return _Instance
        End Get
    End Property

    Public ReadOnly Property IsAlarmSoundPlaying As Boolean
        Get
            Return AlarmVolume > 0
        End Get
    End Property

    Public ReadOnly Property AlarmSoundUri As Uri
        Get
            Return New Uri(My.Settings.AlarmSoundFilePath)
        End Get
    End Property

    Public Property AlarmVolume As Double
        Get
            Return _AlarmVolume
        End Get
        Set(ByVal Value As Double)
            If _AlarmVolume = Value Then Return
            SetProperty(Function() AlarmVolume, _AlarmVolume, Value)
        End Set
    End Property

    Public ReadOnly Property Alarms As ObservableCollection(Of AlarmValueViewModel)
        Get
            Return _Alarms
        End Get
    End Property

    Public Property SelectedAlarm As AlarmValueViewModel
        Get
            Return _SelectedAlarm
        End Get
        Set(value As AlarmValueViewModel)
            SetProperty(Function() SelectedAlarm, _SelectedAlarm, value)
        End Set
    End Property

    Public ReadOnly Property AcknowledgeSelectedAlarmCommand As ICommand
        Get
            If _AcknowledgeSelectedAlarmCommand Is Nothing Then _AcknowledgeSelectedAlarmCommand = New RelayCommand(AddressOf AcknowledgeSelectedAlarm, AddressOf CanAcknowledgeSelectedAlarm)
            Return _AcknowledgeSelectedAlarmCommand
        End Get
    End Property

    Public ReadOnly Property AcknowledgeAllAlarmsCommand As ICommand
        Get
            If _AcknowledgeAllAlarmsCommand Is Nothing Then _AcknowledgeAllAlarmsCommand = New RelayCommand(AddressOf AcknowledgeAllAlarms, AddressOf CanAcknowledgeAllAlarms)
            Return _AcknowledgeAllAlarmsCommand
        End Get
    End Property

    Public ReadOnly Property HasUnacknowledgedAlarms As Boolean
        Get
            Return Alarms.Where(Function(a) Not a.IsAcknowledged).Count > 0
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub UpdateAlarms(alarms As IEnumerable(Of AlarmValue))
        Dim TempSelectedAlarm = SelectedAlarm
        Dim AlarmsToRemove As New List(Of AlarmValueViewModel)
        Dim HadUnacknowledgedAlarms = HasUnacknowledgedAlarms
        Dim NewAlarms As New List(Of AlarmValueViewModel)
        For Each LoopAlarm In alarms
            Dim Alarm = LoopAlarm
            Dim ExistingAlarmVM = _Alarms.FirstOrDefault(Function(a) a.ParameterName.Equals(Alarm.ParameterName))
            If ExistingAlarmVM Is Nothing Then
                Dim NewAlarm As New AlarmValueViewModel(Alarm)
                NewAlarms.Add(NewAlarm)
                _Alarms.Add(NewAlarm)
            Else
                ExistingAlarmVM.Update(Alarm)
            End If
        Next
        For Each LoopAlarmVM In _Alarms
            Dim AlarmVM = LoopAlarmVM
            Dim ExistingAlarm = alarms.FirstOrDefault(Function(a) a.ParameterName.Equals(AlarmVM.ParameterName))
            If ExistingAlarm Is Nothing Then
                AlarmsToRemove.Add(AlarmVM)
                If AlarmVM Is TempSelectedAlarm Then SelectedAlarm = Nothing
            End If
        Next
        AlarmsToRemove.ForEach(Sub(a) _Alarms.Remove(a))
        If SelectedAlarm Is Nothing Then SelectedAlarm = _Alarms.FirstOrDefault
        If (Not HadUnacknowledgedAlarms And HasUnacknowledgedAlarms) Or (HadUnacknowledgedAlarms And Not HasUnacknowledgedAlarms) Then OnPropertyChanged(Function() HasUnacknowledgedAlarms)
        If HasUnacknowledgedAlarms Then
            StartPlayingAlarmSound()
        Else
            StopPlayingAlarmSound()
        End If
    End Sub

    Private Function CanAcknowledgeSelectedAlarm(obj As Object) As Boolean
        Return SelectedAlarm IsNot Nothing AndAlso Not SelectedAlarm.IsAcknowledged
    End Function

    Private Sub AcknowledgeSelectedAlarm(obj As Object)
        Dim Client As AlarmServiceClient = Nothing
        Try
            Client = New AlarmServiceClient
            Client.AcknowledgeAsync(SelectedAlarm.ParameterName)
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

    Private Function CanAcknowledgeAllAlarms(obj As Object) As Boolean
        Return HasUnacknowledgedAlarms
    End Function

    Private Sub AcknowledgeAllAlarms(obj As Object)
        Dim Client As AlarmServiceClient = Nothing
        Try
            Client = New AlarmServiceClient
            For Each Alarm In Alarms
                Client.AcknowledgeAsync(Alarm.ParameterName)
            Next
            Client.Close()
        Catch ex As CommunicationException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As TimeoutException
            If Client IsNot Nothing Then Client.Abort()
        Catch ex As Exception
            If Client IsNot Nothing Then Client.Abort()
        End Try
    End Sub

    Public Sub StartPlayingAlarmSound()
        AlarmVolume = 1
    End Sub

    Private Sub StopPlayingAlarmSound()
        AlarmVolume = 0
    End Sub

#End Region

End Class
