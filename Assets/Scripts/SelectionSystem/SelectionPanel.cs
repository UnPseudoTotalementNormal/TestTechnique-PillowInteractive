using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sailor;
using SelectionSystem;
using TaskSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionPanel : MonoBehaviour
{
    private RectTransform rectTransform;
    private List<RectTransform> layoutsRectTransform = new();
    [SerializeField] private SelectionManager selectionManager;
    
    [Header("References")]
    public TMP_Text sailorNameText;
    public TMP_Text sailorStateText;
    public TMP_Text sailorTirednessText;
    
    public TMP_Text taskNameText;
    public TMP_Text taskDescriptionText;
    public TMP_Text taskTirednessText;
    public TMP_Text taskAvailabilityText;

    [Header("Animation values")]
    public Vector3 hiddenPosition;
    public Vector3 shownPosition;
    public float animationDuration = 0.5f;
    public Ease animationEase = Ease.OutQuint;
    
    public bool isVisible;
    private TaskComponent hoveredTask;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        layoutsRectTransform = GetComponentsInChildren<LayoutGroup>(true).Select(_lg => _lg.GetComponent<RectTransform>()).ToList();
        Hide();
    }

    public void Update()
    {
        if (!selectionManager.currentlySelectedSailorAI)
        {
            if (isVisible)
            {
                Hide(); // Hide the UI if there is no selected sailor
            }
            return;
        }
        
        if (!isVisible)
        {
            Show(); // Show the UI if there is a selected sailor
        }

        hoveredTask = selectionManager.GetHoveredTask(); // Update the hovered task value
        UpdateTexts();
    }

    
    /// <summary>
    /// Update text fields with the currently selected sailor and hovered task information
    /// </summary>
    private void UpdateTexts()
    {
        SailorAI _selectedSailorAI = selectionManager.currentlySelectedSailorAI;
        if (_selectedSailorAI)
        {
            sailorNameText.text = _selectedSailorAI.sailorName;
            sailorStateText.text = _selectedSailorAI.currentState.ToString();
            sailorTirednessText.text = ((int)_selectedSailorAI.tiredness).ToString();
        }
        
        if (hoveredTask)
        {
            taskNameText.text = hoveredTask.taskObject.taskName;
            taskDescriptionText.text = hoveredTask.taskObject.taskDescription;
            int _tiringValue = (int)hoveredTask.taskObject.tiringValue;
            taskTirednessText.text = (_tiringValue > 0 ? "+" : "-") + _tiringValue;
            taskAvailabilityText.text = (hoveredTask.isTaskAvailable && !hoveredTask.isTaskTaken) ? "Oui" : "Non";
        }
        else
        {
            taskNameText.text = "Aucun";
            taskDescriptionText.text = "Aucun";
            taskTirednessText.text = "Aucun";
            taskAvailabilityText.text = "Non";
        }
        
        //force the layout to rebuild to display texts correctly
        foreach (var _rectTransform in layoutsRectTransform)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        }
    }

    private void Show()
    {
        transform.DOLocalMove(shownPosition, animationDuration).SetEase(animationEase);
        isVisible = true;
    }

    private void Hide()
    {
        transform.DOLocalMove(hiddenPosition, animationDuration).SetEase(animationEase);
        isVisible = false;
    }
}
