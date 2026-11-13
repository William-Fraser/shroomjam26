using UnityEngine;
using System.Collections.Generic;
using System;

public class TaskHandler : MonoBehaviour
{
    //save to a list of tasks, maybe a dictionary

    //position lists that hold info for what to do
    //list of attractions /
    //list of jobs / fixing attractions, cleaning mess, helping lost visitors

    public Dictionary<Vector3, Task> attractions; // gets added to a over time, doesn't reduce
    public Vector3[] attractionKeys;
    public List<Task> jobs; // gets added to and removed from, living, todo
    public Task visitorCentre;

    //this should have calls to get info instead of being public like originally planned, keep this due to time contraint and fix later (hopefull)

    void Awake()
    {
        attractions = new Dictionary<Vector3, Task>();
        jobs = new List<Task>();
    }

    public void AddTask(Vector3 pos, Task task)
    {
        if (task.TaskType == TASK.ATTRACTION ||
            task.TaskType == TASK.ATTRACTIONRIDE)
        {
            attractions.Add(pos, task);
            //Debug.Log($"task {task} added at {pos}, #{attractions.Count} attraction");

            attractionKeys = new Vector3[attractions.Count];
            attractions.Keys.CopyTo(attractionKeys, 0);

            GameManager.manager.multM.IncreaseMeterMax(1); // increase max by 1 for every attraction
        }
        else if (task.TaskType == TASK.TRASH ||
            task.TaskType == TASK.LOST ||
            task.TaskType == TASK.FIX)
        {
            jobs.Add(task);

            Debug.Log($"task {task} added at {pos}, #{jobs.Count} job");
        }
        else if (task.TaskType == TASK.VCENTRE)
        {
            visitorCentre = task;
        }
    }

    public void RemoveTask(Vector3 pos, Task task)
    {
        //called on prefab removal
        if (attractions.ContainsKey(pos))
        {
            attractions.Remove(pos);
            attractionKeys = new Vector3[attractions.Count];
            attractions.Keys.CopyTo(attractionKeys, 0);
        }
        else if (jobs.Contains(task))
            jobs.Remove(task);
        else if (visitorCentre != null) { Debug.Log("VisitorCenter exists"); }
        else { Debug.LogError("ERR: TASK NOT LISTED"); }
    }
}
