using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private BuildingSetData baseBuildingSet;
    [SerializeField] private GameProgressionData progressionData;

    [Header("UI")]
    [SerializeField] private BuildButtonManager buildButtonManager;

    private BuildingSetState activeSet;
    private int phaseIndex = 0;

    // Events
    public static event Action OnPhaseConcluded;

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
        // TODO: Just setup for testing right now.
        SetupBuildingSet(progressionData.PhaseSets[0].BuildingSets[0]);
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
            // Increment phase
            phaseIndex++;
            // End Phase Event / World Tick
            OnPhaseConcluded?.Invoke();
            if (phaseIndex < progressionData.PhaseSets.Length)
            {
                // Continue
                // Clear out building buttons.
                buildButtonManager.Clear();
                // Populate with next progression phase of buttons.
                // TODO: Skipping choices inclusion for quicker itteration and testing for now.
                SetupBuildingSet(progressionData.PhaseSets[phaseIndex].BuildingSets[0]);
            }
            else
            {
                // End
                Debug.Log("End of Progression Phases.");
                // Clear out building buttons.
                buildButtonManager.Clear();
            }
            
        }
    }
}
