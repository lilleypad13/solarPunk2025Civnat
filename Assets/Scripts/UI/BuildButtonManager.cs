using System.Collections.Generic;
using UnityEngine;

public class BuildButtonManager : MonoBehaviour
{
    [SerializeField] private BuildButton buildButtonPrefab;

    private List<BuildButton> buildButtons = new List<BuildButton>();

    public void Initialize(BuildingSetData buildings)
    {
        buildButtons.Clear();
        buildButtons = new List<BuildButton>();
        for (int i = 0; i < buildings.Buildings.Length; i++)
        {
            BuildButton button = Instantiate(buildButtonPrefab, this.transform);
            button.Initialize(buildings.Buildings[i]);
            buildButtons.Add(button);
        }
    }

    public void Clear()
    {
        for (int i = 0;i < buildButtons.Count; i++)
        {
            Destroy(buildButtons[i]);
        }
        buildButtons.Clear();
    }
}
