using System;
using System.Collections.Generic;
using UnityEngine;

public class NextSetChoiceController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject nextSetChoiceContainer;
    [SerializeField] private Transform choiceContainerParent;

    [Header("Data")]
    [SerializeField] private SetChoice choiceContainerPrefab;

    private List<SetChoice> activeChoices = new List<SetChoice>();

    // Spawn a choiceContainerPrefab for each choice option into the choiceContainerParent.
    // Destroy them all when done with the screen.

    private void OnEnable()
    {
        GameManager.OnChoicesCrossroad += Open;
        SetChoice.OnSelectionDone += Close;
    }

    private void OnDisable()
    {
        GameManager.OnChoicesCrossroad -= Open;
        SetChoice.OnSelectionDone -= Close;
    }

    public void Open(BuildingSetData[] buildingSets)
    {
        // Enable UI
        nextSetChoiceContainer.SetActive(true);

        // Spawn Elements
        activeChoices.Clear();
        activeChoices = new List<SetChoice>();
        for (int i = 0; i < buildingSets.Length; i++)
        {
            SetChoice temp = Instantiate(choiceContainerPrefab, choiceContainerParent);
            temp.Initialize(buildingSets[i]);
            activeChoices.Add(temp);
        }
    }

    public void Close()
    {
        // Disable UI
        nextSetChoiceContainer.SetActive(false);

        // Handle Objects
        Clear();
    }

    public void Clear()
    {
        if(activeChoices != null)
        {
            foreach (SetChoice choice in activeChoices)
            {
                Destroy(choice.gameObject);
            }
            activeChoices.Clear();
        }
    }
}
