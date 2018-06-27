Public MustInherit Class ScriptViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Private _Name As String
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() Name, _Name, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public MustOverride Sub Execute()

    Protected Sub Log(comment As String, Optional addToUnifiedEventLog As Boolean = True)
        MainViewModel.Instance.TestEngine.TestDiary.Add(comment)
        If addToUnifiedEventLog Then MainViewModel.Instance.UnifiedEventLog.Entries.Add(New UnifiedEventLogEntry("MGT", comment))
    End Sub

    Protected Sub Log(comment As String, addToUnifiedEventLog As Boolean, ParamArray args() As Object)
        Dim FormattedComment = String.Format(comment, args)
        MainViewModel.Instance.TestEngine.TestDiary.Add(FormattedComment)
        If addToUnifiedEventLog Then MainViewModel.Instance.UnifiedEventLog.Entries.Add(New UnifiedEventLogEntry("MGT", FormattedComment))
    End Sub

#End Region

End Class
