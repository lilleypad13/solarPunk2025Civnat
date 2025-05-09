using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldStateSystem : MonoBehaviour
{
    [SerializeField] private Tilemap mainTileMap;

    [Header("Tile Types")]
    [SerializeField] private TileBase defaultTile;
    [SerializeField] private TileBase clearTile;
    [SerializeField] private TileBase blockedTile;
    [SerializeField] private TileBase waterTile;

    [Header("Parameters")]
    [SerializeField] private int countWorldTicksPerGeneration = 5;

    // Data
    private List<BuildingState> buildingStates = new List<BuildingState>();
    private NatureCellState[] natureCellStates;
    private int totalPollution = 0;
    private int totalEco = 0;
    private int totalEnergy = 0;
    private int totalCommunityHealth = 0;
    public int TotalPollution { get { return totalPollution; } }
    public int TotalEco {  get { return totalEco; } }
    public int TotalEnergy { get { return totalEnergy; } }
    public int TotalCommunityHealth { get { return totalCommunityHealth; } }

    [Header("TESTING")]
    [SerializeField] private Gradient grassPollutionGradient;

    // Events
    public static event Action<WorldStateSystem> OnWorldTickCompleted;
    public static event Action<WorldStateSystem> OnEndWorldState;

    private void OnEnable()
    {
        Building.OnPlaced += AddBuilding;
        GameManager.OnPhaseConcluded += WorldTick;
        GameManager.OnGenerationConcluded += GenerationTick;
        GameManager.OnGameEnd += SignalEndGameState;
    }

    private void OnDisable()
    {
        Building.OnPlaced -= AddBuilding;
        GameManager.OnPhaseConcluded -= WorldTick;
        GameManager.OnGenerationConcluded -= GenerationTick;
        GameManager.OnGameEnd -= SignalEndGameState;
    }

    private void Start()
    {
        InitializeWorld();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CountCurrentDefaultTiles();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            WorldTick();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            OnEndWorldState?.Invoke(this);
        }
    }

    private void InitializeWorld()
    {
        List<NatureCellState> cellStatesList = new List<NatureCellState>();
        for (int x = mainTileMap.cellBounds.min.x; x < mainTileMap.cellBounds.max.x; x++)
        {
            for (int y = mainTileMap.cellBounds.min.y; y < mainTileMap.cellBounds.max.y; y++)
            {
                for (int z = mainTileMap.cellBounds.min.z; z < mainTileMap.cellBounds.max.z; z++)
                {
                    TileBase tile = mainTileMap.GetTile(new Vector3Int(x, y, z));
                    if (tile == defaultTile)
                    {
                        cellStatesList.Add(new NatureCellState(new GrassCell(), new Vector3Int(x, y, z)));
                    }
                    else if (tile == waterTile)
                    {
                        // Add as water tile instead.
                        cellStatesList.Add(new NatureCellState(new WaterCell(), new Vector3Int(x, y, z)));
                    }
                }
            }
        }

        natureCellStates = cellStatesList.ToArray();



        DebugNatureCells();
    }

    private void WorldTick()
    {
        Debug.Log("Begin world tick.");
        // Handle Inherent Values
        int tickPollution = 0;
        int tickEnergy = 0;
        int tickEco = 0;
        int tickCommunityHealth = 0;
        for (int i = 0; i < buildingStates.Count; i++)
        {
            tickPollution += buildingStates[i].Building.Function.InherentPollutionValue;
            tickEnergy += buildingStates[i].Building.Function.InherentEnergyValue;
            tickEco += buildingStates[i].Building.Function.InherentEcoValue;
        }

        // Handle Area Values
        for (int i = 0; i < buildingStates.Count; i++)
        {
            buildingStates[i].Building.Function.ApplyAreaPollution(
                GetNatureCellsInArea(
                    buildingStates[i].CellPosition + buildingStates[i].Building.Function.PollutionAreaOffset,
                    buildingStates[i].Building.Function.PollutionArea));
            buildingStates[i].Building.Function.ApplyAreaEco(
                GetNatureCellsInArea(
                    buildingStates[i].CellPosition + buildingStates[i].Building.Function.PollutionAreaOffset,
                    buildingStates[i].Building.Function.PollutionArea));
            tickCommunityHealth += buildingStates[i].Building.Function.GetAppliedCommunityHealth(
                GetNatureCellsInArea(
                    buildingStates[i].CellPosition + buildingStates[i].Building.Function.PollutionAreaOffset,
                    buildingStates[i].Building.Function.PollutionArea));
        }

        // Apply Updates to Cells
        for (int i = 0; i < natureCellStates.Length; i++)
        {
            natureCellStates[i].ApplyCumulativeValues();
        }

        // Update core game values
        totalEnergy += tickEnergy;
        UpdateTotalPollutionValue(tickPollution);
        UpdateTotalEcoValue(tickEco);
        totalCommunityHealth += tickCommunityHealth;

        // Event Signaling
        OnWorldTickCompleted?.Invoke(this);

        // Debug
        DebugNatureCells();
    }

    private void GenerationTick()
    {
        Debug.Log("Begin generation tick.");
        for (int i = 0; i < countWorldTicksPerGeneration; i++)
        {
            WorldTick();
        }
    }

    #region Data Methods
    private void AddBuilding(Building building, Vector3Int position)
    {
        buildingStates.Add(new BuildingState(building, position));
    }

    private void CountCurrentDefaultTiles()
    {
        int counter = 0;
        for (int x = mainTileMap.cellBounds.min.x; x < mainTileMap.cellBounds.max.x; x++)
        {
            for (int y = mainTileMap.cellBounds.min.y; y < mainTileMap.cellBounds.max.y; y++)
            {
                for (int z = mainTileMap.cellBounds.min.z; z < mainTileMap.cellBounds.max.z; z++)
                {
                    TileBase tile = mainTileMap.GetTile(new Vector3Int(x, y, z));
                    if (tile == defaultTile)
                    {
                        counter++;
                    }
                }
            }
        }

        Debug.Log($"Count of Default Tiles: {counter}");
    }

    private NatureCellState[] GetNatureCellsInArea(Vector3Int initialPoint, BoundsInt area)
    {
        List<NatureCellState> foundCells = new List<NatureCellState> ();
        for (int x = initialPoint.x; x < initialPoint.x + area.max.x; x++)
        {
            for (int y = initialPoint.y; y < initialPoint.y + area.max.y; y++)
            {
                for(int z = initialPoint.z; z < initialPoint.z + area.max.z; z++)
                {
                    for (int i = 0; i < natureCellStates.Length; i++)
                    {
                        if (natureCellStates[i].CellPosition == new Vector3Int(x, y, z))
                        {
                            foundCells.Add(natureCellStates[i]);
                        }
                    }
                }
            }
        }

        return foundCells.ToArray();
    }

    private void UpdateTotalPollutionValue(int _tickPollution)
    {
        int tempPollution = _tickPollution;
        for (int i = 0; i < natureCellStates.Length; i++)
        {
            tempPollution += natureCellStates[i].PollutionLevel;
        }
        totalPollution = tempPollution;
    }

    private void UpdateTotalEcoValue(int _tickEco)
    {
        int tempEco = _tickEco;
        for (int i = 0; i < natureCellStates.Length; i++)
        {
            tempEco += natureCellStates[i].EcoLevel;
        }
        totalEco = tempEco;
    }
    #endregion

    #region Event Based
    private void SignalEndGameState()
    {
        OnEndWorldState?.Invoke(this);
    }
    #endregion

    #region Debug Methods
    private void DebugNatureCells()
    {
        string debugMessage = "Nature Cell World Data: \n";
        debugMessage += $"Total Nature Cells: {natureCellStates.Length}\n";

        for (int i = 0; i < natureCellStates.Length; i++)
        {
            debugMessage += $"Type: {natureCellStates[i].Cell} Position: {natureCellStates[i].CellPosition} Pollution: {natureCellStates[i].PollutionLevel} Eco: {natureCellStates[i].EcoLevel}\n";
        }

        debugMessage += "\n";
        debugMessage += $"Total Pollution: {totalPollution}\n";
        debugMessage += $"Total Eco: {totalEco}\n";
        debugMessage += $"Total Energy: {totalEnergy}\n";
        debugMessage += $"Total Community Health: {totalCommunityHealth}\n";

        Debug.Log(debugMessage);

        DebugVisualizePollution();
    }

    private void DebugVisualizePollution()
    {
        for (int i = 0; i < natureCellStates.Length; i++)
        {
            mainTileMap.SetColor(
                natureCellStates[i].CellPosition, 
                grassPollutionGradient.Evaluate(
                    (float)natureCellStates[i].PollutionLevel / 100.0f));
        }
    }
    #endregion
}
