using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    readonly private float speed = 3.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    public void Init(GameObject playerRef)
    {
        player = playerRef;
    }

    void FixedUpdate()
    {
        agent.destination = player.transform.position;
    }
}
