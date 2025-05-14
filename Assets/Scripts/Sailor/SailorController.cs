using System;
using CustomAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace Sailor
{
    public class SailorController : MonoBehaviour
    {
        public SailorStates currentState { get; private set; } = SailorStates.Idle;

        [Header("References")] 
        [SerializeField] private SailorMovement sailorMovement;
        
        [Header("Values")]
        [ReadOnly] public float tiredness;
        public float tirednessThreshold = 100f;
        

        private void Update()
        {
            //Test//
            
            if (Input.GetMouseButtonDown(0))
            {
                Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out RaycastHit _hit))
                {
                    sailorMovement.SetDestination(_hit.point + Vector3.up * 0.5f);
                }
            }
            
            ////////
            
            
            switch (currentState)
            {
                case SailorStates.Idle:
                    IdleStateUpdate();
                    break;
                case SailorStates.OnTask:
                    OnTaskStateUpdate();
                    break;
                case SailorStates.Tired:
                    TiredStateUpdate();
                    break;
            }
        }

        private void IdleStateUpdate()
        {
            
        }

        private void OnTaskStateUpdate()
        {
            
        }

        private void TiredStateUpdate()
        {
            tiredness -= Time.deltaTime;
            if (tiredness > 0)
            {
                return;
            }
            
            SetState(SailorStates.Tired);
            tiredness = 0;
        }

        public void SetState(SailorStates _newState)
        {
            SailorStates _oldState = currentState;

            switch (_oldState)
            {
                case SailorStates.Idle:
                    break;
                case SailorStates.OnTask:
                    break;
                case SailorStates.Tired:
                    break;
            }
            
            currentState = _newState;
            switch (currentState)
            {
                case SailorStates.Idle:
                    break;
                case SailorStates.OnTask:
                    break;
                case SailorStates.Tired:
                    break;
            }
        }


    }
    
    public enum SailorStates
    {
        Idle,
        OnTask,
        Tired,
    }
}
