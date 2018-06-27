Public Class AlarmTypeToStringConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If Not TypeOf value Is AlarmType Then Return DependencyProperty.UnsetValue
        Select Case DirectCast(value, AlarmType)
            Case AlarmType.High
                Return "HI"
            Case AlarmType.HighCritical
                Return "HIHI"
            Case AlarmType.Low
                Return "LO"
            Case AlarmType.LowCritical
                Return "LOLO"
        End Select
        Return DependencyProperty.UnsetValue
    End Function

    Public Function ConvertBack(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        If value Is Nothing Then Return DependencyProperty.UnsetValue
        Select Case value.ToString
            Case "HI"
                Return AlarmType.High
            Case "HIHI"
                Return AlarmType.HighCritical
            Case "LO"
                Return AlarmType.Low
            Case "LOLO"
                Return AlarmType.LowCritical
        End Select
        Return DependencyProperty.UnsetValue
    End Function

End Class
