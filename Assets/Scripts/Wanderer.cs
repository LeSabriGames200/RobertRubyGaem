using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wanderer : MonoBehaviour
{
    public AILocationSelector aiLocationSelector;
    public Transform wanderTarget;
    public NavMeshAgent navMeshAgent;

    private float coolDown;

    void Start()
    {
        Wander();
    }

    void Update()
    {
        if (coolDown > 0f)
        {
            coolDown -= 1f * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (navMeshAgent.velocity.magnitude <= 1f & coolDown <= 0f)
        {
            Wander();
        }
    }

    public void Wander()
    {
        aiLocationSelector.NewTarget();
        navMeshAgent.SetDestination(wanderTarget.position);
    }
}
