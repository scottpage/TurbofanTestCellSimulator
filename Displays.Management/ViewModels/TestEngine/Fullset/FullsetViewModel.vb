Public Class FullsetViewModel
    Inherits ViewModelBase

#Region "Fields"

    Private _ScanType As String
    Private _MajorScanNumber As Integer
    Private _MinorScanNumber As Integer
    Public _Comment As String = String.Empty
    Private _ExtractId As Integer
    Private _IsRunning As Boolean

#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Public Property ScanType As String
        Get
            Return _ScanType
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() ScanType, _ScanType, Value)
        End Set
    End Property

    Public Property MajorScanNumber As Integer
        Get
            Return _MajorScanNumber
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() MajorScanNumber, _MajorScanNumber, Value)
        End Set
    End Property

    Public Property MinorScanNumber As Integer
        Get
            Return _MinorScanNumber
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() MinorScanNumber, _MinorScanNumber, Value)
        End Set
    End Property

    Public Property Comment As String
        Get
            Return _Comment
        End Get
        Set(ByVal Value As String)
            SetProperty(Function() Comment, _Comment, Value)
        End Set
    End Property

    Public Property IsRunning As Boolean
        Get
            Return _IsRunning
        End Get
        Set(ByVal Value As Boolean)
            SetProperty(Function() IsRunning, _IsRunning, Value)
        End Set
    End Property

    Public Property ExtractId As Integer
        Get
            Return _ExtractId
        End Get
        Set(ByVal Value As Integer)
            SetProperty(Function() ExtractId, _ExtractId, Value)
        End Set
    End Property



#End Region

#Region "Methods"

    Public Overrides Sub Reset()
        MyBase.Reset()
        Comment = String.Empty
        ScanType = "A"
        MajorScanNumber = 0
        MinorScanNumber = 0
        IsRunning = False
    End Sub

    Public Sub IncrementExtractId()
        ExtractId += 1
    End Sub

    Public Overrides Function ToString() As String
        Return String.Concat(ScanType, MajorScanNumber, MinorScanNumber)
    End Function

#End Region

End Class
