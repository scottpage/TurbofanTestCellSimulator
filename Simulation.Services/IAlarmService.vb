<ServiceContract(SessionMode:=SessionMode.NotAllowed)>
Public Interface IAlarmService

    <OperationContract(IsOneWay:=True)>
    Sub Acknowledge(parameterName As String)

End Interface
