using UnityEngine;

public enum TASK
{
    VCENTRE,
    ATTRACTION,
    ATTRACTIONRIDE,
    TRASH,
    FIX,
    LOST
}

public class Task : MonoBehaviour
{
    [SerializeField]
    private TASK task;
    public TASK TaskType { get { return task; } }
    [SerializeField]
    private float waitTime;
    public float WaitTime { get { return waitTime; } }

    [Header("Attraction")]
    private TASK OGtype;
    [SerializeField]
    private float happinessValue;
    [SerializeField]
    private GameObject brokenVisual;
    [SerializeField]
    private bool brakable = true;
    [SerializeField]
    [Range(1,100)]
    private float brakeDownRate;
    private float brakingIn;
    private bool broken;
    
    public float HappinessValue { get { return happinessValue; } }
    public GameObject BrokenVisual { get { return brokenVisual; } }
    public bool Broken { get { return broken; } }

    [Header("Job")]
    [SerializeField]
    private float sadnessValue;
    public float SadnessValue { get { return sadnessValue; } }

    [SerializeField]
    [Header("change pos on visitors to X:999, Y:999, Z:999")]
    private Vector3 pos = new Vector3(0,0,0);

    private int maxTime = 111;
    private int rangeEnds = 10;

    // task with switch and methods for each task
    void Start()
    {
        if (pos == Vector3.zero)
            pos = transform.position;

        GameManager.manager.taskM.AddTask(pos, this);

        OGtype = task;
        broken = false;
        brakingIn = (int)Time.fixedTime + (maxTime - Random.Range(brakeDownRate - rangeEnds, brakeDownRate + rangeEnds));
        
        if (task == TASK.ATTRACTION || task == TASK.ATTRACTIONRIDE)
            brokenVisual.SetActive(false);
    }
     
    void Update()
    {
        switch (task)
        { 
            case TASK.VCENTRE:
                //handled by VisitorControl & playerController
                return;

            case TASK.ATTRACTION:
                StartTimeTillBreak();
                BrakeDown();
                return;
            
            case TASK.ATTRACTIONRIDE:
                StartTimeTillBreak();
                BrakeDown();
                return;

            case TASK.TRASH:
                DecreaseHappiness(sadnessValue);
                return;

            case TASK.LOST:
                //Handled by VisitorControl
                return;
            
            case TASK.FIX:
                return;
            
            default: Debug.LogError("ERR: TASK STATE MACHINE BROKE :("); return;
        }   
    }

    public void IncreaseHappiness(float happiness)
    {
        GameManager.manager.multM.IncreaseMeterCurrent(happiness);
    }

    public void DecreaseHappiness(float sadness)
    {
        GameManager.manager.multM.DecreaseMeterCurrent(sadness);
    }

    public void StartTimeTillBreak()
    {
        if (brakingIn + 1 <= Time.fixedTime && //magic number to control order of operations
            broken == false &&
            brakable == true) 
        {
            brakingIn = (int)Time.fixedTime + (maxTime - Random.Range(brakeDownRate - rangeEnds, brakeDownRate + rangeEnds));
        }
    }

    private void BrakeDown()
    {
        if (brakingIn < Time.fixedTime &&
            brakable == true)
        {
            broken = true;
            brokenVisual.SetActive(true);
            task = TASK.FIX;
        }
    }

    public void Fix()
    {
        task = OGtype;
        broken = false;
        brokenVisual.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {

        Debug.Log($"COLLIDE IT HAPPENED !!!!!!!!!!!!!!!!!");
        if (other.transform.parent.gameObject.TryGetComponent<Task>(out Task task) == true)
        {
            Debug.Log($"collision w/{other.transform.parent.gameObject.name}");
            if (task.TaskType == TASK.LOST)
            {
                if (task.gameObject.GetComponent<VisitorControl>().State == VisitorControl.STATE.LOST)
                { 
                    Debug.Log($"lost and found");
                    task.gameObject.GetComponent<VisitorControl>().GetFound(this.transform.position);
                }
            }
        }
    }

    void OnDestroy()
    {
        GameManager.manager.taskM.RemoveTask(pos, this);
    }
}
