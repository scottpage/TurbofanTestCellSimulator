<ServiceContract(SessionMode:=SessionMode.NotAllowed)>
Public Interface IThrottleService

    <OperationContract(IsOneWay:=False)>
    Function GetLeverAngle() As Double
    <OperationContract(IsOneWay:=True)>
    Sub UpdateLeverAngle(value As Double)
    <OperationContract(IsOneWay:=False)>
    Function GetRange() As Range

End Interface
