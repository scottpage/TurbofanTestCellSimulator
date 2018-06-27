Imports System.Windows.Input
Imports System.ComponentModel
Imports System.Linq.Expressions

Public Class Value
    Inherits ViewModelBase
    Implements IValue

#Region "Constants"

    Public Const BadValue As Double = -99999.0#

#End Region

#Region "Events"

    Public Event ConfigurationChanged(sender As Object, e As ConfigurationChangedEventArgs) Implements INotifyConfigurationChanged.ConfigurationChanged

#End Region

#Region "Fields"

    Private Shared ReadOnly Randomizer As New Random(DateTime.Now.Millisecond)
    Private _Values(-1) As Double
    Private _CurrentAverageSampleNumber As Integer
    Private _UpdateSyncObj As New Object

#End Region

#Region "Constructors"

    Public Sub New()
        StoppedPolynomial = New OrderedPolynomial
        DryCrankPolynomial = New OrderedPolynomial
        RunningPolynomial = New OrderedPolynomial
        ShutdownPolynomial = New OrderedPolynomial
        StartPolynomial = New OrderedPolynomial
        WetCrankPolynomial = New OrderedPolynomial
        Maximum = 100
        Minimum = 0
        UsePolynomials = True
        Mode = EngineMode.Stopped
        Quality = Models.Quality.Good
        ReDim _Values(NumberOfSamples - 1)
    End Sub

#End Region

#Region "Properties"

    Public Property Mode As EngineMode Implements IValue.Mode
    Public Property StoppedPolynomial As OrderedPolynomial Implements IValue.StoppedPolynomial
    Public Property DryCrankPolynomial As OrderedPolynomial Implements IValue.DryCrankPolynomial
    Public Property WetCrankPolynomial As OrderedPolynomial Implements IValue.WetCrankPolynomial
    Public Property StartPolynomial As OrderedPolynomial Implements IValue.StartPolynomial
    Public Property RunningPolynomial As OrderedPolynomial Implements IValue.RunningPolynomial
    Public Property ShutdownPolynomial As OrderedPolynomial Implements IValue.ShutdownPolynomial
    Public Property UsePolynomials As Boolean Implements IValue.UsePolynomials

    Private _RedirectedValue As Double
    Public Property RedirectedValue As Double Implements IValue.RedirectedValue
        Get
            Return _RedirectedValue
        End Get
        Set(value As Double)
            SetProperty(Function() RedirectedValue, _RedirectedValue, value)
            If Not IsRedirected Then
                _IsRedirected = True
                OnPropertyChanged(Function() IsRedirected)
            End If
        End Set
    End Property

    Private _IsRedirected As Boolean
    Public ReadOnly Property IsRedirected As Boolean Implements IValue.IsRedirected
        Get
            Return _IsRedirected
        End Get
    End Property

    Private _Average As Double
    Public ReadOnly Property Average As Double Implements IValue.Average
        Get
            Return _Average
        End Get
    End Property

    Private _NumberOfSamples As Integer = 50
    Public Property NumberOfSamples As Integer Implements IValue.NumberOfSamples
        Get
            Return _NumberOfSamples
        End Get
        Set(value As Integer)
            If _NumberOfSamples = value Then Return
            OnPropertyChanging(Function() NumberOfSamples)
            _NumberOfSamples = value
            SyncLock _UpdateSyncObj
                ReDim Preserve _Values(_NumberOfSamples - 1)
            End SyncLock
            OnPropertyChanged(Function() NumberOfSamples)
        End Set
    End Property

    Private _Current As Double
    Public Property Current As Double Implements IValue.Current
        Get
            Return _Current
        End Get
        Set(value As Double)
            SetProperty(Function() Current, _Current, value)
        End Set
    End Property

    Private _StandardDeviation As Double
    Public ReadOnly Property StandardDeviation As Double Implements IValue.StandardDeviation
        Get
            Return _StandardDeviation
        End Get
    End Property

    Private _IsStable As Boolean
    Public ReadOnly Property IsStable As Boolean Implements IValue.IsStable
        Get
            Return _IsStable
        End Get
    End Property

    Private _Maximum As Double = 100
    Public Property Maximum As Double Implements IValue.Maximum
        Get
            Return _Maximum
        End Get
        Set(value As Double)
            If _Maximum = value Or value <= Minimum Then Return
            SetProperty(Function() Maximum, _Maximum, value)
        End Set
    End Property

    Private _Minimum As Double = 0
    Public Property Minimum As Double Implements IValue.Minimum
        Get
            Return _Minimum
        End Get
        Set(value As Double)
            If _Minimum = value Or value >= Maximum Then Return
            SetProperty(Function() Minimum, _Minimum, value)
        End Set
    End Property

    Private _StabilityThreshold As Double = 0.1
    Public Property StabilityThreshold As Double Implements IValue.StabilityThreshold
        Get
            Return _StabilityThreshold
        End Get
        Set(value As Double)
            SetProperty(Function() StabilityThreshold, _StabilityThreshold, value)
        End Set
    End Property

    Private _Quality As Quality = Models.Quality.Good
    Public Property Quality As Quality Implements IValue.Quality
        Get
            Return _Quality
        End Get
        Set(value As Quality)
            SetProperty(Function() Quality, _Quality, value)
        End Set
    End Property

#End Region

#Region "Commands"

#Region "RemoveRedirectCommand"


    Private _RemoveRedirectCommand As ICommand
    Public ReadOnly Property RemoveRedirectCommand As ICommand
        Get
            If _RemoveRedirectCommand Is Nothing Then _
               _RemoveRedirectCommand = New RelayCommand(AddressOf RemoveRedirect, AddressOf CanRemoveRedirect)
            Return _RemoveRedirectCommand
        End Get
    End Property

    Private Function CanRemoveRedirect(obj As Object) As Boolean
        Return IsRedirected
    End Function

    Private Sub RemoveRedirect(obj As Object)
        SetProperty(Function() IsRedirected, _IsRedirected, False)
    End Sub



#End Region

#End Region

#Region "Methods"

    Public Sub Update(value As Double) Implements IValue.Update
        SyncLock _UpdateSyncObj
            Dim RawValue = value
            Dim Polynomial = FindPolynomial(Mode)
            If Not Quality = Models.Quality.Good Then RawValue = BadValue
            If Not Quality = Models.Quality.Bad Then
                If IsRedirected Then
                    RawValue = RedirectedValue
                ElseIf Not IsRedirected And UsePolynomials Then
                    RawValue = Polynomial.Calculate(value)
                End If
                If RawValue < Minimum Then RawValue = Minimum
                If RawValue > Maximum Then RawValue = Maximum
            End If
            Current = RawValue
            If Not Quality = Models.Quality.Bad Then
                If Polynomial.IsNoiseEnabled Then Current = Randomize(Current, Polynomial.NoiseLevel)
                UpdateAverage(Current)
                UpdateStandardDeviation()
                UpdateStability()
                If Polynomial.IsNoiseEnabled Then
                    _Average = Randomize(Average, Polynomial.NoiseLevel)
                End If
            Else
                _Average = BadValue
            End If
            OnPropertyChanged(Function() Average)
        End SyncLock
    End Sub

    Private Function FindPolynomial(mode As EngineMode) As OrderedPolynomial
        Dim Polynomial As OrderedPolynomial = Nothing
        Select Case mode
            Case EngineMode.DryCranking
                Polynomial = DryCrankPolynomial
            Case EngineMode.Running
                Polynomial = RunningPolynomial
            Case EngineMode.Starting
                Polynomial = StartPolynomial
            Case EngineMode.Stopped
                Polynomial = StoppedPolynomial
            Case EngineMode.Stopping
                Polynomial = ShutdownPolynomial
            Case EngineMode.WetCranking
                Polynomial = WetCrankPolynomial
        End Select
        Return Polynomial
    End Function

    Private Function Randomize(value As Double, noiseLevel As Double) As Double
        Dim IntNoiseLevel = CInt(noiseLevel * 100)
        Dim HalfNoiseLevel = IntNoiseLevel / 2
        Dim Noise = Randomizer.Next(-IntNoiseLevel, IntNoiseLevel) / 100
        Return value + Noise
    End Function

    Private Sub UpdateAverage(value As Double)
        _CurrentAverageSampleNumber += 1
        If _CurrentAverageSampleNumber > NumberOfSamples - 1 Then _CurrentAverageSampleNumber = 0
        _Values(_CurrentAverageSampleNumber) = value
        _Average = _Values.Average
    End Sub

    Private Sub UpdateStandardDeviation()
        SetProperty(Function() StandardDeviation, _StandardDeviation, _Values.StandardDeviation)
    End Sub

    Private Sub UpdateStability()
        SetProperty(Function() IsStable, _IsStable, StandardDeviation <= StabilityThreshold)
    End Sub

    Private Sub OnConfigurationChanged(Of T)(expression As Expression(Of Func(Of T)))
        Dim PropName = GetPropertyName(expression)
        RaiseEvent ConfigurationChanged(Me, New ConfigurationChangedEventArgs(PropName))
    End Sub

    Protected Overrides Sub OnPropertyChanged(Of T)(expression As Expression(Of Func(Of T)))
        MyBase.OnPropertyChanged(expression)
        Dim PropName = GetPropertyName(expression)
        If PropName.Equals(GetPropertyName(Function() Minimum)) And _
            PropName.Equals(GetPropertyName(Function() Maximum)) Then
            OnConfigurationChanged(expression)
        End If
    End Sub

    Public Sub Copy(value As IValue) Implements IValue.Copy
        With value
            Minimum = .Minimum
            Maximum = .Maximum
            NumberOfSamples = .NumberOfSamples
            UsePolynomials = .UsePolynomials
            DryCrankPolynomial.Copy(.DryCrankPolynomial)
            WetCrankPolynomial.Copy(.WetCrankPolynomial)
            StoppedPolynomial.Copy(.StoppedPolynomial)
            StartPolynomial.Copy(.StartPolynomial)
            RunningPolynomial.Copy(.RunningPolynomial)
            ShutdownPolynomial.Copy(.ShutdownPolynomial)
        End With
    End Sub

#End Region

#Region "Mode Transitioning"

    Private _StoppedToRunningTransition As EngineModeTransitionPlayback

    ''' <summary>
    ''' Generates an interval driven value table from one engine mode to another for a finite duration.
    ''' </summary>
    ''' <param name="duration">The seconds this transition will represent.</param>
    ''' <param name="interval">The milliseconds between samples for this transition.</param>
    ''' <param name="fromMode">The <see cref="EngineMode" /> transitioning from.</param>
    ''' <param name="toMode">The <see cref="EngineMode" /> transitioning to.</param>
    ''' <remarks></remarks>
    Public Sub PrepareTransition(fromMode As EngineMode, toMode As EngineMode, interval As Integer, duration As Integer)

    End Sub

#End Region

End Class
