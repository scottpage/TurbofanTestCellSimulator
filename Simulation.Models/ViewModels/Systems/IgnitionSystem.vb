Public Class IgnitionSystem
    Implements IIgnitionSystem

    Private _State As IgnitionState = IgnitionState.Off

    Public Sub New()
        Ignitor1 = New Ignitor
        Ignitor2 = New Ignitor
    End Sub

    <DataMember()>
    Public Property Ignitor1 As Ignitor Implements IIgnitionSystem.Ignitor1
    <DataMember()>
    Public Property Ignitor2 As Ignitor Implements IIgnitionSystem.Ignitor2

    <DataMember()>
    Public Property State As IgnitionState Implements IIgnitionSystem.State
        Get
            Return _State
        End Get
        Set(value As IgnitionState)
            _State = value
            Select Case State
                Case IgnitionState.Armed, IgnitionState.Off
                    Ignitor1.IsOn = False
                    Ignitor2.IsOn = False
                Case IgnitionState.On
                    Ignitor1.IsOn = True
                    Ignitor2.IsOn = True
            End Select
        End Set
    End Property

End Class
