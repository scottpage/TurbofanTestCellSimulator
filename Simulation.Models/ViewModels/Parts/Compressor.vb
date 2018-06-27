<DataContract()>
Public Class Compressor
    Implements ICompressor

    Public Sub New()
        Pressure = New Parameter
        Temperature = New Parameter
    End Sub

    <DataMember()>
    Public Property Pressure As Parameter Implements ICompressor.Pressure
    <DataMember()>
    Public Property Temperature As Parameter Implements ICompressor.Temperature

    Public Sub Update(value As Double) Implements ICompressor.Update
        Pressure.Update(value)
        Temperature.Update(value)
    End Sub

End Class
