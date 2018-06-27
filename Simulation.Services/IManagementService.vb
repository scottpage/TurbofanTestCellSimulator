<ServiceContract(SessionMode:=SessionMode.NotAllowed)>
Public Interface IManagementService

    <OperationContract(IsOneWay:=True)>
    Sub StartFullset(comment As String)
    <OperationContract(IsOneWay:=True)>
    Sub StopFullsetAsync()

End Interface
