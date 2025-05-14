using UnityEngine;

namespace TaskSystem
{
    [CreateAssetMenu(fileName = "NewTask", menuName = "TaskSystem/TaskObject")]
    public class TaskObject : ScriptableObject
    {
        public string taskName;
        public string taskDescription;
        public float taskDuration;
    }
}