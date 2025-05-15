using System;
using CustomAttributes;
using Extensions;
using TaskSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Sailor
{
    public class SailorAI : MonoBehaviour
    {
        [field:SerializeField, ReadOnly] public SailorStates currentState { get; private set; } = SailorStates.Idle;

        [Header("References")] 
        [SerializeField] private SailorMovement sailorMovement;
        [ReadOnly] public TaskComponent currentTask;
        
        [Header("Values")]
        [ReadOnly] public string sailorName = "John Sailor";
        [ReadOnly] public float tiredness;
        public float tirednessThreshold = 100f;
        public float tirednessRecoverySpeed = 10f;
        public float normalSpeed = 5;
        public float idleSpeed = 1;
        public Vector3 homePosition;
        
        [SerializeField] private float idleRandomMoveTime; //time in idle before randomly moving
        private float idleRandomMoveTimer = 0;

        public event Action<SailorStates, SailorStates> onStateChanged; //Old state, new state

        private void Awake()
        {
            sailorName = SailorNames.names.PickRandom();
            SetState(SailorStates.Idle);
        }

        private void Update()
        {
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
            if (!sailorMovement.isAtDestination)
            {
                return;
            }
            
            idleRandomMoveTimer -= Time.deltaTime;
            if (idleRandomMoveTimer > 0)
            {
                return;
            }
            
            idleRandomMoveTimer = idleRandomMoveTime;
            sailorMovement.SetDestinationToRandomPoint();
        }

        private void OnTaskStateUpdate()
        {
            if (!sailorMovement.isAtDestination || currentTask.isTaskInProgress)
            {
                return;
            }
            
            currentTask.StartTask();
            currentTask.onTaskCompleted.AddListener(OnTaskCompleted);
        }

        private void TiredStateUpdate()
        {
            if (!sailorMovement.isAtDestination)
            {
                return;
            }
            
            tiredness -= tirednessRecoverySpeed * Time.deltaTime;
            if (tiredness > 0)
            {
                return;
            }
            
            SetState(SailorStates.Idle);
            tiredness = 0;
        }

        public void SetState(SailorStates _newState)
        {
            //handle old state exit
            SailorStates _oldState = currentState;
            switch (_oldState)
            {
                case SailorStates.Idle:
                    sailorMovement.SetSpeed(normalSpeed);
                    break;
                case SailorStates.OnTask:
                    currentTask.onTaskCompleted.RemoveListener(OnTaskCompleted);
                    currentTask = null;
                    break;
                case SailorStates.Tired:
                    break;
            }
            
            //handle new state enter
            currentState = _newState;
            switch (currentState)
            {
                case SailorStates.Idle:
                    sailorMovement.SetSpeed(idleSpeed);
                    break;
                case SailorStates.OnTask:
                    currentTask!.TakeTask();
                    sailorMovement.SetDestination(currentTask.taskPositionTransform.position);
                    break;
                case SailorStates.Tired:
                    sailorMovement.SetDestination(homePosition);
                    break;
            }
            
            onStateChanged?.Invoke(_oldState, _newState);
        }
        
        private void OnTaskCompleted()
        {
            tiredness = Mathf.Clamp(currentTask.taskObject.tiringValue + tiredness, 0, tirednessThreshold);
            
            if (tiredness >= tirednessThreshold)
            {
                SetState(SailorStates.Tired);
                return;
            }
            SetState(SailorStates.Idle);
        }
        
        public bool CanAcceptTask(TaskComponent _taskComponent)
        {
            return currentState == SailorStates.Idle && _taskComponent.isTaskAvailable;
        }
    
        public bool TryAssignTask(TaskComponent _taskComponent)
        {
            if (!CanAcceptTask(_taskComponent))
            {
                return false;
            }
            
            currentTask = _taskComponent;
            SetState(SailorStates.OnTask);
            return true;
        }
    }
    
    public enum SailorStates
    {
        Idle,
        OnTask,
        Tired,
    }
}
