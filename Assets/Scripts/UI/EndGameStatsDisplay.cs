using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameStatsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject endGameStatsContainer;

    [Header("Text")]
    [SerializeField] private TMP_Text endGamePollutionText;
    [SerializeField] private TMP_Text endGameEcoText;
    [SerializeField] private TMP_Text endGameEnergyText;
    [SerializeField] private TMP_Text endGameCommunityHealthText;
    [Header("Sprites")]
    [SerializeField] private Image successSprite;
    [SerializeField] private Image failSprite;
    [Header("Sprite Containers")]
    [SerializeField] private VerticalLayoutGroup successSpriteContainer;
    //[SerializeField] private Sprite pollutionSprite;
    //[SerializeField] private Sprite ecoSprite;
    //[SerializeField] private Sprite energySprite;
    //[SerializeField] private Sprite communityHealthSprite;

    private void OnEnable()
    {
        WorldStateSystem.OnEndWorldState += Setup;
    }

    private void OnDisable()
    {
        WorldStateSystem.OnEndWorldState -= Setup;
    }

    private void Setup(WorldStateSystem worldStateSystem)
    {
        // Text
        endGamePollutionText.text = $"Pollution: {worldStateSystem.TotalPollution}";
        endGameEcoText.text = $"Eco: {worldStateSystem.TotalEco}";
        endGameEnergyText.text = $"Energy: {worldStateSystem.TotalEnergy}";
        endGameCommunityHealthText.text = $"Community: {worldStateSystem.TotalCommunityHealth}";

        // Sprites
        // Pollution
        if (GameManager.Instance.GoalData.IsAtPollutionGoal(worldStateSystem.TotalPollution))
        {
            Instantiate(successSprite, successSpriteContainer.gameObject.transform);
        }
        else
        {
            Instantiate(failSprite, successSpriteContainer.gameObject.transform);
        }
        // Eco
        if (GameManager.Instance.GoalData.IsAtEcoGoal(worldStateSystem.TotalEco))
        {
            Instantiate(successSprite, successSpriteContainer.gameObject.transform);
        }
        else
        {
            Instantiate(failSprite, successSpriteContainer.gameObject.transform);
        }
        // Energy
        if (GameManager.Instance.GoalData.IsAtEnergyGoal(worldStateSystem.TotalEnergy))
        {
            Instantiate(successSprite, successSpriteContainer.gameObject.transform);
        }
        else
        {
            Instantiate(failSprite, successSpriteContainer.gameObject.transform);
        }
        // Community Health
        if (GameManager.Instance.GoalData.IsAtCommunityHealthGoal(worldStateSystem.TotalCommunityHealth))
        {
            Instantiate(successSprite, successSpriteContainer.gameObject.transform);
        }
        else
        {
            Instantiate(failSprite, successSpriteContainer.gameObject.transform);
        }

        // Open it
        Open();
    }

    public void Open()
    {
        endGameStatsContainer.SetActive(true);
    }

    public void Close()
    {
        endGameStatsContainer.SetActive(false);
    }
}
