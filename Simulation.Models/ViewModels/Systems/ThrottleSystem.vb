<DataContract()>
Public Class ThrottleSystem
    Implements IThrottleSystem

    Public Sub New()
        LeverAngle = New Parameter
        ResolverAngle = New Parameter
    End Sub

    <DataMember()>
    Public Property LeverAngle As Parameter Implements IThrottleSystem.LeverAngle
    <DataMember()>
    Public Property ResolverAngle As Parameter Implements IThrottleSystem.ResolverAngle

    Public Sub Update() Implements IThrottleSystem.Update
        ResolverAngle.Update(LeverAngle.Value.Current)
    End Sub

End Class
