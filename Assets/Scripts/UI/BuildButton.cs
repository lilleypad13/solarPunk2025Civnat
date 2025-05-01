using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;

    [Header("Data")]
    [SerializeField] private Building buildingPrefab;
    public Building BuildingPrefab { get { return buildingPrefab; } }
    public BuildingData Data { get; private set; }


    public static event Action<BuildingData> OnStartBuilding;

    private void OnEnable()
    {
        button.onClick.AddListener(InvokeBuildingEvent);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(InvokeBuildingEvent);
    }

    public void Initialize(BuildingData data)
    {
        Data = data;
        buttonImage.sprite = data.UiSprite;
        buildingPrefab = data.BuildingPrefab;
    }

    public void DisableInteraction()
    {
        button.interactable = false;
    }

    public void Clear()
    {
        buttonImage.sprite = null;
        buildingPrefab = null;
    }

    private void InvokeBuildingEvent()
    {
        Debug.Log("Build Button Invoked Build Event!");
        OnStartBuilding?.Invoke(Data);
    }
}
