using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject target;
    readonly private float speed = 3.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    public void Init(GameObject targetRef)
    {
        target = targetRef;
    }

    void FixedUpdate()
    {
        Debug.Assert(agent != null, "Agent cannot be null");
        Debug.Assert(target != null, "Target cannot be null");
        agent.destination = target.transform.position;
    }
}
