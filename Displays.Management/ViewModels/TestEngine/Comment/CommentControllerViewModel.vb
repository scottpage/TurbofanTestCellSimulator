Public Class CommentControllerViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _AvailableComments As New ObservableCollection(Of String)
    Private _SelectedAvailableComment As String
    Private _Comment As String = String.Empty
    Private _SaveCommand As ICommand = New RelayCommand(AddressOf Save, AddressOf CanSave)

#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property SaveCommand As ICommand
        Get
            Return _SaveCommand
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
        Set(ByVal Value As String)
            SetProperty(Function() SelectedAvailableComment, _SelectedAvailableComment, Value)
            If AvailableComments.Contains(SelectedAvailableComment) Then Comment = SelectedAvailableComment
        End Set
    End Property

    Public Property Comment As String
        Get
            Return _Comment
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() Comment, _Comment, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        SelectedAvailableComment = Nothing
        Comment = String.Empty
    End Sub

    Private Function CanSave(obj As Object) As Boolean
        Return Not String.IsNullOrWhiteSpace(Comment)
    End Function

    Private Sub Save(obj As Object)
        Dim Lines = Comment.Replace(Environment.NewLine, vbCr).Split(vbCr(0))

        For Each Line In Lines
            MainViewModel.Instance.TestEngine.TestDiary.Add(Line)
            MainViewModel.Instance.UnifiedEventLog.Entries.Add(New UnifiedEventLogEntry("MGT", Line))
        Next
    End Sub

#End Region

End Class
