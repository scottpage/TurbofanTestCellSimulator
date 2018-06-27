Public Class TelemetryDeviceDataTemplateSelector
    Inherits DataTemplateSelector

    Public Property CHINCADataTemplate As DataTemplate
    Public Property DIGBERTDataTemplate As DataTemplate
    Public Property STINCADataTemplate As DataTemplate

    Public Overrides Function SelectTemplate(item As Object, container As System.Windows.DependencyObject) As System.Windows.DataTemplate
        If TypeOf item Is CHINCAControlViewModel Then Return CHINCADataTemplate
        If TypeOf item Is DIGBERTControlViewModel Then Return DIGBERTDataTemplate
        If TypeOf item Is STINCAControlViewModel Then Return STINCADataTemplate
        Return MyBase.SelectTemplate(item, container)
    End Function

End Class
