<ServiceContract()>
Public Interface IControlService

    <OperationContract(IsOneWay:=True)>
    Sub Reset()
    <OperationContract(IsOneWay:=True)>
    Sub Shutdown()

End Interface
