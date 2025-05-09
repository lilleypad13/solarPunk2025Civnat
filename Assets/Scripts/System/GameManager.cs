using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Data")]
    [SerializeField] private BuildingSetData baseBuildingSet;
    public BuildingSetData BaseBuildingSet { get => baseBuildingSet; }
    [SerializeField] private GameProgressionData[] progressionDatas;
    public GameProgressionData[] ProgressionDatas { get => progressionDatas; }
    [SerializeField] private GoalData goalData;
    public GoalData GoalData { get => goalData; }

    [Header("UI")]
    [SerializeField] private BuildButtonManager buildButtonManager;

    private BuildingSetState activeSet;
    public BuildingSetState ActiveSet { get => activeSet; }
    private int phaseIndex = 0;
    public int PhaseIndex { get => phaseIndex; }
    private int generationIndex = 0;
    public int GenerationIndex { get => generationIndex; }
    private GameProgressionData currentGeneration
    {
        get
        { 
            if(generationIndex < progressionDatas.Length)
            {
                return progressionDatas[generationIndex];
            }
            else
            {
                return null;
            }
        }
    }
    public GameProgressionData CurrentGeneration { get => currentGeneration; }

    // Events
    public static event Action OnPhaseConcluded;
    public static event Action OnGenerationConcluded;
    public static event Action<BuildingSetData[]> OnChoicesCrossroad;
    public static event Action OnGameStateUpdate;
    public static event Action OnGameStartSetupComplete;
    public static event Action OnGameEnd;

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

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    private void Start()
    {
        // TODO: Just setup for testing right now.
        SetupBuildingSet(progressionDatas[0].PhaseSets[0].BuildingSets[0]);

        // Event Signaling
        OnGameStartSetupComplete?.Invoke();
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
            
            if (phaseIndex < progressionDatas[generationIndex].PhaseSets.Length)
            {
                // End Phase Event / World Tick
                OnPhaseConcluded?.Invoke();
                // Continue
                // Clear out building buttons.
                buildButtonManager.Clear();
                // Populate with next progression phase of buttons.
                if(progressionDatas[generationIndex].PhaseSets[phaseIndex].BuildingSets.Length > 1)
                {
                    // Go to choices panel
                    OnChoicesCrossroad?.Invoke(progressionDatas[generationIndex].PhaseSets[phaseIndex].BuildingSets);
                }
                else
                {
                    // Just do forced route.
                    SetupBuildingSet(progressionDatas[generationIndex].PhaseSets[phaseIndex].BuildingSets[0]);
                }
            }
            else
            {
                // Completed generation
                Debug.Log("End of Progression Phase.");
                // End Phase Event / Generation Tick
                OnGenerationConcluded?.Invoke();
                // Update index
                generationIndex++;
                // Clear out building buttons.
                buildButtonManager.Clear();
                if (generationIndex < progressionDatas.Length)
                {
                    // Continue to next generation
                    Debug.Log("Start next generation.");
                    // TODO: Probably something extra eventually, but this will just jump to first set in next gen for now.
                    // Doesn't support choices in first phase currently.
                    // Reset phase index
                    phaseIndex = 0;
                    SetupBuildingSet(progressionDatas[generationIndex].PhaseSets[phaseIndex].BuildingSets[0]);
                }
                else
                {
                    // END
                    Debug.Log("End game");

                    // Event signal
                    OnGameEnd?.Invoke();
                }
            }
            
        }

        OnGameStateUpdate?.Invoke();
    }

    private void ProceedAfterChoiceSelected(BuildingSetData setData)
    {
        SetupBuildingSet(setData);
    }
}
