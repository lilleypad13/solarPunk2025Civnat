using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private BuildButtonManager buildButtonManager;

    [Header("Data")]
    [SerializeField] private BuildingSetData baseBuildingSet;

    private BuildingSetState activeSet;

    private void OnEnable()
    {
        BuildingPlacementManager.OnBuildingPlaced += CheckIfPlacedSinglePlaceBuilding;
    }

    private void OnDisable()
    {
        BuildingPlacementManager.OnBuildingPlaced -= CheckIfPlacedSinglePlaceBuilding;
    }

    private void Start()
    {
        SetupBuildingSet(baseBuildingSet);
    }

    public void SetupBuildingSet(BuildingSetData set)
    {
        // Initialize data
        activeSet = new BuildingSetState(set);

        // Update buttons
        UpdateBuildButtons(set);
    }

    public void UpdateBuildButtons(BuildingSetData set)
    {
        buildButtonManager.Initialize(set);
    }

    public void CheckIfPlacedSinglePlaceBuilding(BuildingData buildingPlaced)
    {
        if (!activeSet.Data.CanDuplicatePlacements)
        {
            buildButtonManager.DisableButtonInteraction(buildingPlaced);
        }

        // Update Set State
        activeSet.BuildingPlaced(buildingPlaced);

        // Then check to advance state too.
        CheckToAdvanceState();
    }

    // Do on building being placed.
    // Could also connect to a button?
    private void CheckToAdvanceState()
    {
        Debug.Log("Checking to advance game state.");
        if(activeSet.HasPlacedAllBuildings)
        {
            // Advance
            Debug.Log("Advance");
        }
    }
}
