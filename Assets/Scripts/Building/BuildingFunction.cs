using System.Numerics;
using UnityEngine;

public abstract class BuildingFunction : MonoBehaviour
{
    [Header("Dimensions")]
    [SerializeField] private BoundsInt pollutionArea;
    public BoundsInt PollutionArea { get { return pollutionArea; } }
    [SerializeField] private Vector3Int pollutionAreaOffset;
    public Vector3Int PollutionAreaOffset {  get { return pollutionAreaOffset; } }

    [Header("Parameters")]
    [SerializeField] protected int pollutionAreaValue = 0;
    public int PollutionAreaValue { get { return pollutionAreaValue; } }
    [SerializeField] protected int inherentPollutionValue = 0;
    public int InherentPollutionValue { get {return inherentPollutionValue; } }
    [SerializeField] protected int ecoAreaValue = 0;
    public int EcoAreaValue { get { return ecoAreaValue; } }
    [SerializeField] protected int inherentEcoValue = 0;
    public int InherentEcoValue { get { return inherentEcoValue; } }
    [SerializeField] protected int inherentEnergyValue = 0;
    public int InherentEnergyValue { get { return inherentEnergyValue; } }
    [SerializeField] protected int inherentCommunityHealthValue = 0;
    public int InherentCommunityHealthValue { get { return inherentCommunityHealthValue; } }
    [SerializeField] protected int pollutedInherentCommunityHealthValue = 0;
    public int PollutedInherentCommunityHealthValue { get { return pollutedInherentCommunityHealthValue; } }

    // TODO
    // Most likely takes a parameter of an area of tiles
    public abstract void ApplyAreaPollution(NatureCellState[] natureCells);
    public abstract void ApplyAreaEco(NatureCellState[] natureCells);
    public abstract int GetAppliedCommunityHealth(NatureCellState[] natureCells);

    // TODO
    // May not need any parameters
    public abstract void Generate();
}
