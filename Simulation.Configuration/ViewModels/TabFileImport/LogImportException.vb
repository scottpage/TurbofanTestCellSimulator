<Serializable>
Public Class LogImportException
    Inherits Exception

    Private _LineNumber As Long
    Private _FieldIndex As Integer

    Public Sub New(lineNumber As Long, fieldIndex As Integer, message As String)
        MyBase.New(message)
        _LineNumber = lineNumber
        _FieldIndex = fieldIndex
    End Sub

    Public Sub New(lineNumber As Long, fieldIndex As Integer, message As String, innerException As Exception)
        MyBase.New(message, innerException)
        _LineNumber = lineNumber
        _FieldIndex = fieldIndex
    End Sub

    Public ReadOnly Property LineNumber As Long
        Get
            Return _LineNumber
        End Get
    End Property

    Public ReadOnly Property FieldIndex As Integer
        Get
            Return _FieldIndex
        End Get
    End Property

End Class
