Imports Microsoft.Win32
Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Public Class TabFileImportViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Private _FilePath As String
    Public Property FilePath As String
        Get
            Return _FilePath
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() FilePath, _FilePath, Value)
        End Set
    End Property

    Private _ImportParameters As New ObservableCollection(Of ImportParameterViewModel)
    Public ReadOnly Property ImportParameters As ObservableCollection(Of ImportParameterViewModel)
        Get
            Return _ImportParameters
        End Get
    End Property

    Private _SelectedImportParameter As ImportParameterViewModel
    Public Property SelectedImportParameter As ImportParameterViewModel
        Get
            Return _SelectedImportParameter
        End Get
        Set(ByVal Value As ImportParameterViewModel)
            SetProperty(Function() SelectedImportParameter, _SelectedImportParameter, Value)
        End Set
    End Property

    Private _SimulationParameters As New ObservableCollection(Of IParameter)
    Public ReadOnly Property SimulationParameters As ObservableCollection(Of IParameter)
        Get
            Return _SimulationParameters
        End Get
    End Property

    Private _SelectedSimulationParameter As IParameter
    Public Property SelectedSimulationParameter As IParameter
        Get
            Return _SelectedSimulationParameter
        End Get
        Set(ByVal Value As IParameter)
            SetProperty(Function() SelectedSimulationParameter, _SelectedSimulationParameter, Value)
        End Set
    End Property

    Private _CombinedParameters As New ObservableCollection(Of CombinedParameterViewModel)
    Public ReadOnly Property CombinedParameters As ObservableCollection(Of CombinedParameterViewModel)
        Get
            Return _CombinedParameters
        End Get
    End Property

    Private _SelectedCombinedParameter As CombinedParameterViewModel
    Public Property SelectedCombinedParameter As CombinedParameterViewModel
        Get
            Return _SelectedCombinedParameter
        End Get
        Set(ByVal Value As CombinedParameterViewModel)
            SetProperty(Function() SelectedCombinedParameter, _SelectedCombinedParameter, Value)
        End Set
    End Property

#End Region

#Region "Commands"

#Region "BrowseCommand"

    Private _BrowseCommand As ICommand
    Public ReadOnly Property BrowseCommand As ICommand
        Get
            If _BrowseCommand Is Nothing Then _
               _BrowseCommand = New RelayCommand(AddressOf Browse, AddressOf CanBrowse)
            Return _BrowseCommand
        End Get
    End Property

    Private Function CanBrowse(obj As Object) As Boolean
        Return True
    End Function

    Private Sub Browse(obj As Object)
        Dim D As New OpenFileDialog
        D.CheckFileExists = True
        D.Multiselect = False
        D.Filter = "All files|*.*"
        If D.ShowDialog Then FilePath = D.FileName
    End Sub

#End Region

#Region "LoadCommand"

    Private _LoadCommand As ICommand
    Public ReadOnly Property LoadCommand As ICommand
        Get
            If _LoadCommand Is Nothing Then _
               _LoadCommand = New RelayCommand(AddressOf Load, AddressOf CanLoad)
            Return _LoadCommand
        End Get
    End Property

    Private Function CanLoad(obj As Object) As Boolean
        Return IO.File.Exists(FilePath)
    End Function

    Private Sub Load(obj As Object)
        SplitAll(Nothing)
        ImportParameters.Clear()
        SelectedImportParameter = Nothing
        Try
            LoadFile()
            SelectedImportParameter = ImportParameters.FirstOrDefault
        Catch ex As LogImportException
            MessageBox.Show(String.Format("An error occured parsing the file at line number {0} and field index {1}:  {2}", ex.LineNumber, ex.FieldIndex, ex))
        End Try
    End Sub

#End Region

#Region "CombineCommand"

    Private _CombineCommand As ICommand
    Public ReadOnly Property CombineCommand As ICommand
        Get
            If _CombineCommand Is Nothing Then _
               _CombineCommand = New RelayCommand(AddressOf Combine, AddressOf CanCombine)
            Return _CombineCommand
        End Get
    End Property

    Private Function CanCombine(obj As Object) As Boolean
        Return SelectedImportParameter IsNot Nothing And SelectedSimulationParameter IsNot Nothing
    End Function

    Private Sub Combine(obj As Object)
        Dim CombinedParameter As New CombinedParameterViewModel(SelectedImportParameter, SelectedSimulationParameter)
        ImportParameters.Remove(SelectedImportParameter)
        SimulationParameters.Remove(SelectedSimulationParameter)
        CombinedParameters.Add(CombinedParameter)
        SelectedImportParameter = ImportParameters.FirstOrDefault
        SelectedSimulationParameter = SimulationParameters.FirstOrDefault
        SelectedCombinedParameter = CombinedParameter
    End Sub

#End Region

#Region "SplitCommand"

    Private _SplitCommand As ICommand
    Public ReadOnly Property SplitCommand As ICommand
        Get
            If _SplitCommand Is Nothing Then _
               _SplitCommand = New RelayCommand(AddressOf Split, AddressOf CanSplit)
            Return _SplitCommand
        End Get
    End Property

    Private Function CanSplit(obj As Object) As Boolean
        Return SelectedCombinedParameter IsNot Nothing
    End Function

    Private Sub Split(obj As Object)
        Split(SelectedCombinedParameter)
    End Sub

#End Region

#Region "SplitAllCommand"

    Private _SplitAllCommand As ICommand
    Public ReadOnly Property SplitAllCommand As ICommand
        Get
            If _SplitAllCommand Is Nothing Then _
               _SplitAllCommand = New RelayCommand(AddressOf SplitAll, AddressOf CanSplitAll)
            Return _SplitAllCommand
        End Get
    End Property

    Private Function CanSplitAll(obj As Object) As Boolean
        Return CombinedParameters.Count > 0
    End Function

    Private Sub SplitAll(obj As Object)
        Dim TempParameters As New List(Of CombinedParameterViewModel)
        For Each P In CombinedParameters
            TempParameters.Add(P)
        Next
        TempParameters.ForEach(Sub(p) Split(p))
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Sub Split(combinedParameter As CombinedParameterViewModel)
        ImportParameters.Add(combinedParameter.ImportParameter)
        SimulationParameters.Add(combinedParameter.SimulationParameter)
        SelectedImportParameter = combinedParameter.ImportParameter
        SelectedSimulationParameter = combinedParameter.SimulationParameter
        CombinedParameters.Remove(combinedParameter)
        SelectedCombinedParameter = CombinedParameters.FirstOrDefault
    End Sub

    Private Sub LoadFile()
        Dim ColumnIndex As Integer = -1
        Using Parser As New TextFieldParser(FilePath)
            Try
                Dim TempImportParameters As New List(Of ImportParameterViewModel)
                Dim Fields() As String = Nothing
                Parser.SetDelimiters(ControlChars.Tab)
                Parser.ReadLine() 'Read header
                Fields = Parser.ReadFields
                For ColumnIndex = 1 To Fields.Count - 1
                    TempImportParameters.Add(New ImportParameterViewModel(Fields(ColumnIndex), ColumnIndex))
                Next
                While Not Parser.EndOfData
                    Fields = Parser.ReadFields
                    For I = 1 To Fields.Count - 1
                        ColumnIndex = I
                        Dim IP = TempImportParameters.First(Function(p) p.ColumnIndex = ColumnIndex)
                        Dim Interval = Convert.ToDouble(Fields(0).Trim)
                        Dim Value = Convert.ToDouble(Fields(ColumnIndex).Replace("B", "").Replace("S", "").Trim)
                        IP.Values.Add(New ImportParameterValueViewModel(Parser.LineNumber, Interval, Value))
                    Next
                End While
                TempImportParameters.ForEach(Sub(p) ImportParameters.Add(p))
            Catch ex As Exception
                Throw New LogImportException(Parser.LineNumber, ColumnIndex, "An exception occured parsing the log file.", ex)
            End Try
        End Using
    End Sub

#End Region

End Class
