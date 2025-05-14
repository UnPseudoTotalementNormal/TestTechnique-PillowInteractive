using System;
using CustomAttributes;
using UnityEngine;

namespace TaskSystem
{
    public class TaskComponent : MonoBehaviour
    {
        public TaskObject taskObject;

        [ReadOnly] public float taskTimer;

        public bool isTaskAvailable;
        public bool isTaskInProgress;

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
        }
        
        public void CancelTask()
        {
            isTaskInProgress = false;
            isTaskAvailable = true;
        }
        
        public void CompleteTask()
        {
            isTaskInProgress = false;
        }
    }
}