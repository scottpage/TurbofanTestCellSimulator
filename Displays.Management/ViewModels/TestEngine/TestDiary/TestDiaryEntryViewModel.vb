Public Class TestDiaryEntryViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _Timestamp As DateTime
    Private _Comment As String

#End Region

#Region "Constructors"

    Public Sub New(comment As String)
        _Timestamp = DateTime.Now
        _Comment = comment
    End Sub

    Public Sub New(timestamp As DateTime, comment As String)
        _Timestamp = timestamp
        _Comment = comment
    End Sub

#End Region

#Region "Properties"

    Public Property Timestamp As DateTime
        Get
            Return _Timestamp
        End Get
        Set(value As DateTime)
            SetProperty(Function() Timestamp, _Timestamp, value)
        End Set
    End Property

    Public Property Comment As String
        Get
            Return _Comment
        End Get
        Set(value As String)
            SetProperty(Function() Comment, _Comment, value)
        End Set
    End Property

#End Region

End Class
