using UnityEngine;
using UnityEngine.AI;

public class TestBot : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }
}
