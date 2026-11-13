using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float sensetivity;
    public CharacterAI character;

    private Camera cam;
    private Vector3 move;
    private Vector3 camStartPos;
    private float screenEdgeDistance;
    private int money;

    private List<HelperControl> helpers;

    private Task currentTask;
    private float waitUntil;

    void Awake()
    {
        cam = Camera.main;
        GameManager.manager.player = this;
        helpers = new List<HelperControl>();
        screenEdgeDistance = Vector3.Distance(GetScreenEdgePosInWorld(cam), GetCentreScreenPosInWorld(cam));
    }

    void Update()
    {
        //control
        ClickInteract();
        DragToMove();

        //work
        StartWorking();
        FinishWorking();
        CancelWorking();
    }

    private void DragToMove()
    {
        
        // clicking on an area and dragging moves the screen and character around
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
            move = Input.mousePosition;
            camStartPos = cam.transform.position;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //find difference
            float diffX = (Input.mousePosition.x - move.x) * sensetivity;
            float diffZ = (Input.mousePosition.y - move.y) * sensetivity;
            Vector3 camPos = cam.transform.position;

            cam.transform.position = new Vector3(
                camStartPos.x - diffX, 
                camPos.y, 
                camStartPos.z - diffZ);

            if (currentTask != null) return;

            character.MoveToTarget(GetCentreScreenPosInWorld(cam));
            // add boundaries
        }
    }

    private void ClickInteract()
    {
        //clicking on an object interacts with it
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {


            Task task;
            task = GetTaskAtMousePos(cam);
            if (task == null) return;

            else if (task.TaskType == TASK.LOST)
            {
                if (task.gameObject.GetComponent<VisitorControl>().State == VisitorControl.STATE.LOST)
                    task.gameObject.GetComponent<VisitorControl>().GetFollow(character);
            }

            else if (task.TaskType == TASK.FIX ||
                task.TaskType == TASK.TRASH)
            {
                if (helpers.Count > 0)
                {
                    helpers[0].SetTask(task);
                    helpers.RemoveAt(0);
                }
                else MoveToWork(task);
            }
        }
    }

    public void AddHelper(HelperControl helper)
    { 
        helpers.Add(helper);
        helper.SetFollowDistance(helpers.Count);
    }

    public void MoveToWork(Task task)
    {
        currentTask = task;
        character.MoveToTarget(task.transform.position);
    }

    public void StartWorking()
    {
        if (currentTask != null &&
            character.transform.position == currentTask.transform.position &&
            waitUntil + 1 <= Time.fixedTime) //magic number to control order of operations
        {
            waitUntil = Time.fixedTime + currentTask.WaitTime;
        }
    }

    private void FinishWorking()
    {
        if (currentTask != null &&
            character.transform.position == currentTask.transform.position &&
            waitUntil < Time.fixedTime)
        {
            character.FixUp(currentTask);

            currentTask = null;
            character.MoveToTarget(GetCentreScreenPosInWorld(cam));
        }
    }

    private void CancelWorking()
    {
        if (currentTask != null &&
            Vector3.Distance(character.transform.position, GetCentreScreenPosInWorld(cam)) > screenEdgeDistance)
        {
            currentTask = null;
            character.MoveToTarget(GetCentreScreenPosInWorld(cam));
        }
    }

    private void IncreaseMoney()
    { 
        
    }

    private void DecreaseMoney(int money)
    { 
        
    }

    private static Vector3 GetMousePosInWorld(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    private static Vector3 GetScreenEdgePosInWorld(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Vector3.zero);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private static Vector3 GetCentreScreenPosInWorld(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction * 500f, Color.red);
            return hit.point;
        }
        return Vector3.zero;
    }

    private static Task GetTaskAtMousePos(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.parent.gameObject.TryGetComponent<Task>(out Task task) == true)
            { 
                if (task.TaskType == TASK.TRASH ||
                    task.TaskType == TASK.FIX ||
                    task.TaskType == TASK.LOST)
                        return task;
            }
        }
        return null;
    }
}