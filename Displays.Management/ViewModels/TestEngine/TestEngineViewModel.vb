Public Class TestEngineViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _TransientLogController As New TransientLogControllerViewModel
    Private _FullsetController As New FullsetControllerViewModel
    Private _DDSController As New DDSControllerViewModel
    Private _TestDiary As New TestDiaryViewModel
    Private _CommentController As New CommentControllerViewModel
    Private _ScriptController As New ScriptControllerViewModel
    Private _SimulationState As New SimulationState

#End Region

#Region "Constructors"

    Public Sub New()
        'TODO:  Remove after testing
        TransientLogController.AvailableLogs.Logs.Add(New TransientLogViewModel("TA1", 0))
        TransientLogController.AvailableLogs.Logs.Add(New TransientLogViewModel("TB1", 0))
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property TransientLogController As TransientLogControllerViewModel
        Get
            Return _TransientLogController
        End Get
    End Property

    Public ReadOnly Property FullsetController As FullsetControllerViewModel
        Get
            Return _FullsetController
        End Get
    End Property

    Public ReadOnly Property DDSController As DDSControllerViewModel
        Get
            Return _DDSController
        End Get
    End Property

    Public ReadOnly Property CommentController As CommentControllerViewModel
        Get
            Return _CommentController
        End Get
    End Property

    Public ReadOnly Property ScriptController As ScriptControllerViewModel
        Get
            Return _ScriptController
        End Get
    End Property

    Public ReadOnly Property TestDiary As TestDiaryViewModel
        Get
            Return _TestDiary
        End Get
    End Property

    Public ReadOnly Property SimulationState As SimulationState
        Get
            Return _SimulationState
        End Get
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        TransientLogController.Reset()
        DDSController.Reset()
        CommentController.Reset()
        TestDiary.Reset()
    End Sub

    Public Sub Update(state As SimulationState)
        _SimulationState = state
    End Sub

#End Region

#Region "Commands"

    Public ReadOnly Property OpenTestInformationPageCommand As ICommand
        Get
            Return MainViewModel.Instance.OpenTestInformationPageCommand
        End Get
    End Property

#End Region

End Class
