Public Interface IElectricalStarter
    Inherits IStarter

    Property CrankingVoltage As Parameter
    Property CrankingCurrent As Parameter
    Property IsCranking As Boolean

End Interface
