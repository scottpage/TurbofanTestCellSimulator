Public NotInheritable Class GraphEdge
    Inherits UserControl

    Public Shared ReadOnly SourceProperty As DependencyProperty = DependencyProperty.Register("Source", GetType(Point), GetType(EngineParameterView), New FrameworkPropertyMetadata(Nothing))
    Public Property Source() As Point
        Get
            Return DirectCast(Me.GetValue(SourceProperty), Point)
        End Get
        Set(value As Point)
            Me.SetValue(SourceProperty, value)
        End Set
    End Property

    Public Shared ReadOnly DestinationProperty As DependencyProperty = DependencyProperty.Register("Destination", GetType(Point), GetType(EngineParameterView), New FrameworkPropertyMetadata(Nothing))
    Public Property Destination() As Point
        Get
            Return DirectCast(Me.GetValue(DestinationProperty), Point)
        End Get
        Set(value As Point)
            Me.SetValue(DestinationProperty, value)
        End Set
    End Property

    Public Sub New()
        Dim segment As New LineSegment(Nothing, True)
        Dim figure As New PathFigure(Nothing, New LineSegment() {segment}, False)
        Dim geometry As New PathGeometry(New PathFigure() {figure})
        Dim sourceBinding As New Binding With {.Source = Me, _
                                               .Path = New PropertyPath(SourceProperty)}
        Dim destinationBinding As New Binding()
        destinationBinding.Source = Me
        destinationBinding.Path = New PropertyPath(DestinationProperty)
        BindingOperations.SetBinding(figure, PathFigure.StartPointProperty, sourceBinding)
        BindingOperations.SetBinding(segment, LineSegment.PointProperty, destinationBinding)
        Content = New Path With {.Data = geometry, _
                                 .StrokeThickness = 5, _
                                 .Stroke = Brushes.White, _
                                 .MinWidth = 1, _
                                 .MinHeight = 1}
    End Sub

End Class
