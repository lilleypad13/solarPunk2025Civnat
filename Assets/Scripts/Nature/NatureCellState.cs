using UnityEngine;

public class NatureCellState
{
    public NatureCell Cell { get; private set; }
    public Vector3Int CellPosition { get; private set; }
    public int PollutionLevel { get; private set; } = 0;

    
    public NatureCellState(NatureCell cell, Vector3Int cellPosition)
    {
        Cell = cell;
        CellPosition = cellPosition;
        PollutionLevel = 0;
    }

    public NatureCellState(NatureCell cell, Vector3Int cellPosition, int pollutionLevel)
    {
        Cell = cell;
        CellPosition = cellPosition;
        PollutionLevel = pollutionLevel;
    }

    public void Pollute(int pollutionLevel)
    {
        PollutionLevel += pollutionLevel;
    }

    public void CleanPollution(int pollutionLevel)
    {
        PollutionLevel -= pollutionLevel;
    }
}
