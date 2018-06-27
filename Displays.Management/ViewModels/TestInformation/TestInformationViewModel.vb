Public Class TestInformationViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Private _LogChanges As Boolean = True
    Public Property LogChanges As Boolean
        Get
            Return _LogChanges
        End Get
        Set(value As Boolean)
            SetProperty(Function() LogChanges, _LogChanges, value)
        End Set
    End Property

    Public ReadOnly Property TestCell As String
        Get
            Return My.Settings.TestCellName
        End Get
    End Property

    Private _EngineType As String = My.Settings.EngineTypeName
    Public Property EngineType As String
        Get
            Return _EngineType
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() EngineType, _EngineType, Value)
            FieldValueChanged("Engine Type", _EngineType, Value)
        End Set
    End Property

    Private _EngineSerialNumber As String = "20005"
    Public Property EngineSerialNumber As String
        Get
            Return _EngineSerialNumber
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() EngineSerialNumber, _EngineSerialNumber, Value)
            FieldValueChanged("Engine S/N", _EngineSerialNumber, Value)
        End Set
    End Property

    Private _EngineBuildNumber As String = "001A/5"
    Public Property EngineBuildNumber As String
        Get
            Return _EngineBuildNumber
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() EngineBuildNumber, _EngineBuildNumber, Value)
            FieldValueChanged("Engine Build No", _EngineBuildNumber, Value)
        End Set
    End Property

    Private _LeadingTester As String
    Public Property LeadingTester As String
        Get
            Return _LeadingTester
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() LeadingTester, _LeadingTester, Value)
            FieldValueChanged("Leading Tester", _LeadingTester, Value)
        End Set
    End Property

    Private _FitterTester As String
    Public Property FitterTester As String
        Get
            Return _FitterTester
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() FitterTester, _FitterTester, Value)
            FieldValueChanged("Fitter Tester", _FitterTester, Value)
        End Set
    End Property

    Private _MCT As String
    Public Property MCT As String
        Get
            Return _MCT
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() MCT, _MCT, Value)
            FieldValueChanged("MCT", _MCT, Value)
        End Set
    End Property

    Private _ScheduleNumber As String = "1"
    Public Property ScheduleNumber As String
        Get
            Return _ScheduleNumber
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() ScheduleNumber, _ScheduleNumber, Value)
            FieldValueChanged("Schedule No", _ScheduleNumber, Value)
        End Set
    End Property

    Private _ColdNozzleArea As String = "Nominal"
    Public Property ColdNozzleArea As String
        Get
            Return _ColdNozzleArea
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() ColdNozzleArea, _ColdNozzleArea, Value)
            FieldValueChanged("Cold Nozzle Area", _ColdNozzleArea, Value)
        End Set
    End Property

    Private _HotNozzleArea As String = "Nominal"
    Public Property HotNozzleArea As String
        Get
            Return _HotNozzleArea
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() HotNozzleArea, _HotNozzleArea, Value)
            FieldValueChanged("Hot Nozzle Area", _HotNozzleArea, Value)
        End Set
    End Property

    Private _FinalNozzleArea As String = "Nominal"
    Public Property FinalNozzleArea As String
        Get
            Return _FinalNozzleArea
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() FinalNozzleArea, _FinalNozzleArea, Value)
            FieldValueChanged("Final Nozzle Area", _FinalNozzleArea, Value)
        End Set
    End Property

    Private _EngineFuelType As String = "Jet A-1"
    Public Property EngineFuelType As String
        Get
            Return _EngineFuelType
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() EngineFuelType, _EngineFuelType, Value)
            FieldValueChanged("Fuel Type", _EngineFuelType, Value)
        End Set
    End Property

    Private _EngineOilType As String = "BP2197"
    Public Property EngineOilType As String
        Get
            Return _EngineOilType
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() EngineOilType, _EngineOilType, Value)
            FieldValueChanged("Engine Oil Type", _EngineOilType, Value)
        End Set
    End Property

    Private _IDGOilType As String = "BP2197"
    Public Property IDGOilType As String
        Get
            Return _IDGOilType
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() IDGOilType, _IDGOilType, Value)
            FieldValueChanged("IDG Oil Type", _IDGOilType, Value)
        End Set
    End Property

    Private _StarterType As String = "Pneumatic"
    Public Property StarterType As String
        Get
            Return _StarterType
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() StarterType, _StarterType, Value)
            FieldValueChanged("Starter Type", _StarterType, Value)
        End Set
    End Property

    Public ReadOnly Property TestName As String
        Get
            Return My.Settings.TestName
        End Get
    End Property

    Public ReadOnly Property TestStartDate As DateTime
        Get
            Return My.Settings.TestStartDate
        End Get
    End Property

    Public ReadOnly Property TestDate As DateTime
        Get
            Return My.Settings.TestDate
        End Get
    End Property

    Private _DebrisGaurdNumber As String = "Not Fitted"
    Public Property DebrisGaurdNumber As String
        Get
            Return _DebrisGaurdNumber
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() DebrisGaurdNumber, _DebrisGaurdNumber, Value)
            FieldValueChanged("Debris Gaurd No", _DebrisGaurdNumber, Value)
        End Set
    End Property

    Private _AirIntake As String = "3-Piece Airmeter"
    Public Property AirIntake As String
        Get
            Return _AirIntake
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() AirIntake, _AirIntake, Value)
            FieldValueChanged("Air Intake", _AirIntake, Value)
        End Set
    End Property

    Private _FanSetNumber As String = "1"
    Public Property FanSetNumber As String
        Get
            Return _FanSetNumber
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() FanSetNumber, _FanSetNumber, Value)
            FieldValueChanged("Fan Set No", _FanSetNumber, Value)
        End Set
    End Property

    Private _WingNumber As String = "2"
    Public Property WingNumber As String
        Get
            Return _WingNumber
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() WingNumber, _WingNumber, Value)
            FieldValueChanged("Wing No", _WingNumber, Value)
        End Set
    End Property

    Private _TRUNumber As String = "Not Fitted"
    Public Property TRUNumber As String
        Get
            Return _TRUNumber
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() TRUNumber, _TRUNumber, Value)
            FieldValueChanged("TRU No", _TRUNumber, Value)
        End Set
    End Property

    Private _ObjectOfTest As String
    Public Property ObjectOfTest As String
        Get
            Return _ObjectOfTest
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() ObjectOfTest, _ObjectOfTest, Value)
            FieldValueChanged("Test Description", _ObjectOfTest, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        FitterTester = String.Empty
        LeadingTester = String.Empty
        MCT = String.Empty
        ObjectOfTest = String.Empty
    End Sub

    Private Sub FieldValueChanged(fieldName As String, oldValue As String, newValue As String)
        If Not LogChanges Then Return
        Dim Comment = "{0} from {1} to {2}"
        Dim OldValueIsEmpty As Boolean = String.IsNullOrWhiteSpace(oldValue)
        Dim NewVaalueIsEmpty As Boolean = String.IsNullOrWhiteSpace(newValue)
        If OldValueIsEmpty And NewVaalueIsEmpty Then Return
        If OldValueIsEmpty Then
            Comment = String.Format("{0} set to {1}", fieldName, newValue)
        Else
            Comment = String.Format(Comment, fieldName, oldValue, newValue)
        End If
        MainViewModel.Instance.TestEngine.TestDiary.Add(Comment)
        MainViewModel.Instance.UnifiedEventLog.Log("MGT", Comment)
    End Sub

    Public Function GetCopy() As TestInformationViewModel
        Return DirectCast(MemberwiseClone(), TestInformationViewModel)
    End Function

    Public Sub Update(ti As TestInformationViewModel)
        With Me
            .AirIntake = ti.AirIntake
            .ColdNozzleArea = ti.ColdNozzleArea
            .DebrisGaurdNumber = ti.DebrisGaurdNumber
            .EngineBuildNumber = ti.EngineBuildNumber
            .EngineFuelType = ti.EngineFuelType
            .EngineOilType = ti.EngineOilType
            .EngineSerialNumber = ti.EngineSerialNumber
            .EngineType = ti.EngineType
            .FanSetNumber = ti.FanSetNumber
            .FinalNozzleArea = ti.FinalNozzleArea
            .FitterTester = ti.FitterTester
            .HotNozzleArea = ti.HotNozzleArea
            .IDGOilType = ti.IDGOilType
            .LeadingTester = ti.LeadingTester
            .MCT = ti.MCT
            .ObjectOfTest = ti.ObjectOfTest
            .ScheduleNumber = ti.ScheduleNumber
            .StarterType = ti.StarterType
            .TRUNumber = ti.TRUNumber
            .WingNumber = ti.WingNumber
        End With
    End Sub

#End Region

#Region "Commands"

    Private _ApplyCommand As ICommand
    Public ReadOnly Property ApplyCommand As ICommand
        Get
            If _ApplyCommand Is Nothing Then _ApplyCommand = New RelayCommand(AddressOf Apply, AddressOf CanApply)
            Return _ApplyCommand
        End Get
    End Property

    Private Function CanApply(obj As Object) As Boolean
        Dim IsValid As Boolean = True
        IsValid = IsValid And Not String.IsNullOrEmpty(TestName)
        IsValid = IsValid And Not String.IsNullOrEmpty(EngineSerialNumber)
        IsValid = IsValid And Not String.IsNullOrEmpty(FitterTester)
        IsValid = IsValid And Not String.IsNullOrEmpty(LeadingTester)
        IsValid = IsValid And Not String.IsNullOrEmpty(MCT)
        IsValid = IsValid And Not String.IsNullOrWhiteSpace(ObjectOfTest)
        Return IsValid
    End Function

    Private Sub Apply(obj As Object)
        MainViewModel.Instance.CloseTestInformationPageCommand.Execute(obj)
    End Sub

    Public ReadOnly Property CancelCommand As ICommand
        Get
            Return MainViewModel.Instance.CloseTestInformationPageCommand
        End Get
    End Property

#End Region

End Class
