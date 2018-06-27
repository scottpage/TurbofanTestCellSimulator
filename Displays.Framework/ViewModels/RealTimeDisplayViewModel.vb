Public Class RealTimeDisplayViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _Title As String = String.Empty
    Private _DateTimeUpdateTimer As DispatcherTimer

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()
        _DateTimeUpdateTimer = New DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, AddressOf UpdateDateTime, CreatorDispatcher)
    End Sub

#End Region

#Region "Properties"

    Public Property Title As String
        Get
            Return _Title
        End Get
        Set(value As String)
            SetProperty(Function() Title, _Title, value)
        End Set
    End Property

    Private _CurrentDateTime As DateTime
    Public Property CurrentDateTime As DateTime
        Get
            Return _CurrentDateTime
        End Get
        Set(ByVal Value As DateTime)
            SetProperty(Function() CurrentDateTime, _CurrentDateTime, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Overridable Overloads Sub Update(state As SimulationState)

    End Sub

    Public Overridable Overloads Sub Update(values As SimulationParameters)

    End Sub

    Private Sub UpdateDateTime(sender As Object, e As EventArgs)
        CurrentDateTime = DateTime.UtcNow
    End Sub

#End Region

End Class
