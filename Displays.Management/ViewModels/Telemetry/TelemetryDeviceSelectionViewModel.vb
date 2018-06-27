Public Class TelemetryDeviceSelectionViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New(name As String)
        _Name = name
    End Sub

#End Region

#Region "Properties"

    Private _Name As String
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    Private _IsSelected As Boolean
    Public Property IsSelected As Boolean
        Get
            Return _IsSelected
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsSelected, _IsSelected, Value)
        End Set
    End Property

    Private _Channel1To12Gain As Integer = 5
    Public Property Channel1To12Gain As Integer
        Get
            Return _Channel1To12Gain
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() Channel1To12Gain, _Channel1To12Gain, Value)
        End Set
    End Property

    Private _Channel13To24Gain As Integer = 5
    Public Property Channel13To24Gain As Integer
        Get
            Return _Channel13To24Gain
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() Channel13To24Gain, _Channel13To24Gain, Value)
        End Set
    End Property

#End Region

End Class
