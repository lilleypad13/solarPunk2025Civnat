using UnityEngine;

// Just wanted a simple way to display an array of arrays in the Inspector.
[System.Serializable]
public class ProgressionSet
{
    [SerializeField] private BuildingSetData[] buildingSets;
    public BuildingSetData[] BuildingSets { get => buildingSets; }
}

[CreateAssetMenu(fileName = "GameProgressionData", menuName = "Scriptable Objects/GameProgressionData")]
public class GameProgressionData : ScriptableObject
{
    [SerializeField] private ProgressionSet[] phaseSets;
    public ProgressionSet[] PhaseSets { get => phaseSets; }
}
