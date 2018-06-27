<DataContract()>
Public Class Valve
    Implements IValve

    <DataMember()>
    Public Property IsOpen As Boolean Implements IValve.IsOpen

End Class
