<ServiceContract()>
Public Interface ISimulationStateService

    <OperationContract(IsOneWay:=True)>
    Sub Update(state As SimulationState)

End Interface
