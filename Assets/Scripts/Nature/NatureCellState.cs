using UnityEngine;

public class NatureCellState
{
    public NatureCell Cell { get; private set; }
    public Vector3Int CellPosition { get; private set; }
    public int PollutionLevel { get; private set; } = 0;
    public int EcoLevel { get; private set; } = 0;

    private int cumulativePollutionApplication = 0;
    private int cumulativeEcoAppliction = 0;

    #region Constructors
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
    #endregion


    public void Pollute(int pollutionLevel)
    {
        cumulativePollutionApplication += pollutionLevel;
    }

    public void ApplyEco (int ecoLevel)
    {
        cumulativeEcoAppliction += ecoLevel;
    }

    public void ApplyCumulativeValues()
    {
        // Threshold apply cumulative values.
        if(PollutionLevel + cumulativePollutionApplication < 0)
        {
            PollutionLevel = 0;
        }
        else
        {
            PollutionLevel += cumulativePollutionApplication;
        }

        if(EcoLevel + cumulativeEcoAppliction < 0)
        {
            EcoLevel = 0;
        }
        else
        {
            EcoLevel += cumulativeEcoAppliction;
        }

        // Reset after applications
        cumulativePollutionApplication = 0;
        cumulativeEcoAppliction = 0;
    }
}
