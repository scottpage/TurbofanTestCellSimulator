<DataContract()>
Public Class Ignitor
    Implements IIgnitor

    <DataMember()>
    Public Property IsOn As Boolean Implements IIgnitor.IsOn

End Class
