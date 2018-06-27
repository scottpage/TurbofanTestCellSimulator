Public Class TestDiaryViewModel
    Inherits ViewModelBase

    Private _Entries As New ObservableCollection(Of TestDiaryEntryViewModel)
    Public ReadOnly Property Entries As ObservableCollection(Of TestDiaryEntryViewModel)
        Get
            Return _Entries
        End Get
    End Property

    Private _SelectedEntry As TestDiaryEntryViewModel
    Public Property SelectedEntry As TestDiaryEntryViewModel
        Get
            Return _SelectedEntry
        End Get
        Set(value As TestDiaryEntryViewModel)
            SetProperty(Function() SelectedEntry, _SelectedEntry, value)
        End Set
    End Property

    Private _SelectLastEntryOnAdd As Boolean = True
    Public Property SelectLastEntryOnAdd As Boolean
        Get
            Return _SelectLastEntryOnAdd
        End Get
        Set(value As Boolean)
            SetProperty(Function() SelectLastEntryOnAdd, _SelectLastEntryOnAdd, value)
        End Set
    End Property

    Public Sub Clear()
        Entries.Clear()
    End Sub

    Public Function Add(comment As String) As TestDiaryEntryViewModel
        Return Add(DateTime.UtcNow, comment)
    End Function

    Public Function Add(timestamp As DateTime, comment As String) As TestDiaryEntryViewModel
        If String.IsNullOrEmpty(comment) Then Return Nothing
        Dim Entry As New TestDiaryEntryViewModel(timestamp, comment)
        Entries.Add(Entry)
        SelectedEntry = Entry
        Return Entry
    End Function

    Public Sub Add(comments() As String)
        Dim LastEntry As TestDiaryEntryViewModel = Nothing
        For Each Comment In comments
            Dim Entry = Add(Comment)
            If Entry IsNot Nothing Then LastEntry = Entry
        Next
        SelectedEntry = LastEntry
    End Sub

    Public Overrides Sub Reset()
        MyBase.Reset()
        SelectedEntry = Nothing
        Entries.Clear()
    End Sub

End Class
