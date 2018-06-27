Public Class ThrottleProfileViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _ActualValues As ObservableCollection(Of Point)
    Private _TargetValues As ObservableCollection(Of Point)
    Private _Timer As DispatcherTimer
    Private _CurrentValueIndex As Integer

#End Region

#Region "Constructors"

    Public Sub New()
        Dim TempActualValues As New List(Of Point)
        Dim TempTargetValues As New List(Of Point)

        ParameterName = "TLA"
        ValueMinimum = 0
        ValueMaximum = 100
        MaximumTime = 60

        For I = 1 To MaximumTime
            TempActualValues.Add(New Point(I, 0))
            TempTargetValues.Add(New Point(I, I * 0.104166667 + 0))
        Next
        _ActualValues = New ObservableCollection(Of Point)(TempActualValues)
        _TargetValues = New ObservableCollection(Of Point)(TempTargetValues)
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property ActualValues As ObservableCollection(Of Point)
        Get
            Return _ActualValues
        End Get
    End Property

    Public ReadOnly Property TargetValues As ObservableCollection(Of Point)
        Get
            Return _TargetValues
        End Get
    End Property

    Private _ValueMinimum As Double
    Public Property ValueMinimum As Double
        Get
            Return _ValueMinimum
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() ValueMinimum, _ValueMinimum, Value)
        End Set
    End Property

    Private _ValueMaximum As Double
    Public Property ValueMaximum As Double
        Get
            Return _ValueMaximum
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() ValueMaximum, _ValueMaximum, Value)
        End Set
    End Property

    Private _ParameterName As String
    Public Property ParameterName As String
        Get
            Return _ParameterName
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() ParameterName, _ParameterName, Value)
        End Set
    End Property

    Private _MaximumTime As Double
    Public Property MaximumTime As Double
        Get
            Return _MaximumTime
        End Get
        Set(ByVal Value As Double)
            SetProperty(Function() MaximumTime, _MaximumTime, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Sub StartUpdating()
        _Timer = New DispatcherTimer(TimeSpan.FromMilliseconds(250), DispatcherPriority.Normal, AddressOf UpdateValues, Dispatcher.CurrentDispatcher)
        _Timer.Start()
    End Sub

    Public Sub StopUpdating()
        If _Timer IsNot Nothing Then
            _Timer.Stop()
            _Timer = Nothing
        End If
    End Sub

    Private Sub UpdateValues(sender As Object, e As EventArgs)
        Dim ParameterValue = MainViewModel.Instance.Simulator.State.Parameters.ThrottleLeverAngle.Value
        Dim ActualP = ActualValues(_CurrentValueIndex)
        If Not Double.IsNaN(ParameterValue) Then ActualP.Y = ParameterValue
        ActualValues(_CurrentValueIndex) = ActualP
        _CurrentValueIndex += 1
        If _CurrentValueIndex > MaximumTime - 1 Then _CurrentValueIndex = 0
    End Sub

#End Region

End Class
