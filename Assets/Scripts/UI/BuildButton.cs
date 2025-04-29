using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Building buildingPrefab;
    public Building BuildingPrefab { get { return buildingPrefab; } }

    public event Action<Building> OnStartBuilding;

    private void OnEnable()
    {
        button.onClick.AddListener(InvokeBuildingEvent);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(InvokeBuildingEvent);
    }

    private void InvokeBuildingEvent()
    {
        Debug.Log("Build Button Invoked Build Event!");
        OnStartBuilding?.Invoke(buildingPrefab);
    }
}
