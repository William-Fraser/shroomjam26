using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Outline outline;

    void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineWidth = 10f;
        outline.OutlineColor = Color.white;
        outline.enabled = false;
    }

    public void SetFollowDistance(int order)
    {
        agent.stoppingDistance = order * 2;
    }

    public void MoveToTarget(Vector3 target)
    {
        agent.SetDestination(target);
        Debug.Log("destination set");
    }

    public void FixUp(Task currentTask)
    {
        //used for characters cleaning/fixing the park
        if (currentTask.TaskType == TASK.TRASH)
            Destroy(currentTask.gameObject);
        else
        {
            currentTask.Fix();
            GameManager.manager.taskM.RemoveTask(Vector3.zero, currentTask);
        }
    }
}
