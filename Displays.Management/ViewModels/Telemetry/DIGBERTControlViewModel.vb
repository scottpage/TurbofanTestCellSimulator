Public Class DIGBERTControlViewModel
    Inherits TelemetryDeviceViewModel
    Implements IPolarizing

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New(number As Integer, unitId As String)
        MyBase.New("DIGBERT", number, unitId)
    End Sub

#End Region

#Region "Properties"

    Private _IsPolarityOn As Boolean
    Public Property IsPolarityOn As Boolean Implements IPolarizing.IsPolarityOn
        Get
            Return _IsPolarityOn
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsPolarityOn, _IsPolarityOn, Value)
        End Set
    End Property

    Private _IsPowerOn As Boolean
    Public Property IsPowerOn As Boolean
        Get
            Return _IsPowerOn
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsPowerOn, _IsPowerOn, Value)
        End Set
    End Property

    Private _SelectionB As New TelemetryDeviceSelectionViewModel("B")
    Public ReadOnly Property SelectionB As TelemetryDeviceSelectionViewModel
        Get
            Return _SelectionB
        End Get
    End Property

    Private _SelectionC As New TelemetryDeviceSelectionViewModel("C")
    Public ReadOnly Property SelectionC As TelemetryDeviceSelectionViewModel
        Get
            Return _SelectionC
        End Get
    End Property

#End Region

#Region "Methods"




#End Region

#Region "Commands"

    Protected Overrides Sub SelectSelectionA(obj As Object)
        MyBase.SelectSelectionA(obj)
        SelectionB.IsSelected = False
        SelectionC.IsSelected = False
    End Sub

#Region "TogglePowerCommand"

    Private _TogglePowerCommand As ICommand
    Public ReadOnly Property TogglePowerCommand As ICommand
        Get
            If _TogglePowerCommand Is Nothing Then _TogglePowerCommand = New RelayCommand(AddressOf TogglePower, AddressOf CanTogglePower)
            Return _TogglePowerCommand
        End Get
    End Property

    Private Function CanTogglePower(obj As Object) As Boolean
        Return True
    End Function

    Private Sub TogglePower(obj As Object)
        IsPowerOn = Not IsPowerOn
    End Sub

#End Region

#Region "TogglePolarityCommand"

    Private _TogglePolarityCommand As ICommand
    Public ReadOnly Property TogglePolarityCommand As ICommand Implements IPolarizing.TogglePolarityCommand
        Get
            If _TogglePolarityCommand Is Nothing Then _
               _TogglePolarityCommand = New RelayCommand(AddressOf TogglePolarity, AddressOf CanTogglePolarity)
            Return _TogglePolarityCommand
        End Get
    End Property

    Private Function CanTogglePolarity(obj As Object) As Boolean
        Return True
    End Function

    Private Sub TogglePolarity(obj As Object)
        IsPolarityOn = Not IsPolarityOn
    End Sub

#End Region

#Region "Select Selection B"

    Private _SelectSelectionBCommand As ICommand
    Public ReadOnly Property SelectSelectionBCommand As ICommand
        Get
            If _SelectSelectionBCommand Is Nothing Then _SelectSelectionBCommand = New RelayCommand(AddressOf SelectSelectionB, AddressOf CanSelectSelectionB)
            Return _SelectSelectionBCommand
        End Get
    End Property

    Private Function CanSelectSelectionB(obj As Object) As Boolean
        Return True
    End Function

    Private Sub SelectSelectionB(obj As Object)
        SelectionA.IsSelected = False
        SelectionB.IsSelected = True
        SelectionC.IsSelected = False
        SelectedSelection = SelectionB
    End Sub

#End Region

#Region "Select Selection C"

    Private _SelectSelectionCCommand As ICommand
    Public ReadOnly Property SelectSelectionCCommand As ICommand
        Get
            If _SelectSelectionCCommand Is Nothing Then _SelectSelectionCCommand = New RelayCommand(AddressOf SelectSelectionC, AddressOf CanSelectSelectionC)
            Return _SelectSelectionCCommand
        End Get
    End Property

    Private Function CanSelectSelectionC(obj As Object) As Boolean
        Return True
    End Function

    Private Sub SelectSelectionC(obj As Object)
        SelectionA.IsSelected = False
        SelectionB.IsSelected = False
        SelectionC.IsSelected = True
        SelectedSelection = SelectionB
    End Sub

#End Region

#End Region

End Class
