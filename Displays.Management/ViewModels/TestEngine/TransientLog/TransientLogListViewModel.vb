Public Class TransientLogListViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private WithEvents _Logs As ObservableCollection(Of TransientLogViewModel)
    Private _SelectedLog As TransientLogViewModel
    Private _IsIndexVisible As Boolean

#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property Logs As ObservableCollection(Of TransientLogViewModel)
        Get
            If _Logs Is Nothing Then _Logs = New ObservableCollection(Of TransientLogViewModel)
            Return _Logs
        End Get
    End Property

    Public Property SelectedLog As TransientLogViewModel
        Get
            Return _SelectedLog
        End Get
        Set(ByVal Value As TransientLogViewModel)
            SetProperty(Function() SelectedLog, _SelectedLog, Value)
        End Set
    End Property

    Public Property IsIndexVisible As Boolean
        Get
            Return _IsIndexVisible
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsIndexVisible, _IsIndexVisible, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Private Sub _Logs_CollectionChanged(sender As Object, e As System.Collections.Specialized.NotifyCollectionChangedEventArgs) Handles _Logs.CollectionChanged
        Select Case e.Action
            Case Specialized.NotifyCollectionChangedAction.Add
                For Each Item In e.NewItems
                    Dim Log = DirectCast(Item, TransientLogViewModel)
                    Log.IsIndexVisible = IsIndexVisible
                Next
        End Select
    End Sub

#End Region

End Class
