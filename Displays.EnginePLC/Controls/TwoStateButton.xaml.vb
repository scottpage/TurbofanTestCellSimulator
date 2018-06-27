Public Class TwoStateButton

    Public Shared ReadOnly StateProperty As DependencyProperty = DependencyProperty.Register("State", GetType(Boolean), GetType(TwoStateButton), New FrameworkPropertyMetadata(False))
    <Category("Common")> _
    Public Property State As Boolean
        Get
            Return CType(GetValue(StateProperty), Boolean)
        End Get
        Set(ByVal value As Boolean)
            SetValue(StateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IsIndicatorEnabledProperty As DependencyProperty = DependencyProperty.Register("IsIndicatorEnabled", GetType(Boolean), GetType(TwoStateButton), New FrameworkPropertyMetadata(True))
    <Category("Common")> _
    Public Property IsIndicatorEnabled As Boolean
        Get
            Return CType(GetValue(IsIndicatorEnabledProperty), Boolean)
        End Get
        Set(ByVal value As Boolean)
            SetValue(IsIndicatorEnabledProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IndicatorFillProperty As DependencyProperty = DependencyProperty.Register("IndicatorFill", GetType(Brush), GetType(TwoStateButton), New FrameworkPropertyMetadata(Brushes.Lime))
    <Category("Brushes")> _
    Public Property IndicatorFill As Brush
        Get
            Return CType(GetValue(IndicatorFillProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(IndicatorFillProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IndicatorVisiblityProperty As DependencyProperty = DependencyProperty.Register("IndicatorVisiblity", GetType(Visibility), GetType(TwoStateButton), New FrameworkPropertyMetadata(Visibility.Hidden))
    <Category("Visibility")> _
    Public Property IndicatorVisiblity As Visibility
        Get
            Return CType(GetValue(IndicatorVisiblityProperty), Visibility)
        End Get
        Set(ByVal value As Visibility)
            SetValue(IndicatorVisiblityProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TopTextProperty As DependencyProperty = DependencyProperty.Register("TopText", GetType(String), GetType(TwoStateButton), New FrameworkPropertyMetadata(String.Empty))
    <Category("Text")> _
    Public Property TopText As String
        Get
            Return Convert.ToString(GetValue(TopTextProperty))
        End Get
        Set(ByVal value As String)
            SetValue(TopTextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly BottomTextProperty As DependencyProperty = DependencyProperty.Register("BottomText", GetType(String), GetType(TwoStateButton), New FrameworkPropertyMetadata(String.Empty))
    <Category("Text")> _
    Public Property BottomText As String
        Get
            Return Convert.ToString(GetValue(BottomTextProperty))
        End Get
        Set(ByVal value As String)
            SetValue(BottomTextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TopTextSelectedForegroundProperty As DependencyProperty = DependencyProperty.Register("TopTextSelectedForeground", GetType(Brush), GetType(TwoStateButton), New FrameworkPropertyMetadata(Brushes.Black))
    <Category("Brushes")> _
    Public Property TopTextSelectedForeground As Brush
        Get
            Return CType(GetValue(TopTextSelectedForegroundProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(TopTextSelectedForegroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TopTextNotSelectedForegroundProperty As DependencyProperty = DependencyProperty.Register("TopTextNotSelectedForeground", GetType(Brush), GetType(TwoStateButton), New FrameworkPropertyMetadata(Brushes.Gray))
    <Category("Brushes")> _
    Public Property TopTextNotSelectedForeground As Brush
        Get
            Return CType(GetValue(TopTextNotSelectedForegroundProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(TopTextNotSelectedForegroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly BottomTextSelectedForegroundProperty As DependencyProperty = DependencyProperty.Register("BottomTextSelectedForeground", GetType(Brush), GetType(TwoStateButton), New FrameworkPropertyMetadata(Brushes.Black))
    <Category("Brushes")> _
    Public Property BottomTextSelectedForeground As Brush
        Get
            Return CType(GetValue(BottomTextSelectedForegroundProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(BottomTextSelectedForegroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly BottomTextNotSelectedForegroundProperty As DependencyProperty = DependencyProperty.Register("BottomTextNotSelectedForeground", GetType(Brush), GetType(TwoStateButton), New FrameworkPropertyMetadata(Brushes.Gray))
    <Category("Brushes")> _
    Public Property BottomTextNotSelectedForeground As Brush
        Get
            Return CType(GetValue(BottomTextNotSelectedForegroundProperty), Brush)
        End Get
        Set(ByVal value As Brush)
            SetValue(BottomTextNotSelectedForegroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CommandProperty As DependencyProperty = DependencyProperty.Register("Command", GetType(ICommand), GetType(TwoStateButton), New FrameworkPropertyMetadata(Nothing))
    <Category("Common")> _
    Public Property Command As ICommand
        Get
            Return DirectCast(GetValue(CommandProperty), ICommand)
        End Get
        Set(ByVal value As ICommand)
            SetValue(CommandProperty, value)
        End Set
    End Property

    Private Sub TwoStateButton_IsEnabledChanged(sender As Object, e As System.Windows.DependencyPropertyChangedEventArgs) Handles Me.IsEnabledChanged
        If Not IsEnabled Then State = False
    End Sub

End Class
