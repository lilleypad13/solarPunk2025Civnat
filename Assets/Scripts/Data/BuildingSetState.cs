using System.Collections.Generic;
using UnityEngine;

public class BuildingSetState
{
    public BuildingSetData Data {  get; private set; }
    public List<BuildingData> BuildingsPlacedDuringSet { get; private set; }

    public bool HasPlacedAllBuildings
    {
        get
        {
            return BuildingsPlacedDuringSet.Count >= Data.CountBuildingsToPlace;
        }
    }

    #region Constructors
    public BuildingSetState(BuildingSetData data)
    {
        this.Data = data;
        BuildingsPlacedDuringSet = new List<BuildingData>();
    }
    #endregion

    public void BuildingPlaced(BuildingData building)
    {
        BuildingsPlacedDuringSet.Add(building);
    }
}
