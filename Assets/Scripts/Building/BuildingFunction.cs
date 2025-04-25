using UnityEngine;

public abstract class BuildingFunction : MonoBehaviour
{
    [SerializeField] private BoundsInt pollutionArea;
    public BoundsInt PollutionArea { get { return pollutionArea; } }

    // TODO
    // Most likely takes a parameter of an area of tiles
    public abstract void Pollute(NatureCellState[] natureCells);

    // TODO
    // May not need any parameters
    public abstract void Generate();
}
