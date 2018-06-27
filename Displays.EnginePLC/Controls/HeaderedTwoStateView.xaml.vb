Public Class HeaderedTwoStateView

    Public Shared ReadOnly HeaderProperty As DependencyProperty = _
                       DependencyProperty.Register("Header", _
                       GetType(String), GetType(HeaderedTwoStateView), _
                       New FrameworkPropertyMetadata("Header"))

    <Category("Common")> _
    Public Property Header As String
        Get
            Return DirectCast(GetValue(HeaderProperty), String)
        End Get

        Set(ByVal value As String)
            SetValue(HeaderProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateOneTextProperty As DependencyProperty = _
                   DependencyProperty.Register("StateOneText", _
                   GetType(String), GetType(HeaderedTwoStateView), _
                   New FrameworkPropertyMetadata("State #1"))

    <Category("Common")> _
    Public Property StateOneText As String
        Get
            Return DirectCast(GetValue(StateOneTextProperty), String)
        End Get

        Set(ByVal value As String)
            SetValue(StateOneTextProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateTwoTextProperty As DependencyProperty = _
               DependencyProperty.Register("StateTwoText", _
               GetType(String), GetType(HeaderedTwoStateView), _
               New FrameworkPropertyMetadata("State #2"))

    <Category("Common")> _
    Public Property StateTwoText As String
        Get
            Return DirectCast(GetValue(StateTwoTextProperty), String)
        End Get

        Set(ByVal value As String)
            SetValue(StateTwoTextProperty, value)
        End Set
    End Property

#Region "Background Brushes"

    Public Shared ReadOnly StateOneSelectedBackgroundProperty As DependencyProperty = _
                           DependencyProperty.Register("StateOneSelectedBackground", _
                           GetType(Brush), GetType(HeaderedTwoStateView), _
                           New FrameworkPropertyMetadata(Brushes.Green))

    <Category("Common")> _
    Public Property StateOneSelectedBackground As Brush
        Get
            Return DirectCast(GetValue(StateOneSelectedBackgroundProperty), Brush)
        End Get

        Set(ByVal value As Brush)
            SetValue(StateOneSelectedBackgroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateOneNotSelectedBackgroundProperty As DependencyProperty = _
                       DependencyProperty.Register("StateOneNotSelectedBackground", _
                       GetType(Brush), GetType(HeaderedTwoStateView), _
                       New FrameworkPropertyMetadata(Brushes.Transparent))

    <Category("Common")> _
    Public Property StateOneNotSelectedBackground As Brush
        Get
            Return DirectCast(GetValue(StateOneNotSelectedBackgroundProperty), Brush)
        End Get

        Set(ByVal value As Brush)
            SetValue(StateOneNotSelectedBackgroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateOneBackgroundProperty As DependencyProperty = _
                   DependencyProperty.Register("StateOneBackground", _
                   GetType(Brush), GetType(HeaderedTwoStateView), _
                   New FrameworkPropertyMetadata(Brushes.Green))

    <Category("Common")> _
    Public Property StateOneBackground As Brush
        Get
            Return DirectCast(GetValue(StateOneBackgroundProperty), Brush)
        End Get

        Set(ByVal value As Brush)
            SetValue(StateOneBackgroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateTwoSelectedBackgroundProperty As DependencyProperty = _
                       DependencyProperty.Register("StateTwoSelectedBackground", _
                       GetType(Brush), GetType(HeaderedTwoStateView), _
                       New FrameworkPropertyMetadata(Brushes.Red))

    <Category("Common")> _
    Public Property StateTwoSelectedBackground As Brush
        Get
            Return DirectCast(GetValue(StateTwoSelectedBackgroundProperty), Brush)
        End Get

        Set(ByVal value As Brush)
            SetValue(StateTwoSelectedBackgroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateTwoNotSelectedBackgroundProperty As DependencyProperty = _
                   DependencyProperty.Register("StateTwoNotSelectedBackground", _
                   GetType(Brush), GetType(HeaderedTwoStateView), _
                   New FrameworkPropertyMetadata(Brushes.Transparent))

    <Category("Common")> _
    Public Property StateTwoNotSelectedBackground As Brush
        Get
            Return DirectCast(GetValue(StateTwoNotSelectedBackgroundProperty), Brush)
        End Get

        Set(ByVal value As Brush)
            SetValue(StateTwoNotSelectedBackgroundProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateTwoBackgroundProperty As DependencyProperty = _
               DependencyProperty.Register("StateTwoBackground", _
               GetType(Brush), GetType(HeaderedTwoStateView), _
               New FrameworkPropertyMetadata(Brushes.Transparent))

    <Category("Common")> _
    Public Property StateTwoBackground As Brush
        Get
            Return DirectCast(GetValue(StateTwoBackgroundProperty), Brush)
        End Get

        Set(ByVal value As Brush)
            SetValue(StateTwoBackgroundProperty, value)
        End Set
    End Property

#End Region

#Region "Commands"

    <Category("Common")>
    Public Property StateOneCommand As ICommand
        Get
            Return CType(GetValue(StateOneCommandProperty), ICommand)
        End Get

        Set(ByVal value As ICommand)
            SetValue(StateOneCommandProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateOneCommandProperty As DependencyProperty = _
                           DependencyProperty.Register("StateOneCommand", _
                           GetType(ICommand), GetType(HeaderedTwoStateView), _
                           New FrameworkPropertyMetadata(Nothing))

    <Category("Common")>
    Public Property StateTwoCommand As ICommand
        Get
            Return CType(GetValue(StateTwoCommandProperty), ICommand)
        End Get

        Set(ByVal value As ICommand)
            SetValue(StateTwoCommandProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StateTwoCommandProperty As DependencyProperty = _
                           DependencyProperty.Register("StateTwoCommand", _
                           GetType(ICommand), GetType(HeaderedTwoStateView), _
                           New FrameworkPropertyMetadata(Nothing))

#End Region

    Public Shared ReadOnly StateProperty As DependencyProperty = _
                       DependencyProperty.Register("State", _
                       GetType(Boolean), GetType(HeaderedTwoStateView), _
                       New FrameworkPropertyMetadata(False))

    <Category("Common")> _
    Public Property State As Boolean
        Get
            Return DirectCast(GetValue(StateProperty), Boolean)
        End Get
        Set(ByVal value As Boolean)
            SetValue(StateProperty, value)
        End Set
    End Property

End Class
