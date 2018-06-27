<DataContract()> _
Public NotInheritable Class TrentXWBParameters
    Inherits TrentParameters

    <DataMember()>
    Public Property StarterPressure As New ParameterValue

    Public Overloads Sub Update(values As TrentXWBParameters)
        MyBase.Update(values)
        StarterPressure.Update(values.StarterPressure)
    End Sub

End Class
