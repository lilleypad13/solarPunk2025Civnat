using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingFunction function;
    public BuildingFunction Function { get { return function; } }
    public bool IsPlaced { get; private set; }
    public BoundsInt area;

    // Events
    public static event Action<Building, Vector3Int> OnPlaced;

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.Instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        IsPlaced = true;
        GridBuildingSystem.Instance.TakeArea(areaTemp);

        // Event
        OnPlaced?.Invoke(this, positionInt);
    }

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.Instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuildingSystem.Instance.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }

    // TODO: Can be unique per type of building.
    public void Pollute()
    {

    }
}
