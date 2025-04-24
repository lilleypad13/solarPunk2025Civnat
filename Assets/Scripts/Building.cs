using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool IsPlaced { get; private set; }
    public BoundsInt area;

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.Instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        IsPlaced = true;
        GridBuildingSystem.Instance.TakeArea(areaTemp);
    }

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.Instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuildingSystem.Instance.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }
}
