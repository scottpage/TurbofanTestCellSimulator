Imports System.Windows.Markup

<ContentProperty("InnerContent")> _
Public Class PLCDisplaySection

    <Category("Common")> _
    Public Property Header As String
        Get
            Return DirectCast(GetValue(HeaderProperty), String)
        End Get

        Set(ByVal value As String)
            SetValue(HeaderProperty, value)
        End Set
    End Property

    Public Shared ReadOnly HeaderProperty As DependencyProperty = _
                           DependencyProperty.Register("Header", _
                           GetType(String), GetType(PLCDisplaySection), _
                           New FrameworkPropertyMetadata(String.Empty))

    <Category("Common")> _
    Public Property InnerContent As FrameworkElement
        Get
            Return DirectCast(GetValue(InnerContentProperty), FrameworkElement)
        End Get

        Set(ByVal value As FrameworkElement)
            SetValue(InnerContentProperty, value)
        End Set
    End Property

    Public Shared ReadOnly InnerContentProperty As DependencyProperty = _
                           DependencyProperty.Register("InnerContent", _
                           GetType(FrameworkElement), GetType(PLCDisplaySection), _
                           New FrameworkPropertyMetadata(Nothing))

End Class
