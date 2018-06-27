Public Class PolynomialView

    Public Property PolynomialName As String
        Get
            Return CStr(GetValue(PolynomialNameProperty))
        End Get

        Set(ByVal value As String)
            SetValue(PolynomialNameProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PolynomialNameProperty As DependencyProperty = _
                           DependencyProperty.Register("PolynomialName", _
                           GetType(String), GetType(PolynomialView), _
                           New FrameworkPropertyMetadata("Polynomial"))

End Class
