using UnityEngine;
using UnityEngine.UI;

public class VisitorControl : CharacterAI
{
    public enum STATE
    {
        WALK,
        VISIT,
        LITTER,
        LOST
    }

    [SerializeField]
    private STATE state;
    public STATE State { get { return state; } }

    [Space(20)]
    private int currentDestination;
    public GameObject Visual;
    private float waitUntil;
    public float currentGameTime;

    [Space(20)]
    [Range(1, 100)]
    public int trashRate;
    public int dropTrashTime;
    public bool dropTrash = true;
    public GameObject TrashPrefab;

    [Space(20)]
    [Range(1, 100)]
    public int lostRate;
    public int getLostTime;
    public bool getLost = true;
    public GameObject lostNotifier;
    public bool followBool;
    public CharacterAI followingCharacter;

    private int maxTime = 111;
    private int rangeEnds = 40;

    [Space(20)]
    [SerializeField]
    private Task currentTask;
    private TaskHandler taskHandl;

    void Start()
    {
        taskHandl = GameManager.manager.taskM;

        state = STATE.WALK;
        currentDestination = -1;
        SetDestination();
        SetCurrentTask();

        dropTrashTime = (maxTime - Random.Range(trashRate - rangeEnds, trashRate + rangeEnds));// magic number : max trashrate is 100 so they'll drop every 3 seconds otherwise
        getLostTime = (maxTime - Random.Range(lostRate - rangeEnds, lostRate + rangeEnds));// magic number : max trashrate is 100 so they'll drop every 3 seconds otherwise
        lostNotifier.SetActive(false);
        followBool = false;

        UpgradeManager.LowerT += LowerTragedy;
    }

    void Update()
    {
        currentGameTime = Time.fixedTime;
        StateMachine();
    }

    private void StateMachine()
    {
        switch (state)
        {
            case STATE.WALK:
                //Debug.Log($"current pos:{transform.position} target:{tHandl.attractionKeys[currentDestination]}");
                lostNotifier.SetActive(false);
                StartWaiting();
                FinishWaiting();
                ControlVisual();
                SetCurrentTask();
                ReachDestination();
                RandomlyGetLost();
                RandomlyDropTrash();
                return;

            case STATE.VISIT:
                ReachBrokenRide();
                //sometimes turning invisible to simulate using the attraction
                ControlVisual();
                //upon arriving at a place of interest they stand there for a given amount of time
                StartWaiting();
                FinishWaiting();
                //this increases the happiness meter
                currentTask.IncreaseHappiness(currentTask.HappinessValue);
                return;

            case STATE.LITTER:
                //visitor stands still for a bit and then produces litter,
                //this is a random case? only enter while walking
                //litter starts a low happiness loss
                StartWaiting();
                CreateTrash();
                FinishWaiting();
                return;

            case STATE.LOST:
                //visitor starts crying? and will say lost they then become an actionable task
                //lost visitors start happiness loss
                //returning them to the enterance or visitor centre stops the happiness loss
                currentTask.DecreaseHappiness(currentTask.SadnessValue);
                Following();

                return;

            default: Debug.LogError("ERR: VISITOR STATE MACHINE BROKE :("); return;
        }
    }

    private void SetCurrentTask()
    {
        currentTask = taskHandl.attractions[taskHandl.attractionKeys[currentDestination]];
    }

    private void SetDestination()
    {
        currentDestination++;

        currentDestination = ChooseNewDestination(currentDestination, 0);

        agent.SetDestination(taskHandl.attractionKeys[currentDestination]);

       // Debug.Log($"destination set {currentDestination} {state}");
    }

    private int ChooseNewDestination(int current, int breaker)
    {
        int newDestination = Random.Range(0, taskHandl.attractionKeys.Length);
        int recursionBreaker = breaker;

        recursionBreaker++;

        if (newDestination != current)
            return newDestination;
        else if (recursionBreaker == taskHandl.attractionKeys.Length)
            return 0;
        else
            return ChooseNewDestination(current, recursionBreaker);
    }

    private void ReachDestination()
    {
        if (transform.position.x == currentTask.transform.position.x &&
            transform.position.z == currentTask.transform.position.z)
        {
            state = STATE.VISIT;
        }
    }

    private void ReachBrokenRide()
    {
        if (currentTask.Broken)
        {
            Debug.Log($"its broke");
            currentTask.DecreaseHappiness(currentTask.SadnessValue);
            agent.isStopped = false;
            state = STATE.WALK;
            SetDestination();
            SetCurrentTask();
        }
    }

    private void StartWaiting()
    {
        if (waitUntil+1 <= Time.fixedTime) //magic number to control order of operations
        {
            waitUntil = Time.fixedTime + currentTask.WaitTime;
            //Debug.Log($"{currentTask.name},{waitUntil}, {currentTask.WaitTime}, {Time.fixedTime}");
        }    
    }
    
    private void FinishWaiting()
    {
        if (waitUntil < Time.fixedTime)

        {
            agent.isStopped = false;
            state = STATE.WALK;
            SetDestination();
            SetCurrentTask();
        }
    }

    private void ControlVisual()
    {
        if (currentTask.TaskType == TASK.ATTRACTIONRIDE &&
            waitUntil > Time.fixedTime &&
            state == STATE.VISIT &&
            Visual.activeInHierarchy == true)
                Visual.SetActive(false);

        if (waitUntil <= Time.fixedTime &&
            Visual.activeInHierarchy == false)
                Visual.SetActive(true);
    }
     
    private void RandomlyDropTrash()
    {
        if (!dropTrash || dropTrashTime > Time.fixedTime) return;

        dropTrashTime = (int)Time.fixedTime + (maxTime - Random.Range(trashRate-rangeEnds, trashRate+rangeEnds));// magic number : max trashrate is 100 so they'll drop every 3 seconds otherwise
        agent.isStopped = true;
        state = STATE.LITTER;
        currentTask = Instantiate(TrashPrefab, transform.position, TrashPrefab.transform.rotation).GetComponent<Task>();
        currentTask.gameObject.SetActive(false);
    }

    private void CreateTrash()
    {
        if (waitUntil <= Time.fixedTime)
        {
            Debug.Log("dropping trash");
            currentTask.gameObject.SetActive(true);
        }
    }

    private void RandomlyGetLost()
    {
        if (!getLost || getLostTime > Time.fixedTime) return;

        Debug.Log("Lost Start");
        getLostTime = (int)Time.fixedTime + (maxTime - Random.Range(lostRate - rangeEnds, lostRate + rangeEnds));// magic number : max trashrate is 100 so they'll drop every 3 seconds otherwise
        agent.isStopped = true;
        state = STATE.LOST;
        currentTask = taskHandl.visitorCentre;
        lostNotifier.SetActive(true);
    }

    public void GetFollow(CharacterAI player)
    {
        followBool = true;
        followingCharacter = player;
        agent.isStopped = false;
        agent.stoppingDistance = 2;
    }

    public void Following()
    {
        if (followBool)
        { 
            agent.SetDestination(followingCharacter.transform.position);
        }
    }

    public void GetFound(Vector3 pos)//get found?
    {
        followBool = false;
        agent.SetDestination(pos);
        agent.stoppingDistance = 0;
        lostNotifier.SetActive(false);
        state = STATE.WALK;
        SetDestination();
    }

    public void LowerTragedy()
    {
        trashRate = trashRate - 20;
        lostRate = lostRate - 20;
        UpgradeManager.LowerT -= LowerTragedy;
    }

    //UI / Outline
    public void SelectUI()
    {
        //if (state == STATE.LOST)
            outline.enabled = true;
    }

    public void DeselectUI()
    {
        //if (state == STATE.LOST)
            outline.enabled = false;
    }

}
