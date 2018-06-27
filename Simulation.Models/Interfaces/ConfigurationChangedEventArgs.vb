Public Class ConfigurationChangedEventArgs
    Inherits EventArgs

    Private _PropertyName As String

    Public Sub New(propertyName As String)
        _PropertyName = propertyName
    End Sub

    Public ReadOnly Property PropertyName As String
        Get
            Return _PropertyName
        End Get
    End Property

End Class
