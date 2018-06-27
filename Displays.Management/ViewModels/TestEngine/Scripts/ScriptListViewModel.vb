Public Class ScriptControllerViewModel
    Inherits ViewModelBase

    Private _ExecuteSelectedScriptCommand As ICommand = New RelayCommand(AddressOf ExecuteSelectedScript, AddressOf CanExecuteSelectedScript)
    Private _Scripts As New ObservableCollection(Of ScriptViewModel)

    Public Sub New()
        Scripts.Add(New PrestartReadingsScriptViewModel)
        Scripts.Add(New StartReadingsScriptViewModel)
        Scripts.Add(New StopReadingsScriptViewModel)
    End Sub

    Public ReadOnly Property Scripts As ObservableCollection(Of ScriptViewModel)
        Get
            Return _Scripts
        End Get
    End Property

    Private _SelectedScript As ScriptViewModel
    Public Property SelectedScript As ScriptViewModel
        Get
            Return _SelectedScript
        End Get
        Set(ByVal Value As ScriptViewModel)
            SetProperty(Function() SelectedScript, _SelectedScript, Value)
        End Set
    End Property

    Public ReadOnly Property ExecuteSelectedScriptCommand As ICommand
        Get
            Return _ExecuteSelectedScriptCommand
        End Get
    End Property

    Private Function CanExecuteSelectedScript(obj As Object) As Boolean
        Return SelectedScript IsNot Nothing
    End Function

    Private Sub ExecuteSelectedScript(obj As Object)
        SelectedScript.Execute()
    End Sub

End Class
