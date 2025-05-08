using TMPro;
using UnityEngine;

public class GameStateDisplayController : MonoBehaviour
{
    [SerializeField] private TMP_Text buildingsLeftInSetCount;
    [SerializeField] private TMP_Text generationInfoText;
    [SerializeField] private TMP_Text turnInfoText;

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
        buildingsLeftInSetCount.text = $"Buildings to Place this Set: {buildingsLeftCount}";

        // Generation Text
        generationInfoText.text = $"Generation: {GameManager.Instance.GenerationIndex + 1} / {GameManager.Instance.ProgressionDatas.Length}";

        // Turn Text
        if(GameManager.Instance.CurrentGeneration != null)
        {
            turnInfoText.text = $"Set: {GameManager.Instance.PhaseIndex + 1} / {GameManager.Instance.CurrentGeneration.PhaseSets.Length}";
        }
        else
        {
            turnInfoText.text = "DONE";
        }
    }
}
