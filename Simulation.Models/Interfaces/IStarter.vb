Public Interface IStarter

    Property Type As StarterType
    Function GetParameters() As IEnumerable(Of IParameter)
    Sub Update(value As Double)

End Interface
