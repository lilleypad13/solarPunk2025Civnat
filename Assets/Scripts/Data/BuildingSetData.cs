using UnityEngine;

[CreateAssetMenu(fileName = "BuildingSetData", menuName = "Scriptable Objects/BuildingSetData")]
public class BuildingSetData : ScriptableObject
{
    [SerializeField] private int buildingsToPlace = 1;
    public int BuildingsToPlace { get => buildingsToPlace; }
    [SerializeField] private bool canDuplicatePlacements = true;
    public bool CanDuplicatePlacements { get => canDuplicatePlacements; }
    [SerializeField] private BuildingData[] buildings;
    public BuildingData[] Buildings {  get => buildings; }
}
