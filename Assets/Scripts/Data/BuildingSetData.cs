using UnityEngine;

[CreateAssetMenu(fileName = "BuildingSetData", menuName = "Scriptable Objects/BuildingSetData")]
public class BuildingSetData : ScriptableObject
{
    [SerializeField] private int countBuildingsToPlace = 1;
    public int CountBuildingsToPlace { get => countBuildingsToPlace; }
    [SerializeField] private bool canDuplicatePlacements = true;
    public bool CanDuplicatePlacements { get => canDuplicatePlacements; }
    [SerializeField] private BuildingData[] buildings;
    public BuildingData[] Buildings {  get => buildings; }
    [Header("Choice UI Elements")]
    [SerializeField] private Sprite choiceUIIcon;
    public Sprite ChoiceUIIcon { get => choiceUIIcon; }
    [SerializeField] private string choiceText;
    public string ChoiceText { get => choiceText; }
}
