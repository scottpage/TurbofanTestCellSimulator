<DataContract(), XmlInclude(GetType(PneumaticStarter)), KnownType(GetType(PneumaticStarter))>
Public MustInherit Class Starter
    Implements IStarter

    Protected Sub New(type As StarterType)
        _Type = type
    End Sub

    <DataMember()> _
    Public Property Type As StarterType Implements IStarter.Type

    Public MustOverride Function GetParameters() As System.Collections.Generic.IEnumerable(Of IParameter) Implements IStarter.GetParameters

    Public MustOverride Sub Update(value As Double) Implements IStarter.Update

End Class
