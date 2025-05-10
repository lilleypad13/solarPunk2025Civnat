using TMPro;
using UnityEngine;

public class BuildingCountDisplayController : MonoBehaviour
{
    [SerializeField] private TMP_Text buildingCountText;

    private void OnEnable()
    {
        GameManager.OnGameStateUpdate += UpdateInfo;
        SetChoice.OnSelectionDone += UpdateInfo;
        GameManager.OnGameStartSetupComplete += UpdateInfo;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateUpdate -= UpdateInfo;
        SetChoice.OnSelectionDone -= UpdateInfo;
        GameManager.OnGameStartSetupComplete -= UpdateInfo;
    }

    private void UpdateInfo()
    {
        // Building Count Text
        int buildingsLeftCount = GameManager.Instance.ActiveSet.Data.CountBuildingsToPlace
            - GameManager.Instance.ActiveSet.BuildingsPlacedDuringSet.Count;
        buildingCountText.text = $"{buildingsLeftCount}";
    }
}
