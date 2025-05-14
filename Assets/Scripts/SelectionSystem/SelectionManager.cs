using System;
using CustomAttributes;
using Sailor;
using TaskSystem;
using UnityEngine;

namespace SelectionSystem
{
    public class SelectionManager : MonoBehaviour
    {
        [ReadOnly] public SailorController currentlySelectedSailor;

        public void TrySelectAt(Vector2 _clickPosition)
        {
            Ray _ray = Camera.main.ScreenPointToRay(_clickPosition);
            if (Physics.Raycast(_ray, out RaycastHit _hit))
            {
                SailorController _hitSailor = _hit.collider.GetComponentInParent<SailorController>();
                if (_hitSailor)
                {
                    SelectSailor(_hitSailor);
                    return;
                }
            }
            
            if (currentlySelectedSailor)
            {
                UnSelectSailor();
            }
        }
        
        public void TryOrderAt(Vector2 _clickPosition)
        {
            if (currentlySelectedSailor == null)
            {
                return;
            }
            
            Ray _ray = Camera.main.ScreenPointToRay(_clickPosition);
            if (Physics.Raycast(_ray, out RaycastHit _hit))
            {
                var _taskComponent = _hit.collider.GetComponentInParent<TaskComponent>();
                if (_taskComponent)
                {
                    currentlySelectedSailor.TryAssignTask(_taskComponent);
                }
            }
        }
        
        private void UnSelectSailor()
        {
            currentlySelectedSailor = null;
        }

        private void SelectSailor(SailorController _sailor)
        {
            if (currentlySelectedSailor)
            {
                UnSelectSailor();
            }
            currentlySelectedSailor = _sailor;
        }
    }
}