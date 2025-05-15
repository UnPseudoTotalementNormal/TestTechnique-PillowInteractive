using System;
using CustomAttributes;
using Sailor;
using TaskSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace SelectionSystem
{
    public class SelectionManager : MonoBehaviour
    {
        [ReadOnly] public SailorAI currentlySelectedSailorAI;

        public void TrySelectAt(Vector2 _clickPosition)
        {
            Ray _ray = Camera.main.ScreenPointToRay(_clickPosition);
            if (Physics.Raycast(_ray, out RaycastHit _hit))
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
        
        public void TryOrderAt(Vector2 _clickPosition)
        {
            if (currentlySelectedSailorAI == null)
            {
                return;
            }
            
            Ray _ray = Camera.main.ScreenPointToRay(_clickPosition);
            if (Physics.Raycast(_ray, out RaycastHit _hit))
            {
                var _taskComponent = _hit.collider.GetComponentInParent<TaskComponent>();
                if (_taskComponent)
                {
                    currentlySelectedSailorAI.TryAssignTask(_taskComponent);
                }
            }
        }
        
        private void UnSelectSailor()
        {
            currentlySelectedSailorAI = null;
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
            if (Physics.Raycast(_ray, out RaycastHit _hit))
            {
                _hitTask = _hit.collider.GetComponentInParent<TaskComponent>();
            }
        
            return _hitTask;
        }
    }
}