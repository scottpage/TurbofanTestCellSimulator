Public Class TransientLogListView

    Public Shared ReadOnly HeaderProperty As DependencyProperty = _
                           DependencyProperty.Register("Header", _
                           GetType(String), GetType(TransientLogListView), _
                           New FrameworkPropertyMetadata(Nothing))

    Public Property Header As String
        Get
            Return TryCast(GetValue(HeaderProperty), String)
        End Get

        Set(ByVal value As String)
            SetValue(HeaderProperty, value)
        End Set
    End Property

End Class
