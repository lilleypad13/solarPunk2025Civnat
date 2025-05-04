using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetChoice : MonoBehaviour
{
    [SerializeField] private Button choiceButton;
    [SerializeField] private Image buttonIcon;
    [SerializeField] private TMP_Text choiceText;

    private BuildingSetData buildingSetData;

    public static event Action<BuildingSetData> OnSelectedSet;
    public static event Action OnSelectionDone;

    public void Initialize(BuildingSetData setData)
    {
        buildingSetData = setData;
        buttonIcon.sprite = setData.ChoiceUIIcon;
        choiceText.text = setData.ChoiceText;

        choiceButton.onClick.AddListener(SignalButtonClickEvent);
    }

    private void SignalButtonClickEvent()
    {
        Debug.Log($"Choice Selection with Set: {buildingSetData.name}");
        OnSelectedSet?.Invoke(buildingSetData);
        OnSelectionDone?.Invoke();
    }
}
