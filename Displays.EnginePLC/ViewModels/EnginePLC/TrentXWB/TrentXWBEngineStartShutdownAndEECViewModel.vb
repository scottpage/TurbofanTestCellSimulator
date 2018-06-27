Public Class TrentXWBEngineStartShutdownAndEECViewModel
    Inherits ViewModelBase

#Region "Properties"

    Private _EngineStartSystem As New TrentXWBEngineStartSystemViewModel
    Public ReadOnly Property EngineStartSystem As TrentXWBEngineStartSystemViewModel
        Get
            Return _EngineStartSystem
        End Get
    End Property

    Private _FacilityStartSystem As New TrentXWBFacilityStartSystemViewModel
    Public ReadOnly Property FacilityStartSystem As TrentXWBFacilityStartSystemViewModel
        Get
            Return _FacilityStartSystem
        End Get
    End Property

    Private _Hydraulics As New TrentXWBHydraulicsViewModel
    Public ReadOnly Property Hydraulics As TrentXWBHydraulicsViewModel
        Get
            Return _Hydraulics
        End Get
    End Property

    Private _EECFunctions As New TrentXWBEECFunctionsViewModel
    Public ReadOnly Property EECFunctions As TrentXWBEECFunctionsViewModel
        Get
            Return _EECFunctions
        End Get
    End Property

#End Region

#Region "Methods"

    Public Sub Update(state As SimulationState)
        EECFunctions.Update(state.EnginePLC)
        EngineStartSystem.Update(state.EnginePLC)
        FacilityStartSystem.Update(state.EnginePLC)
        Hydraulics.Update(state.EnginePLC)
    End Sub

#End Region

#Region "Commands"

#Region "Show start readings command"

    Private _ShowStartReadingsCommand As ICommand
    Public ReadOnly Property ShowStartReadingsCommand As ICommand
        Get
            If _ShowStartReadingsCommand Is Nothing Then _ShowStartReadingsCommand = New RelayCommand(AddressOf ShowStartReadings, AddressOf CanShowStartReadings)
            Return _ShowStartReadingsCommand
        End Get
    End Property

    Private Function CanShowStartReadings(obj As Object) As Boolean
        Return MainViewModel.Instance.StartReadings.Visibility = Visibility.Hidden
    End Function

    Private Sub ShowStartReadings(obj As Object)
        MainViewModel.Instance.StartReadings.Visibility = Visibility.Visible
    End Sub

#End Region

#Region "Show stop readings command"

    Private _ShowStopReadingsCommand As ICommand
    Public ReadOnly Property ShowStopReadingsCommand As ICommand
        Get
            If _ShowStopReadingsCommand Is Nothing Then _ShowStopReadingsCommand = New RelayCommand(AddressOf ShowStopReadings, AddressOf CanShowStopReadings)
            Return _ShowStopReadingsCommand
        End Get
    End Property

    Private Function CanShowStopReadings(obj As Object) As Boolean
        Return MainViewModel.Instance.StopReadings.Visibility = Visibility.Hidden
    End Function

    Private Sub ShowStopReadings(obj As Object)
        MainViewModel.Instance.StopReadings.Visibility = Visibility.Visible
    End Sub

#End Region

#End Region

End Class
