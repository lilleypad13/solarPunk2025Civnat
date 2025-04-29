using System.Numerics;
using UnityEngine;

public abstract class BuildingFunction : MonoBehaviour
{
    [SerializeField] private BoundsInt pollutionArea;
    public BoundsInt PollutionArea { get { return pollutionArea; } }
    [SerializeField] private Vector3Int pollutionAreaOffset;
    public Vector3Int PollutionAreaOffset {  get { return pollutionAreaOffset; } }

    //public BoundsInt EffectiveArea
    //{
    //    get
    //    {
    //        return new BoundsInt(
    //            pollutionArea.min + pollutionAreaOffset, 
    //            pollutionArea.size);
    //    }
    //}

    // TODO
    // Most likely takes a parameter of an area of tiles
    public abstract void Pollute(NatureCellState[] natureCells);

    // TODO
    // May not need any parameters
    public abstract void Generate();

    // DEBUG
    protected void OnDrawGizmosSelected()
    {
        //var offset = new Vector3(.5f, .5f, 0f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pollutionArea.center, pollutionArea.size);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(pollutionArea.min + PollutionAreaOffset, Vector3Int.one);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(pollutionArea.max + PollutionAreaOffset, Vector3Int.one);
    }
}
