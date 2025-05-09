using TMPro;
using UnityEngine;

public class BuildingInfoDisplayController : MonoBehaviour
{
    [SerializeField] private GameObject panelContainer;

    [Header("Text Elements")]
    [SerializeField] private TMP_Text areaOfInfluence;
    [SerializeField] private TMP_Text pollutionPerTile;
    [SerializeField] private TMP_Text inherentPollution;
    [SerializeField] private TMP_Text ecoPerTile;
    [SerializeField] private TMP_Text inherentEco;
    [SerializeField] private TMP_Text inherentEnergy;
    [SerializeField] private TMP_Text inherentCommunityHealth;
    [SerializeField] private TMP_Text inherentCommunityHealthPolluted;

    [Header("Descriptive Strings")]
    [SerializeField] private string areaOfInfluenceDescription = "Area of Influence: ";
    [SerializeField] private string pollutionPerTileDescription = "Pollution Per Tile: ";
    [SerializeField] private string inherentPollutionDescription = "Inherent Pollution: ";
    [SerializeField] private string ecoPerTileDescription = "Eco Per Tile: ";
    [SerializeField] private string inherentEcoDescription = "Inherent Eco: ";
    [SerializeField] private string inherentEnergyDescription = "Inherent Energy Generation: ";
    [SerializeField] private string inherentCommunityHealthDescription= "Flat Community Health: ";
    [SerializeField] private string inherentCommunityHealthPollutedDescription = "Flat Community Health (Polluted): ";

    private void OnEnable()
    {
        BuildButton.OnStartBuilding += Open;
        BuildingPlacementManager.OnEndPlacingBuilding += Close;
    }

    private void OnDisable()
    {
        BuildButton.OnStartBuilding -= Open;
        BuildingPlacementManager.OnEndPlacingBuilding -= Close;
    }

    private void Open(BuildingData data)
    {
        BuildingFunction function = data.BuildingPrefab.Function;
        areaOfInfluence.text = areaOfInfluenceDescription + function.PollutionArea.xMax + " X " + function.PollutionArea.yMax;
        pollutionPerTile.text = pollutionPerTileDescription + function.PollutionAreaValue.ToString();
        inherentPollution.text = inherentPollutionDescription + function.InherentPollutionValue.ToString();
        ecoPerTile.text = ecoPerTileDescription + function.EcoAreaValue.ToString();
        inherentEco.text = inherentEcoDescription + function.InherentEcoValue.ToString();
        inherentEnergy.text = inherentEnergyDescription + function.InherentEnergyValue.ToString();
        inherentCommunityHealth.text = inherentCommunityHealthDescription + function.InherentCommunityHealthValue.ToString();
        inherentCommunityHealthPolluted.text = inherentCommunityHealthPollutedDescription + function.PollutedInherentCommunityHealthValue.ToString();

        panelContainer.SetActive(true);
    }

    private void Close()
    {
        panelContainer.SetActive(false);
    }
}
