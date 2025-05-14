using System;
using CustomAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace TaskSystem
{
    public class TaskComponent : MonoBehaviour
    {
        public Transform taskPositionTransform;
        public TaskObject taskObject;

        [ReadOnly] public float taskTimer;

        /*[ReadOnly]*/ public bool isTaskAvailable;
        [ReadOnly] public bool isTaskInProgress;
        
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

        public void StartTask()
        {
            isTaskInProgress = true;
            isTaskAvailable = false;
            taskTimer = 0f;
            onTaskStarted?.Invoke();
        }
        
        public void CancelTask()
        {
            isTaskInProgress = false;
            isTaskAvailable = true;
            onTaskCanceled?.Invoke();
        }
        
        public void CompleteTask()
        {
            isTaskInProgress = false;
            onTaskCompleted?.Invoke();
        }
    }
}