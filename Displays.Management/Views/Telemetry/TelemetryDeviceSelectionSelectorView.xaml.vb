Public Class TelemetryDeviceSelectionSelectorView

    Public Property Command As ICommand
        Get
            Return CType(GetValue(CommandProperty), ICommand)
        End Get
        Set(ByVal value As ICommand)
            SetValue(CommandProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CommandProperty As DependencyProperty = _
                           DependencyProperty.Register("Command", _
                           GetType(ICommand), GetType(TelemetryDeviceSelectionSelectorView), _
                           New FrameworkPropertyMetadata(Nothing))

    Public Property IsSelected As Boolean
        Get
            Return CBool(GetValue(IsSelectedProperty))
        End Get

        Set(ByVal value As Boolean)
            SetValue(IsSelectedProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IsSelectedProperty As DependencyProperty = _
                           DependencyProperty.Register("IsSelected", _
                           GetType(Boolean), GetType(TelemetryDeviceSelectionSelectorView), _
                           New FrameworkPropertyMetadata(False))

    Public Property SelectionName As String
        Get
            Return CStr(GetValue(SelectionNameProperty))
        End Get
        Set(ByVal value As String)
            SetValue(SelectionNameProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SelectionNameProperty As DependencyProperty = _
                           DependencyProperty.Register("SelectionName", _
                           GetType(String), GetType(TelemetryDeviceSelectionSelectorView), _
                           New FrameworkPropertyMetadata(String.Empty))

End Class
