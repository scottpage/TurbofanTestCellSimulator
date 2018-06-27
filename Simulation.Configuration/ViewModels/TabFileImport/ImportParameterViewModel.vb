Public Class ImportParameterViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New(name As String, columnIndex As Integer)
        _Name = name
        _ColumnIndex = columnIndex
    End Sub

#End Region

#Region "Properties"

    Private _Name As String
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    Private _ColumnIndex As Integer
    Public ReadOnly Property ColumnIndex As Integer
        Get
            Return _ColumnIndex
        End Get
    End Property

    Private _Values As New ObservableCollection(Of ImportParameterValueViewModel)
    Public ReadOnly Property Values As ObservableCollection(Of ImportParameterValueViewModel)
        Get
            Return _Values
        End Get
    End Property

#End Region

#Region "Commands"



#End Region

#Region "Methods"



#End Region

End Class
