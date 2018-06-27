'Credits to this blog posts by Rock Star Josh Smith for this great solution 
'Converted from:
'http://www.codeplex.com/cracknetproject/SourceControl/DownloadSourceCode.aspx?changeSetId=14687
'
'
'This biggest change I made was to not register handlers for CanExecute if the command will always execute.
'This will save on performance
'If the handlers are not registered, the CanExecute function will get called when it is data bound.  This class
'always returns True if no handlers are registered for CanExecuteChanged
'
'There is a standard RelayCommand and a generic version to allow strong typed parameters as required.
'
Imports System.Windows.Input
Imports System.ComponentModel

Public NotInheritable Class RelayCommand
    Implements ICommand

#Region " Declarations "

    Private ReadOnly _objCanExecuteMethod As Predicate(Of Object) = Nothing
    Private ReadOnly _objExecuteMethod As Action(Of Object) = Nothing

#End Region

#Region " Events "

    Public Custom Event CanExecuteChanged As EventHandler Implements System.Windows.Input.ICommand.CanExecuteChanged
        AddHandler(ByVal value As EventHandler)

            If _objCanExecuteMethod IsNot Nothing Then
                AddHandler CommandManager.RequerySuggested, value
            End If

        End AddHandler
        '
        RemoveHandler(ByVal value As EventHandler)

            If _objCanExecuteMethod IsNot Nothing Then
                RemoveHandler CommandManager.RequerySuggested, value
            End If

        End RemoveHandler
        '
        RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)

            If _objCanExecuteMethod IsNot Nothing Then
                CommandManager.InvalidateRequerySuggested()
            End If

        End RaiseEvent
    End Event

#End Region

#Region " Constructor "

    Public Sub New(ByVal objExecuteMethod As Action(Of Object))
        Me.New(objExecuteMethod, Nothing)
    End Sub

    Public Sub New(ByVal objExecuteMethod As Action(Of Object), ByVal objCanExecuteMethod As Predicate(Of Object))

        If objExecuteMethod Is Nothing Then
            Throw New ArgumentNullException("objExecuteMethod", "Delegate comamnds can not be null")
        End If

        _objExecuteMethod = objExecuteMethod
        _objCanExecuteMethod = objCanExecuteMethod
    End Sub

#End Region

#Region " Methods "

    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute

        If _objCanExecuteMethod Is Nothing Then
            Return True

        Else
            Return _objCanExecuteMethod(parameter)
        End If

    End Function

    Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute

        If _objExecuteMethod Is Nothing Then
            Return

        Else
            _objExecuteMethod(parameter)
        End If

    End Sub

    Public Sub InvalidRequerySuggested()
        RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
    End Sub

#End Region

End Class

'
'=====================================================================================================
'=====================================================================================================
'=====================================================================================================
'
Public NotInheritable Class RelayCommand(Of T)
    Implements ICommand

#Region " Declarations "

    Private ReadOnly _objCanExecuteMethod As Predicate(Of T) = Nothing
    Private ReadOnly _objExecuteMethod As Action(Of T) = Nothing

#End Region

#Region " Events "

    Public Custom Event CanExecuteChanged As EventHandler Implements System.Windows.Input.ICommand.CanExecuteChanged
        AddHandler(ByVal value As EventHandler)

            If _objCanExecuteMethod IsNot Nothing Then
                AddHandler CommandManager.RequerySuggested, value
            End If

        End AddHandler
        '
        RemoveHandler(ByVal value As EventHandler)

            If _objCanExecuteMethod IsNot Nothing Then
                RemoveHandler CommandManager.RequerySuggested, value
            End If

        End RemoveHandler
        '
        RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)

            If _objCanExecuteMethod IsNot Nothing Then
                CommandManager.InvalidateRequerySuggested()
            End If

        End RaiseEvent
    End Event

#End Region

#Region " Constructors "

    Public Sub New(ByVal objExecuteMethod As Action(Of T))
        Me.New(objExecuteMethod, Nothing)
    End Sub

    Public Sub New(ByVal objExecuteMethod As Action(Of T), ByVal objCanExecuteMethod As Predicate(Of T))

        If objExecuteMethod Is Nothing Then
            Throw New ArgumentNullException("objExecuteMethod", "Delegate comamnds can not be null")
        End If

        _objExecuteMethod = objExecuteMethod
        _objCanExecuteMethod = objCanExecuteMethod
    End Sub

#End Region

#Region " Methods "

    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute

        If _objCanExecuteMethod Is Nothing Then
            Return True

        Else
            Return _objCanExecuteMethod(DirectCast(parameter, T))
        End If

    End Function

    Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
        _objExecuteMethod(DirectCast(parameter, T))
    End Sub

    Public Sub InvalidRequerySuggested()
        RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
    End Sub

#End Region

End Class
