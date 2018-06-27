Imports System.ComponentModel

Public Class ParameterListViewModel
    Inherits ObservableCollection(Of IParameter)

#Region "Fields"



#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

#End Region

#Region "Properties"

    Private _SelectedParameter As IParameter
    Public Property SelectedParameter As IParameter
        Get
            Return _SelectedParameter
        End Get
        Set(ByVal Value As IParameter)
            If _SelectedParameter Is Value Then Return
            _SelectedParameter = Value
            OnPropertyChanged(New PropertyChangedEventArgs("SelectedParameter"))
        End Set
    End Property

#End Region

#Region "Methods"



#End Region

End Class
