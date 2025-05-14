using System;
using UnityEngine;

namespace Sailor
{
    public class SailorController : MonoBehaviour
    {
        public SailorStates currentState { get; private set; } = SailorStates.Idle;

        private void Update()
        {
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
