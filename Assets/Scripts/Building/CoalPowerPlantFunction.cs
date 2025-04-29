using UnityEngine;

public class CoalPowerPlantFunction : BuildingFunction
{
    public override void Generate()
    {
        Debug.Log($"{gameObject.name} generated!");
    }

    public override void Pollute(NatureCellState[] natureCells)
    {
        Debug.Log($"{gameObject.name} polluted!");
        for (int i = 0; i < natureCells.Length; i++)
        {
            natureCells[i].Pollute(pollutionAreaValue);
        }
    }
}
