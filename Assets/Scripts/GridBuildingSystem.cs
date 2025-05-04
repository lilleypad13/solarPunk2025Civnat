using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public enum TileType
{
    Empty, 
    White, 
    Green, 
    Red
}

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem Instance;

    [Header("Grids")]
    [SerializeField] private GridLayout gridLayout;
    public GridLayout GridLayout { get => gridLayout; }
    [SerializeField] private Tilemap mainTileMap;
    public Tilemap MainTileMap { get => mainTileMap; }
    [SerializeField] private Tilemap tempTileMap;
    public Tilemap TempTileMap { get => tempTileMap; }

    [Header("Tiles")]
    [SerializeField] private TileBase defaultTile;
    [SerializeField] private TileBase clearTile;
    [SerializeField] private TileBase blockedTile;

    [Header("Parameters")]
    [SerializeField] private Vector3 placementOffset = new Vector3 (0.0f, 0.0f, 0.0f);

    public static Dictionary<TileType, TileBase> TileBases = new Dictionary<TileType, TileBase>();

    private Building temp;
    private Vector3 previousPosition;
    private BoundsInt previousArea;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TileBases.Add(TileType.Empty, null);
        TileBases.Add(TileType.White, defaultTile);
        TileBases.Add(TileType.Green, clearTile);
        TileBases.Add(TileType.Red, blockedTile);
    }

    #region Tilemap Handling
    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (Vector3Int v in area.allPositionsWithin)
        {
            Vector3Int position = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(position);
            counter++;
        }

        return array;
    }

    public static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] tileArray, TileType type)
    {
        for (int i = 0; i < tileArray.Length; i++)
        {
            tileArray[i] = TileBases[type];
        }
    }

    public void DisableMainBuildingGrid()
    {
        mainTileMap.gameObject.SetActive(false);
    }

    public void EnableMainBuildingGrid()
    {
        mainTileMap.gameObject.SetActive(true);
    }
    #endregion

    public void InitializeWithBuilding(GameObject building)
    {
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        OutlineBuildingArea(temp);
    }

    public void ClearArea()
    {
        TileBase[] toClear = new TileBase[previousArea.size.x * previousArea.size.y * previousArea.size.z];
        FillTiles(toClear, TileType.Empty);
        tempTileMap.SetTilesBlock(previousArea, toClear);
    }

    public void OutlineBuildingArea(Building building)
    {
        ClearArea();

        building.area.position = gridLayout.WorldToCell(building.gameObject.transform.position);
        BoundsInt buildingArea = building.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTileMap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if(baseArray[i] == TileBases[TileType.White])
            {
                tileArray[i] = TileBases[TileType.Green];
            }
            else
            {
                FillTiles(tileArray, TileType.Red);
                break;
            }
        }

        tempTileMap.SetTilesBlock(buildingArea, tileArray);
        previousArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock (area, mainTileMap);
        foreach (TileBase tile in baseArray)
        {
            if(tile != TileBases[TileType.White])
            {
                Debug.Log("Cannot place here.");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, TempTileMap);
        SetTilesBlock(area, TileType.Green, mainTileMap);
    }

    #region Building Helpers
    public Vector3Int GetCellPosition(Vector2 screenPosition)
    {
        return gridLayout.LocalToCell(screenPosition);
    }

    public Vector3 GetOffsetPositionFromCell(Vector3Int cellPosition)
    {
        return gridLayout.CellToLocalInterpolated(cellPosition) + placementOffset;
    }
    #endregion
}
