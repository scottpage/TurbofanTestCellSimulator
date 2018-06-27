Public Class MainViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Private Sub New()
    End Sub

#End Region

#Region "Properties"

    Private Shared _Instance As MainViewModel
    Public Shared ReadOnly Property Instance As MainViewModel
        Get
            If _Instance Is Nothing Then _Instance = New MainViewModel
            Return _Instance
        End Get
    End Property

    Private _UnifiedEventLog As New UnifiedEventLogViewModel
    Public ReadOnly Property UnifiedEventLog As UnifiedEventLogViewModel
        Get
            Return _UnifiedEventLog
        End Get
    End Property

    Private _TestEngine As New TestEngineViewModel
    Public ReadOnly Property TestEngine As TestEngineViewModel
        Get
            Return _TestEngine
        End Get
    End Property

    Private _Telemetry As New TelemetryControllerViewModel
    Public ReadOnly Property Telemetry As TelemetryControllerViewModel
        Get
            Return _Telemetry
        End Get
    End Property

    Private _TestInformation As New TestInformationViewModel
    Public ReadOnly Property TestInformation As TestInformationViewModel
        Get
            Return _TestInformation
        End Get
    End Property


#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        Telemetry.Reset()
        TestInformation.Reset()
        UnifiedEventLog.Reset()
        TestEngine.Reset()
    End Sub

#End Region

#Region "Commands"

    Private _TestInformationPage As TestInformationWindow

    Private _OpenTestInformationPageCommand As New RelayCommand(AddressOf OpenTestInformationPage, AddressOf CanOpenTestInformationPage)
    Public ReadOnly Property OpenTestInformationPageCommand As ICommand
        Get
            Return _OpenTestInformationPageCommand
        End Get
    End Property

    Private Function CanOpenTestInformationPage(obj As Object) As Boolean
        Return _TestInformationPage Is Nothing
    End Function

    Private Sub OpenTestInformationPage(obj As Object)
        Dim CopyOfCurrentTestInformation = TestInformation.GetCopy
        CopyOfCurrentTestInformation.LogChanges = False
        _TestInformationPage = New TestInformationWindow
        AddHandler _TestInformationPage.Closed, AddressOf TestInformationWindow_Closed
        With _TestInformationPage
            .DataContext = CopyOfCurrentTestInformation
            If .ShowDialog() Then
                TestInformation.Update(CopyOfCurrentTestInformation)
            End If
        End With
    End Sub

    Private _CloseTestInformationPageCommand As ICommand
    Public ReadOnly Property CloseTestInformationPageCommand As ICommand
        Get
            If _CloseTestInformationPageCommand Is Nothing Then _CloseTestInformationPageCommand = New RelayCommand(AddressOf CloseTestInformationPage, AddressOf CanCloseTestInformationPage)
            Return _CloseTestInformationPageCommand
        End Get
    End Property

    Private Function CanCloseTestInformationPage(obj As Object) As Boolean
        Return _TestInformationPage IsNot Nothing
    End Function

    Private Sub CloseTestInformationPage(obj As Object)
        If _TestInformationPage IsNot Nothing Then
            _TestInformationPage.Close()
            _TestInformationPage = Nothing
        End If
    End Sub

    Private Sub TestInformationWindow_Closed(sender As Object, e As EventArgs)
        _TestInformationPage = Nothing
        _OpenTestInformationPageCommand.InvalidRequerySuggested()
    End Sub

#End Region

End Class
