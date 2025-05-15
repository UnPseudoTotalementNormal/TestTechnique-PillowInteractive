using System;
using CustomAttributes;
using DG.Tweening;
using Sailor;
using TaskSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace SelectionSystem
{
    public class SelectionManager : MonoBehaviour
    {
        [ReadOnly] public SailorAI currentlySelectedSailorAI;

        private TaskComponent lastHoveredTask;
        
        public event Action<SailorAI, TaskComponent> onTaskAssigned;
        
        /// <summary>
        /// Try to select a sailor at the given mouse position
        /// </summary>
        public void TrySelectAt(Vector2 _clickPosition)
        {
            Ray _ray = Camera.main.ScreenPointToRay(_clickPosition);
            int _layer = LayerMask.GetMask("Sailor");
            if (Physics.Raycast(_ray, out RaycastHit _hit, Mathf.Infinity, _layer))
            {
                SailorAI _hitSailorAI = _hit.collider.GetComponentInParent<SailorAI>();
                if (_hitSailorAI)
                {
                    SelectSailor(_hitSailorAI);
                    return;
                }
            }
            
            if (currentlySelectedSailorAI)
            {
                UnSelectSailor();
            }
        }
        
        /// <summary>
        /// Try to assign a task to the currently selected sailor
        /// </summary>
        public void TryOrderAt(Vector2 _clickPosition)
        {
            if (currentlySelectedSailorAI == null)
            {
                return;
            }
            
            Ray _ray = Camera.main.ScreenPointToRay(_clickPosition);
            int _layer = LayerMask.GetMask("Task");
            if (Physics.Raycast(_ray, out RaycastHit _hit, Mathf.Infinity, _layer))
            {
                var _taskComponent = _hit.collider.GetComponentInParent<TaskComponent>();
                if (_taskComponent && currentlySelectedSailorAI.TryAssignTask(_taskComponent))
                {
                    onTaskAssigned?.Invoke(currentlySelectedSailorAI, _taskComponent);
                }
            }
        }

        private void Update()
        {
            if (!currentlySelectedSailorAI)
            {
                return;
            }
            
            TaskComponent _hoveredTask = GetHoveredTask();
            if (lastHoveredTask == _hoveredTask)
            {
                return;
            }
            
            if (lastHoveredTask)
            {
                HoverTaskVisuals(lastHoveredTask, false);
            }
            if (_hoveredTask)
            {
                HoverTaskVisuals(_hoveredTask, true);
            }
            lastHoveredTask = _hoveredTask;
        }

        /// <summary>
        /// Highlight the task visuals when hovered & unhighlight when not hovered
        /// </summary>
        private void HoverTaskVisuals(TaskComponent _taskComponent, bool _hover)
        {
            _taskComponent.taskModelTransform.DOScale(_hover ? 1.5f : 1f, 0.5f).SetEase(Ease.OutQuint);
        }
        
        private void UnSelectSailor()
        {
            currentlySelectedSailorAI = null;
            if (lastHoveredTask)
            {
                HoverTaskVisuals(lastHoveredTask, false);
                lastHoveredTask = null;
            }
        }

        private void SelectSailor(SailorAI _sailorAI)
        {
            if (currentlySelectedSailorAI)
            {
                UnSelectSailor();
            }
            currentlySelectedSailorAI = _sailorAI;
        }
        
        public TaskComponent GetHoveredTask()
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            TaskComponent _hitTask = null;
            int _layer = LayerMask.GetMask("Task");
            if (Physics.Raycast(_ray, out RaycastHit _hit, Mathf.Infinity, _layer))
            {
                _hitTask = _hit.collider.GetComponentInParent<TaskComponent>();
            }
        
            return _hitTask;
        }
    }
}