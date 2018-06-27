Imports System.Collections.Specialized

''' <summary>
''' This class contains a few useful extenders for the ListBox
''' </summary>
Public Class ListViewExtenders
    Inherits DependencyObject
    Public Shared ReadOnly AutoScrollToEndProperty As DependencyProperty = DependencyProperty.RegisterAttached("AutoScrollToEnd", GetType(Boolean), GetType(ListViewExtenders), New UIPropertyMetadata(False, AddressOf OnAutoScrollToEndChanged))

    ''' <summary>
    ''' Returns the value of the AutoScrollToEndProperty
    ''' </summary>
    ''' <param name="obj">The dependency-object whichs value should be returned</param>
    ''' <returns>The value of the given property</returns>
    Public Shared Function GetAutoScrollToEnd(obj As DependencyObject) As Boolean
        Return CBool(obj.GetValue(AutoScrollToEndProperty))
    End Function

    ''' <summary>
    ''' Sets the value of the AutoScrollToEndProperty
    ''' </summary>
    ''' <param name="obj">The dependency-object whichs value should be set</param>
    ''' <param name="value">The value which should be assigned to the AutoScrollToEndProperty</param>
    Public Shared Sub SetAutoScrollToEnd(obj As DependencyObject, value As Boolean)
        obj.SetValue(AutoScrollToEndProperty, value)
    End Sub

    ''' <summary>
    ''' This method will be called when the AutoScrollToEnd
    ''' property was changed
    ''' </summary>
    ''' <param name="d">The sender (the ListBox)</param>
    ''' <param name="args">Some additional information</param>
    Public Shared Sub OnAutoScrollToEndChanged(d As DependencyObject, args As DependencyPropertyChangedEventArgs)
        Dim ListView = TryCast(d, ListBox)
        Dim listBoxItems = ListView.Items
        Dim data = TryCast(listBoxItems.SourceCollection, INotifyCollectionChanged)
        Dim ScrollToEndHandler As New NotifyCollectionChangedEventHandler(Sub(s, e)
                                                                              If ListView.Items.Count > 0 Then
                                                                                  Dim LastItem = ListView.Items(ListView.Items.Count - 1)
                                                                                  ListView.ScrollIntoView(LastItem)
                                                                              End If
                                                                          End Sub)
        If CBool(args.NewValue) Then
            AddHandler data.CollectionChanged, ScrollToEndHandler
        Else
            RemoveHandler data.CollectionChanged, ScrollToEndHandler
        End If
    End Sub

End Class
