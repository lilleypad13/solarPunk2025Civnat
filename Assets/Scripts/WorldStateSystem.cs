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

    // Data
    private List<BuildingState> buildingStates = new List<BuildingState>();
    private NatureCellState[] natureCellStates;

    [Header("TESTING")]
    [SerializeField] private Gradient grassPollutionGradient;

    private void OnEnable()
    {
        Building.OnPlaced += AddBuilding;
    }

    private void OnDisable()
    {
        Building.OnPlaced -= AddBuilding;
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

        if (Input.GetKeyDown(KeyCode.D))
        {
            DebugNatureCells();
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
                }
            }
        }

        natureCellStates = cellStatesList.ToArray();

        DebugNatureCells();
    }

    private void WorldTick()
    {
        for (int i = 0; i < buildingStates.Count; i++)
        {
            buildingStates[i].Building.Function.Pollute(
                GetNatureCellsInArea(
                    buildingStates[i].CellPosition + buildingStates[i].Building.Function.PollutionAreaOffset,
                    buildingStates[i].Building.Function.PollutionArea));
        }

        DebugNatureCells();
    }

    private void AddBuilding(Building building, Vector3Int position)
    {
        buildingStates.Add(new BuildingState(building, position));
    }

    #region Data Methods
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
    #endregion

    #region Debug Methods
    private void DebugNatureCells()
    {
        string debugMessage = "Nature Cell World Data: \n";
        debugMessage += $"Total Nature Cells: {natureCellStates.Length}\n";

        for (int i = 0; i < natureCellStates.Length; i++)
        {
            debugMessage += $"Type: {natureCellStates[i].Cell} Position: {natureCellStates[i].CellPosition} Pollution: {natureCellStates[i].PollutionLevel}\n";
        }

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
