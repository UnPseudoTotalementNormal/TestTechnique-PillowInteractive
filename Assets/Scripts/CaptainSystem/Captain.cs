using System;
using DG.Tweening;
using Sailor;
using SelectionSystem;
using TaskSystem;
using TMPro;
using UnityEngine;

namespace CaptainSystem
{
    public class Captain : MonoBehaviour
    {
        [SerializeField] private SelectionManager selectionManager;
        
        [SerializeField] private TMP_Text orderTextPrefab;
        [SerializeField] private float orderTextDuration = 4f;

        private void Start()
        {
            selectionManager.onTaskAssigned += OnTaskAssigned;
        }

        private void OnTaskAssigned(SailorAI _sailor, TaskComponent _assignedTask)
        {
            GiveOrderVisualFeedback(_sailor, _assignedTask);
        }

        private void GiveOrderVisualFeedback(SailorAI _sailor, TaskComponent _assignedTask)
        {
            var _newOrderText = Instantiate(orderTextPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
            _newOrderText.text = $"{_sailor.sailorName}, {_assignedTask.taskObject.taskOrderPhrase}";
            var _textTransform = _newOrderText.transform;

            _newOrderText.GetComponent<CanvasGroup>().DOFade(0, orderTextDuration).SetEase(Ease.InQuint);
            _textTransform.DOMove(_textTransform.position + transform.forward * 2f + Vector3.up * 1f, orderTextDuration)
                .OnComplete(() =>
                {
                    Destroy(_newOrderText.gameObject);
                });
        }
    }
}
