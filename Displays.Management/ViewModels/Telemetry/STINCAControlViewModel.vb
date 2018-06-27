Public Class STINCAControlViewModel
    Inherits TelemetryDeviceViewModel
    Implements IPolarizing

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New(number As Integer, unitId As String)
        MyBase.New("STINCA", number, unitId)
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

    Private _SupplyPowerHighPositiveWarning As Boolean
    Public Property SupplyPowerHighPositiveWarning As Boolean
        Get
            Return _SupplyPowerHighPositiveWarning
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() SupplyPowerHighPositiveWarning, _SupplyPowerHighPositiveWarning, Value)
        End Set
    End Property

    Private _SupplyPowerLowPositiveWarning As Boolean
    Public Property SupplyPowerLowPositiveWarning As Boolean
        Get
            Return _SupplyPowerLowPositiveWarning
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() SupplyPowerLowPositiveWarning, _SupplyPowerLowPositiveWarning, Value)
        End Set
    End Property

    Private _SupplyPowerHighNegativeWarning As Boolean
    Public Property SupplyPowerHighNegativeWarning As Boolean
        Get
            Return _SupplyPowerHighNegativeWarning
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() SupplyPowerHighNegativeWarning, _SupplyPowerHighNegativeWarning, Value)
        End Set
    End Property

    Private _SupplyPowerLowNegativeWarning As Boolean
    Public Property SupplyPowerLowNegativeWarning As Boolean
        Get
            Return _SupplyPowerLowNegativeWarning
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() SupplyPowerLowNegativeWarning, _SupplyPowerLowNegativeWarning, Value)
        End Set
    End Property

    Private _IsAirOn As Boolean = True
    Public Property IsAirOn As Boolean
        Get
            Return _IsAirOn
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsAirOn, _IsAirOn, Value)
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

    Private _SelectionD As New TelemetryDeviceSelectionViewModel("D")
    Public ReadOnly Property SelectionD As TelemetryDeviceSelectionViewModel
        Get
            Return _SelectionD
        End Get
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        IsPolarityOn = False
        IsAirOn = False
        SupplyPowerHighNegativeWarning = False
        SupplyPowerHighPositiveWarning = False
        SupplyPowerLowNegativeWarning = False
        SupplyPowerLowPositiveWarning = False
        MyBase.Reset()
    End Sub


#End Region

#Region "Commands"

    Protected Overrides Sub SelectSelectionA(obj As Object)
        MyBase.SelectSelectionA(obj)
        SelectionB.IsSelected = False
        SelectionC.IsSelected = False
        SelectionD.IsSelected = False
    End Sub

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
        SelectionD.IsSelected = False
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
        SelectionD.IsSelected = False
        SelectedSelection = SelectionC
    End Sub

#End Region

#Region "Select Selection D"

    Private _SelectSelectionDCommand As ICommand
    Public ReadOnly Property SelectSelectionDCommand As ICommand
        Get
            If _SelectSelectionDCommand Is Nothing Then _SelectSelectionDCommand = New RelayCommand(AddressOf SelectSelectionD, AddressOf CanSelectSelectionD)
            Return _SelectSelectionDCommand
        End Get
    End Property

    Private Function CanSelectSelectionD(obj As Object) As Boolean
        Return True
    End Function

    Private Sub SelectSelectionD(obj As Object)
        SelectionA.IsSelected = False
        SelectionB.IsSelected = False
        SelectionC.IsSelected = False
        SelectionD.IsSelected = True
        SelectedSelection = SelectionD
    End Sub

#End Region

#End Region

End Class
