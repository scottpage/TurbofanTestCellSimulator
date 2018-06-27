<ServiceContract()>
Public Interface IUnifiedEventLogService

    <OperationContract(IsOneWay:=True)>
    Sub LogEntry(severity As Severity, source As String, message As String)

End Interface
