Public Class EngineParameterView

    Public Property AnchorPoint As Point
        Get
            Return CType(GetValue(AnchorPointProperty), Point)
        End Get

        Set(ByVal value As Point)
            SetValue(AnchorPointProperty, value)
        End Set
    End Property

    Public Shared ReadOnly AnchorPointProperty As DependencyProperty = _
                           DependencyProperty.Register("AnchorPoint", _
                           GetType(Point), GetType(EngineParameterView), _
                           New FrameworkPropertyMetadata(New Point))

    Public Property IsInput As Boolean
        Get
            Return CBool(GetValue(IsInputProperty))
        End Get

        Set(ByVal value As Boolean)
            SetValue(IsInputProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IsInputProperty As DependencyProperty = _
                           DependencyProperty.Register("IsInput", _
                           GetType(Boolean), GetType(EngineParameterView), _
                           New FrameworkPropertyMetadata(True))

    Public Property Node As FrameworkElement
        Get
            Return CType(GetValue(NodeProperty), FrameworkElement)
        End Get

        Set(ByVal value As FrameworkElement)
            SetValue(NodeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NodeProperty As DependencyProperty = _
                           DependencyProperty.Register("Node", _
                           GetType(FrameworkElement), GetType(EngineParameterView), _
                           New FrameworkPropertyMetadata(Nothing))


    'Private Sub EngineParameterView_LayoutUpdated(sender As Object, e As System.EventArgs) Handles Me.LayoutUpdated
    '    Dim size = RenderSize
    '    Dim ofs = New Point(size.Width / 2, If(isInput, 0, size.Height))
    '    AnchorPoint = TransformToVisual(node.canvas).Transform(ofs)
    'End Sub

End Class
