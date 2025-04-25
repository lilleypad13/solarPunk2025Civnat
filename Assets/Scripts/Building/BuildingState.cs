using UnityEngine;

public class BuildingState
{
    public Building Building { get; private set; }
    public Vector3Int CellPosition { get; private set; }
    public BoundsInt Bounds 
    {
        get { return Building.area; }
    }

    public BuildingState(Building building, Vector3Int cellPosition)
    {
        Building = building;
        CellPosition = cellPosition;
    }
}
