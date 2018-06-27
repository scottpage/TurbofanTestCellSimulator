Public Interface IShaft
    Inherits IParameter

    Property CorrectedSpeed As Double
    Property CorrectedSpeedRoot20 As Double
    Property Number As Integer
    Property MinimumRPM As Double
    Property MaximumRPM As Double
    Property IsRotating As Boolean
    Property LaneATrackedOrderVibration As Parameter
    Property LaneBTrackedOrderVibration As Parameter

End Interface
