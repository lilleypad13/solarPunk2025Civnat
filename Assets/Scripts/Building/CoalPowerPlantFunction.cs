using UnityEngine;

public class CoalPowerPlantFunction : BuildingFunction
{
    public override void Generate()
    {
        Debug.Log($"{gameObject.name} generated!");
    }

    public override void ApplyAreaPollution(NatureCellState[] natureCells)
    {
        //Debug.Log($"{gameObject.name} polluted!");
        for (int i = 0; i < natureCells.Length; i++)
        {
            natureCells[i].Pollute(pollutionAreaValue);
        }
    }

    public override void ApplyAreaEco(NatureCellState[] natureCells)
    {
        //Debug.Log($"{gameObject.name} area eco applied!");
        for (int i = 0; i < natureCells.Length; i++)
        {
            natureCells[i].ApplyEco(ecoAreaValue);
        }
    }

    public override int GetAppliedCommunityHealth(NatureCellState[] natureCells)
    {
        for (int i = 0; i < natureCells.Length; i++)
        {
            if (natureCells[i].PollutionLevel > 0)
            {
                return PollutedInherentCommunityHealthValue;
            }
        }
        return InherentCommunityHealthValue;
    }
}
