using System;
using System.Collections.Generic;
using System.Linq;
using CustomAttributes;
using Extensions;
using TaskSystem;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [ReadOnly] public List<TaskComponent> taskComponents = new();
    
    public float taskSpawnMinInterval = 5f;
    public float taskSpawnMaxInterval = 15f;
    
    [SerializeField, ReadOnly] private float taskSpawnTimer;
    
    private void Start()
    {
        taskComponents = GetComponentsInChildren<TaskComponent>().ToList();
        taskSpawnTimer = UnityEngine.Random.Range(taskSpawnMinInterval, taskSpawnMaxInterval);
    }

    private void Update()
    {
        taskSpawnTimer -= Time.deltaTime;
        if (taskSpawnTimer <= 0)
        {
            SpawnTask();
            taskSpawnTimer = UnityEngine.Random.Range(taskSpawnMinInterval, taskSpawnMaxInterval);
        }
    }

    private void SpawnTask()
    {
        TaskComponent _randomTask = GetSpawnableTasks().PickRandom();
        if (!_randomTask)
        {
            return;
        }

        _randomTask.isTaskAvailable = true;
    }
    
    private List<TaskComponent> GetSpawnableTasks()
    {
        return taskComponents.Where(_task => !_task.isTaskTaken && !_task.isTaskAvailable).ToList();
    }
}
