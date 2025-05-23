using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacementManager : MonoBehaviour
{
    [Header("System")]
    [SerializeField] private GridBuildingSystem gridBuildingSystem;

    private bool isPlacing = false;
    private Building currentBuilding;
    private Vector3Int previousPosition;
    private BuildingData currentBuildingData;

    // Events
    public static event Action<BuildingData> OnBuildingPlaced;
    public static event Action OnBegingPlacingBuilding;
    public static event Action OnEndPlacingBuilding;

    private void OnEnable()
    {
        BuildButton.OnStartBuilding += BeginPlacingBuilding;
    }

    private void OnDisable()
    {
        BuildButton.OnStartBuilding -= BeginPlacingBuilding;
    }

    private void Update()
    {
        if (isPlacing)
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }

            if (!currentBuilding.IsPlaced)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = gridBuildingSystem.GetCellPosition(touchPosition);

                if (previousPosition != cellPosition)
                {
                    currentBuilding.transform.localPosition = gridBuildingSystem.GetOffsetPositionFromCell(cellPosition);
                    previousPosition = cellPosition;
                    gridBuildingSystem.OutlineBuildingArea(currentBuilding);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Place
                PlaceBuilding();
            }

            if (Input.GetMouseButtonDown(1))
            {
                // Cancel
                CancelBuilding();
            }
        }
    }

    public void BeginPlacingBuilding(BuildingData buildingData)
    {
        // Event Signaling
        OnBegingPlacingBuilding?.Invoke();
        GridBuildingSystem.Instance.EnableMainBuildingGrid();
        // Logic
        currentBuildingData = buildingData;
        currentBuilding = Instantiate(buildingData.BuildingPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity).GetComponent<Building>();
        gridBuildingSystem.OutlineBuildingArea(currentBuilding);
        isPlacing = true;
    }

    public void PlaceBuilding()
    {
        if (isPlacing)
        {
            if (currentBuilding != null)
            {
                if (currentBuilding.CanBePlaced())
                {
                    currentBuilding.Place();

                    // Event Signaling
                    OnBuildingPlaced?.Invoke(currentBuildingData);

                    EndPlacingBuilding();
                }
            }
        }
    }

    public void CancelBuilding()
    {
        gridBuildingSystem.ClearArea();
        Destroy(currentBuilding.gameObject);
        EndPlacingBuilding();
    }

    public void EndPlacingBuilding()
    {
        // Event signaling
        OnEndPlacingBuilding?.Invoke();
        GridBuildingSystem.Instance.DisableMainBuildingGrid();

        // Logic
        currentBuildingData = null;
        currentBuilding = null;
        isPlacing = false;
    }
}
