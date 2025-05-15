using System;
using CustomAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace TaskSystem
{
    public class TaskComponent : MonoBehaviour
    {
        public Transform taskPositionTransform;
        public Transform taskModelTransform;
        public TaskObject taskObject;
        

        [ReadOnly] public float taskTimer;

        [ReadOnly] public bool isTaskAvailable;
        [ReadOnly] public bool isTaskInProgress;
        [ReadOnly] public bool isTaskTaken;
        
        public UnityEvent onTaskStarted = new();
        public UnityEvent onTaskCanceled = new();
        public UnityEvent onTaskCompleted = new();

        private void Awake()
        {
            if (!taskPositionTransform)
            {
                taskPositionTransform = transform;
            }
        }

        private void Update()
        {
            if (!isTaskInProgress)
            {
                return;
            }
            
            taskTimer += Time.deltaTime;
            if (taskTimer >= taskObject.taskDuration)
            {
                CompleteTask();
            }
        }
        
        public void TakeTask()
        {
            isTaskTaken = true;
            isTaskAvailable = false;
        }

        public void StartTask()
        {
            isTaskAvailable = false;
            isTaskInProgress = true;
            taskTimer = 0f;
            onTaskStarted?.Invoke();
        }
        
        public void CancelTask()
        {
            isTaskInProgress = false;
            isTaskTaken = false;
            isTaskAvailable = true;
            onTaskCanceled?.Invoke();
        }
        
        public void CompleteTask()
        {
            isTaskInProgress = false;
            isTaskTaken = false;
            onTaskCompleted?.Invoke();
        }
    }
}