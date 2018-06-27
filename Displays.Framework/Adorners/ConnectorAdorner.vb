Public Class ConnectorAdorner
    Inherits Adorner

    Public Sub New(ByVal adornedElement As UIElement)
        MyBase.New(adornedElement)
    End Sub

    Protected Overrides Sub OnRender(drawingContext As System.Windows.Media.DrawingContext)
        MyBase.OnRender(drawingContext)
        Dim Bounds As New Rect(AdornedElement.DesiredSize)
        Dim RenderBrush As New SolidColorBrush(Colors.LightGray)
        Dim RenderPen As New Pen(New SolidColorBrush(Colors.Black), 1.5)
        Dim RenderRadius As Double
        RenderBrush.Opacity = 0.2
        RenderRadius = 5.0

        'Draw a circle at each corner.
        drawingContext.DrawEllipse(RenderBrush, RenderPen, New Point(Bounds.Width / 2, Bounds.Top), RenderRadius, RenderRadius)
        drawingContext.DrawEllipse(RenderBrush, RenderPen, New Point(Bounds.Width / 2, Bounds.Bottom), RenderRadius, RenderRadius)
        drawingContext.DrawEllipse(RenderBrush, RenderPen, New Point(Bounds.Left, Bounds.Height / 2), RenderRadius, RenderRadius)
        drawingContext.DrawEllipse(RenderBrush, RenderPen, New Point(Bounds.Right, Bounds.Height / 2), RenderRadius, RenderRadius)
    End Sub

End Class
