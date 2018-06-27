<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False, Inherited:=True)> _
Public Class ThrottleAttribute
    Inherits Attribute

    Public Sub New(manufacturer As String, _
                   model As String, _
                   majorVersion As Integer, _
                   minorVersion As Integer, _
                   drawing As String)
        _Manufacturer = manufacturer
        _Model = model
        _MajorVersion = majorVersion
        _MinorVersion = minorVersion
        _Drawing = drawing
    End Sub

    Private _Manufacturer As String
    Public ReadOnly Property Manufacturer As String
        Get
            Return _Manufacturer
        End Get
    End Property

    Private _Model As String
    Public ReadOnly Property Model As String
        Get
            Return _Model
        End Get
    End Property

    Private _MajorVersion As Integer
    Public ReadOnly Property MajorVersion As Integer
        Get
            Return _MajorVersion
        End Get
    End Property

    Private _MinorVersion As Integer
    Public ReadOnly Property MinorVersion As Integer
        Get
            Return _MinorVersion
        End Get
    End Property

    Private _Drawing As String
    Public ReadOnly Property Drawing As String
        Get
            Return _Drawing
        End Get
    End Property

End Class
