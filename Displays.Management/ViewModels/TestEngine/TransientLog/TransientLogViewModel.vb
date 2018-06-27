Public Class TransientLogViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _Name As String
    Private _Index As Integer
    Private _IsIndexVisible As Boolean

#End Region

#Region "Constructors"

    Public Sub New(name As String, index As Integer)
        _Name = name
        _Index = index
    End Sub

#End Region

#Region "Properties"

    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() Name, _Name, Value)
        End Set
    End Property

    Public Property Index As Integer
        Get
            Return _Index
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() Index, _Index, Value)
        End Set
    End Property

    Public ReadOnly Property DisplayName As String
        Get
            If IsIndexVisible Then Return String.Concat(Name, ".", Index)
            Return Name
        End Get
    End Property

    Public Property IsIndexVisible As Boolean
        Get
            Return _IsIndexVisible
        End Get
        Set(ByVal Value As Boolean)
            OnPropertyChanging(Function() DisplayName)
            SetProperty(Function() IsIndexVisible, _IsIndexVisible, Value)
            OnPropertyChanged(Function() DisplayName)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub IncrementIndex()
        System.Threading.Interlocked.Increment(Index)
    End Sub

#End Region

End Class
