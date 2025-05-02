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

    private void UpdateDisplay(WorldStateSystem worldStateSystem)
    {
        pollutionText.text = $"Pollution: {worldStateSystem.TotalPollution}";
        ecoText.text = $"Eco: {worldStateSystem.TotalEco}";
        energyText.text = $"Energy: {worldStateSystem.TotalEnergy}";
        communityText.text = $"Community: {worldStateSystem.TotalCommunityHealth}";
    }
}
