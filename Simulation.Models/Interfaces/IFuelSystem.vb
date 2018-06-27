Public Interface IFuelSystem

    Property Pressure As Parameter
    Property Temperature As Parameter
    Property Valve As Valve
    Sub Update(value As Double)

End Interface
