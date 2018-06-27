Public Class FullsetControllerViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _Fullset As New FullsetViewModel
    Private _StartCommand As New RelayCommand(AddressOf Start, AddressOf CanStart)
    Private _AvailableComments As New ObservableCollection(Of String)
    Private _SelectedAvailableComment As String
    Private _ScanTypes As ObservableCollection(Of String)
    Private _FullsetTimer As DispatcherTimer
    Private _IsFullsetRunning As Boolean

#End Region

#Region "Constructors"

    Public Sub New()
        _ScanTypes = New ObservableCollection(Of String)(GetScanTypes)
        Fullset.ScanType = "A"
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property Fullset As FullsetViewModel
        Get
            Return _Fullset
        End Get
    End Property

    Public ReadOnly Property StartCommand As ICommand
        Get
            Return _StartCommand
        End Get
    End Property

    Public ReadOnly Property AvailableComments As ObservableCollection(Of String)
        Get
            Return _AvailableComments
        End Get
    End Property

    Public Property SelectedAvailableComment As String
        Get
            Return _SelectedAvailableComment
        End Get
        Set(value As String)
            SetProperty(Function() SelectedAvailableComment, _SelectedAvailableComment, value)
        End Set
    End Property

    Public ReadOnly Property ScanTypes As ObservableCollection(Of String)
        Get
            Return _ScanTypes
        End Get
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        [Stop]()
    End Sub

    Public Sub StartFullset(fullset As FullsetViewModel)
        Dim Errored As Boolean = False
        Dim C As New ManagementServiceClient
        Dim Timer As DispatcherTimer = Nothing
        Dim CompletedHandler As New EventHandler(Sub(s, e)
                                                     Dim FullsetComment = String.Format("""{0}""", fullset.Comment)
                                                     Dim TestDiaryComment = String.Concat("SS", fullset.ToString, " ", "EXTRACT ID: ", fullset.ExtractId, FullsetComment)
                                                     Timer.Stop()
                                                     Try
                                                         C.StopFullsetAsync()
                                                         MainViewModel.Instance.TestEngine.TestDiary.Add(TestDiaryComment)
                                                         MainViewModel.Instance.UnifiedEventLog.Entries.Add(New UnifiedEventLogEntry("MGT", TestDiaryComment))
                                                     Catch
                                                     Finally
                                                         Try
                                                             C.Close()
                                                         Catch
                                                         End Try
                                                         C = Nothing
                                                     End Try
                                                     Me.Fullset.IsRunning = False
                                                 End Sub)
        Timer = New DispatcherTimer(TimeSpan.FromSeconds(30), DispatcherPriority.Normal, CompletedHandler, Dispatcher.CurrentDispatcher)
        Try
            C.StartFullsetAsync(fullset.Comment)
        Catch
            Errored = True
        Finally
            Try
                C.Close()
            Catch
            End Try
            C = Nothing
        End Try
        If Not Errored Then
            Me.Fullset.IsRunning = True
            Timer.Start()
        Else
            MainViewModel.Instance.UnifiedEventLog.Log(Severity.Critical, "MGT", "Error starting fullset, unable to connect to RTE.")
        End If
    End Sub

    Private Function GetScanTypes() As List(Of String)
        Dim Items As New List(Of String)
        For i As Int16 = Convert.ToInt16("A"c) To Convert.ToInt16("Z"c)
            Dim letter As Char = Convert.ToChar(i)
            Items.Add(letter)
        Next
        Return Items
    End Function

    Private Function CanStart(obj As Object) As Boolean
        Return Not Fullset.IsRunning
    End Function

    Private Sub Start(obj As Object)
        Dim Errored As Boolean = False
        _FullsetTimer = New DispatcherTimer(TimeSpan.FromSeconds(30), DispatcherPriority.Normal, AddressOf Fullset_Completed, Dispatcher.CurrentDispatcher)
        Fullset.IncrementExtractId()
        Dim C As New ManagementServiceClient
        Try
            C.StartFullsetAsync(Fullset.Comment)
        Catch
            Errored = True
        Finally
            Try
                C.Close()
            Catch
            End Try
            C = Nothing
        End Try
        If Not Errored Then
            _FullsetTimer.Start()
            Fullset.IsRunning = True
        Else
            MainViewModel.Instance.UnifiedEventLog.Log(Severity.Critical, "MGT", "Error starting fullset, unable to connect to RTE.")
        End If
    End Sub

    Private Sub [Stop]()
        If _FullsetTimer IsNot Nothing Then
            _FullsetTimer.Stop()
            _FullsetTimer = Nothing
        End If
        AddFullsetCompletedTestDiaryComment(Fullset)
        Fullset.IsRunning = False
    End Sub

    Private Sub Fullset_Completed(sender As Object, e As EventArgs)
        [Stop]()
    End Sub

    Private Sub AddFullsetCompletedTestDiaryComment(fullset As FullsetViewModel)
        Dim FullsetComment = String.Format("""{0}""", fullset.Comment)
        Dim TestDiaryComment = String.Concat("SS", fullset.ToString, " ", "EXTRACT ID: ", fullset.ExtractId, FullsetComment)
        fullset.IsRunning = False
        MainViewModel.Instance.TestEngine.TestDiary.Add(TestDiaryComment)
        MainViewModel.Instance.UnifiedEventLog.Entries.Add(New UnifiedEventLogEntry("MGT", TestDiaryComment))
        Dim C As New ManagementServiceClient
        Try
            C.StopFullsetAsync()
        Catch
        Finally
            C.Close()
            C = Nothing
        End Try
    End Sub

#End Region

End Class
