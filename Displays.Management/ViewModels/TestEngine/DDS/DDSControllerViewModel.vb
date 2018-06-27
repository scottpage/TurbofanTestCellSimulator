Public Class DDSControllerViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _ManoeuvreStartTimestamp As DateTime
    Private _TestDefinition As String = "M000030T0033C01_000003.cits"
    Private _AvailableTime As Integer = 1069
    Private _ElapsedTime As Integer
    Private _History As Integer
    Private _IsManoeuvreRunning As Boolean
    Private _CurrentExtractId As String
    Private _CurrentDTLog As TransientLogViewModel
    Private _IsCurrentManoeuvreScheduled As Boolean
    Private _StartScheduledManoeuvreCommand As ICommand = New RelayCommand(AddressOf StartScheduledManoeuvre, AddressOf CanStartScheduledManoeuvre)
    Private _StartUnscheduledManoeuvreCommand As ICommand = New RelayCommand(AddressOf StartUnscheduledManoeuvre, AddressOf CanStartUnscheduledManoeuvre)
    Private _StopManoeuvreCommand As ICommand = New RelayCommand(AddressOf StopManoeuvre, AddressOf CanStopManoeuvre)
    Private _UnscheduledManoeuvreTimer As DispatcherTimer

#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property StartScheduledManoeuvreCommand As ICommand
        Get
            Return _StartScheduledManoeuvreCommand
        End Get
    End Property

    Public ReadOnly Property StartUnscheduledManoeuvreCommand As ICommand
        Get
            Return _StartUnscheduledManoeuvreCommand
        End Get
    End Property

    Public ReadOnly Property StopManoeuvreCommand As ICommand
        Get
            Return _StopManoeuvreCommand
        End Get
    End Property

    Public Property TestDefinition As String
        Get
            Return _TestDefinition
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() TestDefinition, _TestDefinition, Value)
        End Set
    End Property

    Public Property AvailableTime As Integer
        Get
            Return _AvailableTime
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() AvailableTime, _AvailableTime, Value)
        End Set
    End Property

    Public Property ElapsedTime As Integer
        Get
            Return _ElapsedTime
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() ElapsedTime, _ElapsedTime, Value)
        End Set
    End Property

    Public Property History As Integer
        Get
            Return _History
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() History, _History, Value)
        End Set
    End Property

    Public Property IsManoeuvreRunning As Boolean
        Get
            Return _IsManoeuvreRunning
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsManoeuvreRunning, _IsManoeuvreRunning, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        StopManoeuvre(Nothing)
    End Sub

    Private Function CanStartScheduledManoeuvre(obj As Object) As Boolean
        Return Not IsManoeuvreRunning
    End Function

    Private Sub StartScheduledManoeuvre(obj As Object)
        IsManoeuvreRunning = True
        _IsCurrentManoeuvreScheduled = True
        _ManoeuvreStartTimestamp = DateTime.Now
        AddTestDiaryComment(True)
        StartTransientLog()
    End Sub

    Private Function CanStartUnscheduledManoeuvre(obj As Object) As Boolean
        Return Not IsManoeuvreRunning And ElapsedTime > 0
    End Function

    Private Sub StartUnscheduledManoeuvre(obj As Object)
        _UnscheduledManoeuvreTimer = New DispatcherTimer(TimeSpan.FromMinutes(ElapsedTime), DispatcherPriority.Normal, AddressOf UnscheduledManoeuvreCompleted, Dispatcher.CurrentDispatcher)
        IsManoeuvreRunning = True
        _ManoeuvreStartTimestamp = DateTime.Now
        _IsCurrentManoeuvreScheduled = False
        AddTestDiaryComment(True)
        StartTransientLog()
        _UnscheduledManoeuvreTimer.Start()
    End Sub

    Private Sub UnscheduledManoeuvreCompleted(sender As Object, e As EventArgs)
        StopManoeuvre(Nothing)
    End Sub

    Private Function CanStopManoeuvre(obj As Object) As Boolean
        Return True
    End Function

    Private Sub StopManoeuvre(obj As Object)
        If _UnscheduledManoeuvreTimer IsNot Nothing Then
            _UnscheduledManoeuvreTimer.Stop()
            _UnscheduledManoeuvreTimer = Nothing
        End If
        If IsManoeuvreRunning Then
            Dim Duration = DateTime.Now.Subtract(_ManoeuvreStartTimestamp)
            AvailableTime -= CInt(Duration.TotalMinutes)
            History += CInt(Duration.TotalMinutes)
            IsManoeuvreRunning = False
            AddTestDiaryComment(False)
            StopTransientLog()
        End If
    End Sub

    Private Sub StartTransientLog()
        _CurrentDTLog = MainViewModel.Instance.TestEngine.TransientLogController.StartDTLog
    End Sub

    Private Sub StopTransientLog()
        MainViewModel.Instance.TestEngine.TransientLogController.StopLog(_CurrentDTLog)
    End Sub

    Private Sub AddTestDiaryComment(started As Boolean)
        Dim TestDiaryComment = "DDS "
        If started Then
            _CurrentExtractId = GenerateManoeuvreExtractId()
            TestDiaryComment = String.Concat(TestDiaryComment, "starts ")
        Else
            TestDiaryComment = String.Concat(TestDiaryComment, "stops ")
        End If
        If _IsCurrentManoeuvreScheduled Then
            TestDiaryComment = String.Concat(TestDiaryComment, "Scheduled ")
        Else
            TestDiaryComment = String.Concat(TestDiaryComment, "Unscheduled ")
        End If
        TestDiaryComment = String.Concat(TestDiaryComment, "manoeuvre.  Manoeuvre ID is ", _CurrentExtractId, ".index.")
        MainViewModel.Instance.TestEngine.TestDiary.Add(TestDiaryComment)
        MainViewModel.Instance.UnifiedEventLog.Entries.Add(New UnifiedEventLogEntry("MGT", TestDiaryComment))
    End Sub

    Private Function GenerateManoeuvreExtractId() As String
        Return DateTime.Now.ToString("hhmmssddMMyy")
    End Function

#End Region

End Class
