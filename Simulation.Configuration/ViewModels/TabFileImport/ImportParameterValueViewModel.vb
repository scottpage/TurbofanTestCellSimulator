Public Class ImportParameterValueViewModel
    Inherits ViewModelBase

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New(lineNumber As Long, interval As Double, value As Double)
        _LineNumber = lineNumber
        _Interval = interval
        _Value = value
    End Sub

#End Region

#Region "Properties"

    Private _LineNumber As Long
    Public ReadOnly Property LineNumber As Long
        Get
            Return _LineNumber
        End Get
    End Property

    Private _Interval As Double
    Public ReadOnly Property Interval As Double
        Get
            Return _Interval
        End Get
    End Property

    Private _Value As Double
    Public ReadOnly Property Value As Double
        Get
            Return _Value
        End Get
    End Property

#End Region

#Region "Commands"



#End Region

#Region "Methods"



#End Region

End Class
