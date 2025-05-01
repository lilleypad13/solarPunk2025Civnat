using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private BuildButtonManager buildButtonManager;

    [Header("Data")]
    [SerializeField] private BuildingSetData baseBuildingSet;

    private void Start()
    {
        UpdateBuildButtons();
    }

    public void UpdateBuildButtons()
    {
        buildButtonManager.Initialize(baseBuildingSet);
    }

    #region Debuggin

    #endregion
}
