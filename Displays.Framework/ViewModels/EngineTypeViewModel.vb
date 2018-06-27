Public Class EngineTypeViewModel
    Inherits ViewModelBase

    Public Sub New(engineType As EngineType)
        Select Case engineType
            Case Displays.Services.EngineType.TrentXWB
                Name = "Trent XWB"
        End Select
        Type = engineType
    End Sub

    Private _Name As String
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() Name, _Name, Value)
        End Set
    End Property

    Private _Type As EngineType
    Public Property Type As EngineType
        Get
            Return _Type
        End Get
        Set(ByVal Value As EngineType)
            SetProperty(Function() Type, _Type, Value)
        End Set
    End Property

End Class
