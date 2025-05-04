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
    public static event Action OnGenerationConcluded;
    public static event Action<BuildingSetData[]> OnChoicesCrossroad;

    private void OnEnable()
    {
        BuildingPlacementManager.OnBuildingPlaced += CheckIfPlacedSinglePlaceBuilding;
        SetChoice.OnSelectedSet += ProceedAfterChoiceSelected;
    }

    private void OnDisable()
    {
        BuildingPlacementManager.OnBuildingPlaced -= CheckIfPlacedSinglePlaceBuilding;
        SetChoice.OnSelectedSet -= ProceedAfterChoiceSelected;
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
            
            if (phaseIndex < progressionData.PhaseSets.Length)
            {
                // End Phase Event / World Tick
                OnPhaseConcluded?.Invoke();
                // Continue
                // Clear out building buttons.
                buildButtonManager.Clear();
                // Populate with next progression phase of buttons.
                if(progressionData.PhaseSets[phaseIndex].BuildingSets.Length > 1)
                {
                    // Go to choices panel
                    OnChoicesCrossroad?.Invoke(progressionData.PhaseSets[phaseIndex].BuildingSets);
                }
                else
                {
                    // Just do forced route.
                    SetupBuildingSet(progressionData.PhaseSets[phaseIndex].BuildingSets[0]);
                }
            }
            else
            {
                // End
                Debug.Log("End of Progression Phases.");
                // End Phase Event / Generation Tick
                OnGenerationConcluded?.Invoke();
                // Clear out building buttons.
                buildButtonManager.Clear();
            }
            
        }
    }

    private void ProceedAfterChoiceSelected(BuildingSetData setData)
    {
        SetupBuildingSet(setData);
    }
}
