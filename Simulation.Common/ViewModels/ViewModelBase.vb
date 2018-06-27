Imports System.ComponentModel

Namespace ViewModels

    Public Class ViewModelBase
        Implements INotifyPropertyChanging
        Implements INotifyPropertyChanged

#Region "Fields"

        Private _Dispatcher As Dispatcher
        Private _IsDirty As Boolean

#End Region

#Region "Constructors"

        Protected Sub New()
            _Dispatcher = Dispatcher.CurrentDispatcher
        End Sub

#End Region

#Region "Properties"

        Protected ReadOnly Property Dispatcher As Dispatcher
            Get
                Return _Dispatcher
            End Get
        End Property

        Public Property IsDirty As Boolean
            Get
                Return _IsDirty
            End Get
            Set(ByVal Value As Boolean)
                If _IsDirty = Value Then Return
                'Don't let this property dirty itself (whaaaattt??  lol)
                RaiseEvent PropertyChanging(Me, New PropertyChangingEventArgs("IsDirty"))
                _IsDirty = Value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IsDirty"))
            End Set
        End Property

#End Region

#Region "Methods"

        Private Delegate Sub RefreshCommandsSyncHandler()

        Public Overridable Sub Initialize()
        End Sub

        Public Sub RefreshCommands()
            Dim Handler As New RefreshCommandsSyncHandler(AddressOf RefreshCommandsSync)
            _Dispatcher.Invoke(Handler)
        End Sub

        Protected Overridable Sub RefreshCommandsSync()
        End Sub

        Public Overridable Sub Reset()
        End Sub

#End Region

#Region "INotifyPropertyChanging and INotifyPropertyChanged"

        Public Event PropertyChanging(sender As Object, e As System.ComponentModel.PropertyChangingEventArgs) Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
        Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Protected Overridable Sub OnPropertyChanging(propertyName As String)
#If DEBUG Then
#If VALIDATEPROPERTYNAMES Then
        ValidatePropertyName(propertyName)
#End If
#End If
            RaiseEvent PropertyChanging(Me, New PropertyChangingEventArgs(propertyName))
        End Sub

        Protected Overridable Sub OnPropertyChanged(propertyName As String)
#If DEBUG Then
#If VALIDATEPROPERTYNAMES Then
        ValidatePropertyName(propertyName)
#End If
#End If
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
            IsDirty = True
        End Sub

#If DEBUG Then

        Private Sub ValidatePropertyName(propertyName As String)
            If TypeDescriptor.GetProperties(Me).Find(propertyName, False) Is Nothing Then Throw New ArgumentException(String.Format("No property named ""{0}"" exists for the current object", propertyName), propertyName)
        End Sub

#End If

#End Region

    End Class
End Namespace