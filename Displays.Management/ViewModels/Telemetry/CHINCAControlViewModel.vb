Public Class CHINCAControlViewModel
    Inherits TelemetryDeviceViewModel

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New(number As Integer, unitId As String)
        MyBase.New("CHINCA", number, unitId)
    End Sub

#End Region

#Region "Properties"

    Private _Channel As Integer
    Public Property Channel As Integer
        Get
            Return _Channel
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() Channel, _Channel, Value)
        End Set
    End Property

    Private _IsConnected As Boolean = True
    Public Property IsConnected As Boolean
        Get
            Return _IsConnected
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsConnected, _IsConnected, Value)
        End Set
    End Property

    Private _Gain As Integer
    Public Property Gain As Integer
        Get
            Return _Gain
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() Gain, _Gain, Value)
        End Set
    End Property

    Private _Mode As Integer
    Public Property Mode As Integer
        Get
            Return _Mode
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() Mode, _Mode, Value)
        End Set
    End Property

    Private _IsPowerFaulted As Boolean
    Public Property IsPowerFaulted As Boolean
        Get
            Return _IsPowerFaulted
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsPowerFaulted, _IsPowerFaulted, Value)
        End Set
    End Property

    Private _IsAirValveOpened As Boolean = True
    Public Property IsAirValveOpened As Boolean
        Get
            Return _IsAirValveOpened
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsAirValveOpened, _IsAirValveOpened, Value)
        End Set
    End Property

#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        Channel = 0
        Me.Gain = 5
        Me.IsAirValveOpened = True
        Me.IsConnected = True
        Me.IsPowerFaulted = False
        MyBase.Reset()
    End Sub


#End Region

#Region "Commands"

#Region "ChannelUpCommand"

    Private _ChannelUpCommand As ICommand
    Public ReadOnly Property ChannelUpCommand As ICommand
        Get
            If _ChannelUpCommand Is Nothing Then _
               _ChannelUpCommand = New RelayCommand(AddressOf ChannelUp, AddressOf CanChannelUp)
            Return _ChannelUpCommand
        End Get
    End Property

    Private Function CanChannelUp(obj As Object) As Boolean
        Return Channel < Integer.MaxValue
    End Function

    Private Sub ChannelUp(obj As Object)
        Channel += 1
    End Sub

#End Region

#Region "ChannelDownCommand"

    Private _ChannelDownCommand As ICommand
    Public ReadOnly Property ChannelDownCommand As ICommand
        Get
            If _ChannelDownCommand Is Nothing Then _
               _ChannelDownCommand = New RelayCommand(AddressOf ChannelDown, AddressOf CanChannelDown)
            Return _ChannelDownCommand
        End Get
    End Property

    Private Function CanChannelDown(obj As Object) As Boolean
        Return Channel > 1
    End Function

    Private Sub ChannelDown(obj As Object)
        Channel -= 1
    End Sub

#End Region

#End Region

End Class
