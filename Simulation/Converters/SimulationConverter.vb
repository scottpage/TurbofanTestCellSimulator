Public Class SimulationConverter

    Public Shared Function ToModelQuality(quality As SimulationStateService.Quality) As Models.Quality
        Select Case quality
            Case SimulationStateService.Quality.Bad
                Return Models.Quality.Bad
            Case SimulationStateService.Quality.Good
                Return Models.Quality.Good
        End Select
        Return Models.Quality.Suspect
    End Function

    Public Shared Function ToSimulationQuality(quality As Models.Quality) As SimulationStateService.Quality
        Select Case quality
            Case Models.Quality.Bad
                Return SimulationStateService.Quality.Bad
            Case Models.Quality.Good
                Return SimulationStateService.Quality.Good
        End Select
        Return SimulationStateService.Quality.Suspect
    End Function

    Public Shared Function ToModelEngineMode(mode As SimulationStateService.EngineMode) As Models.EngineMode
        Select Case mode
            Case SimulationStateService.EngineMode.AbortingStart
                Return Models.EngineMode.AbortingStart
            Case SimulationStateService.EngineMode.DryCranking
                Return Models.EngineMode.DryCranking
            Case SimulationStateService.EngineMode.Running
                Return Models.EngineMode.Running
            Case SimulationStateService.EngineMode.Starting
                Return Models.EngineMode.Starting
            Case SimulationStateService.EngineMode.Stopped
                Return Models.EngineMode.Stopped
            Case SimulationStateService.EngineMode.Stopping
                Return Models.EngineMode.Stopping
            Case SimulationStateService.EngineMode.WetCranking
                Return Models.EngineMode.WetCranking
        End Select
        Return Models.EngineMode.None
    End Function

    Public Shared Function ToSimulationEngineMode(mode As Models.EngineMode) As SimulationStateService.EngineMode
        Select Case mode
            Case Models.EngineMode.AbortingStart
                Return SimulationStateService.EngineMode.AbortingStart
            Case Models.EngineMode.DryCranking
                Return SimulationStateService.EngineMode.DryCranking
            Case Models.EngineMode.Running
                Return SimulationStateService.EngineMode.Running
            Case Models.EngineMode.Starting
                Return SimulationStateService.EngineMode.Starting
            Case Models.EngineMode.Stopped
                Return SimulationStateService.EngineMode.Stopped
            Case Models.EngineMode.Stopping
                Return SimulationStateService.EngineMode.Stopping
            Case Models.EngineMode.WetCranking
                Return SimulationStateService.EngineMode.WetCranking
        End Select
        Return SimulationStateService.EngineMode.None
    End Function

End Class
