using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private Building buildingPrefab;
    public Building BuildingPrefab { get => buildingPrefab; }
    [SerializeField] private Sprite uiSprite;
    public Sprite UiSprite { get => uiSprite; }
}
