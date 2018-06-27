Public MustInherit Class TelemetryDeviceViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Protected Sub New(model As String, number As Integer, unitId As String)
        _Model = model
        _Number = number
        _UnitId = unitId
        SelectionA.IsSelected = True
        SelectedSelection = SelectionA
    End Sub

#End Region

#Region "Properties"

    Private _Model As String
    Public Property Model As String
        Get
            Return _Model
        End Get
        Set(value As String)
            SetProperty(Function() Model, _Model, value)
        End Set
    End Property

    Private _Number As Integer
    Public Property Number As Integer
        Get
            Return _Number
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() Number, _Number, Value)
        End Set
    End Property

    Private _UnitId As String
    Public Property UnitId As String
        Get
            Return _UnitId
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() UnitId, _UnitId, Value)
        End Set
    End Property

    Private _IsCalibrationOn As Boolean
    Public Property IsCalibrationOn As Boolean
        Get
            Return _IsCalibrationOn
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsCalibrationOn, _IsCalibrationOn, Value)
        End Set
    End Property

    Private _SelectedSelection As TelemetryDeviceSelectionViewModel
    Public Property SelectedSelection As TelemetryDeviceSelectionViewModel
        Get
            Return _SelectedSelection
        End Get
        Set(ByVal Value As TelemetryDeviceSelectionViewModel)
            SetProperty(Function() SelectedSelection, _SelectedSelection, Value)
        End Set
    End Property

    Private _SelectionA As New TelemetryDeviceSelectionViewModel("A")
    Public ReadOnly Property SelectionA As TelemetryDeviceSelectionViewModel
        Get
            Return _SelectionA
        End Get
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        IsCalibrationOn = False
        SelectSelectionACommand.Execute(Nothing)
    End Sub

#End Region

#Region "Commands"

#Region "ToggleCalibrationCommand"

    Private _ToggleCalibrationCommand As ICommand
    Public ReadOnly Property ToggleCalibrationCommand As ICommand
        Get
            If _ToggleCalibrationCommand Is Nothing Then _
               _ToggleCalibrationCommand = New RelayCommand(AddressOf ToggleCalibration, AddressOf CanToggleCalibration)
            Return _ToggleCalibrationCommand
        End Get
    End Property

    Private Function CanToggleCalibration(obj As Object) As Boolean
        Return True
    End Function

    Private Sub ToggleCalibration(obj As Object)
        IsCalibrationOn = Not IsCalibrationOn
    End Sub

#End Region

#Region "Select Selection A Command"

    Private _SelectSelectionACommand As ICommand
    Public ReadOnly Property SelectSelectionACommand As ICommand
        Get
            If _SelectSelectionACommand Is Nothing Then _SelectSelectionACommand = New RelayCommand(AddressOf SelectSelectionA, AddressOf CanSelectSelectionA)
            Return _SelectSelectionACommand
        End Get
    End Property

    Private Function CanSelectSelectionA(obj As Object) As Boolean
        Return True
    End Function

    Protected Overridable Sub SelectSelectionA(obj As Object)
        SelectionA.IsSelected = True
        SelectedSelection = SelectionA
    End Sub

#End Region

#End Region

End Class
