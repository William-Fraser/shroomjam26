using UnityEngine;

public class HelperControl : CharacterAI
{
    //follows main character
    //used to fix/clean before main character is sent to

    //roster of helpers, probably a list
    //cycle follow distance so the closest one is the next used for tasks
    //something like using the ID of their list index to create the distance

    //state machine for AI, following, working, returning, 
    public enum STATE
    { 
        FOLLOW,
        TRAVEL,
        WORK,
        RETURN
    }

    private STATE state;
    private CharacterAI followingCharacter;
    private Task currentTask;

    private float waitUntil;

    void Start()
    {
        
        followingCharacter = GameManager.manager.player.character;
        GameManager.manager.player.AddHelper(this);
        state = STATE.FOLLOW;

        Debug.Log($"{followingCharacter}, {agent}");
    }

    void Update()
    {
        StateMachine();
    }

    private void StateMachine()
    {
        switch (state)
        { 
            case STATE.FOLLOW:
                Following();
                return;

            case STATE.TRAVEL:
                ReachDestination();
                return;

            case STATE.WORK:
                StartWorking();
                FinishWorking();
                return;

            case STATE.RETURN:
                Returning();
                return;

            default: Debug.LogError("ERR: HELPER STATE MACHINE BROKE :("); return;
        }
    }

    public void Following()
    {
        agent.SetDestination(followingCharacter.transform.position);
    }

    //travel to a task and work on it
    public void SetTask(Task task)
    {
        agent.stoppingDistance = 0;
        currentTask = task;
        state = STATE.TRAVEL;
        agent.SetDestination(task.transform.position);
    }

    private void ReachDestination()
    {
        if (transform.position.x == currentTask.transform.position.x &&
            transform.position.z == currentTask.transform.position.z)
        {
            state = STATE.WORK;
        }
    }

    public void StartWorking()
    {
        if (waitUntil + 1 <= Time.fixedTime) //magic number to control order of operations
        {
            waitUntil = Time.fixedTime + currentTask.WaitTime;
        }
    }

    private void FinishWorking()
    {
        if (waitUntil < Time.fixedTime)
        {
            FixUp(currentTask);

            currentTask = null;
            state = STATE.RETURN;
            agent.SetDestination(followingCharacter.transform.position);
        }
    }

    public void Return()
    {
        currentTask = null;
        state = STATE.RETURN;
        agent.SetDestination(followingCharacter.transform.position);
    }

    private void Returning()
    {
        agent.SetDestination(followingCharacter.transform.position);

        if (Vector3.Distance(transform.position, followingCharacter.transform.position) < 10)
        {
            Returned();
        }
    }

    private void Returned()
    {
        GameManager.manager.player.AddHelper(this);
        state = STATE.FOLLOW;
    }
}
