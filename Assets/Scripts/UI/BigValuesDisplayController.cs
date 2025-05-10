using UnityEngine;
using TMPro;

public class BigValuesDisplayController : MonoBehaviour
{
    [SerializeField] private TMP_Text pollutionText;
    [SerializeField] private TMP_Text ecoText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text communityText;

    private void OnEnable()
    {
        WorldStateSystem.OnWorldTickCompleted += UpdateDisplay;
    }

    private void OnDisable()
    {
        WorldStateSystem.OnWorldTickCompleted -= UpdateDisplay;
    }

    private void Start()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        pollutionText.text = $" 0";
        ecoText.text = $" 0";
        energyText.text = $" 0";
        communityText.text = $" 0";
    }

    private void UpdateDisplay(WorldStateSystem worldStateSystem)
    {
        pollutionText.text = $" {worldStateSystem.TotalPollution}";
        ecoText.text = $" {worldStateSystem.TotalEco}";
        energyText.text = $" {worldStateSystem.TotalEnergy}";
        communityText.text = $" {worldStateSystem.TotalCommunityHealth}";
    }
}
