using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monster_Nav : MonsterBase
{
    protected NavMeshAgent agent;

    protected override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        base.Start();
    }

    public override void MoveTo(Vector3 wantValue)
    {
        //방향 = 
        moveDirection = (wantValue - transform.position).normalized;
        preferedDirection = moveDirection.normalized;

        agent.SetDestination(wantValue);
    }

    protected override void MovementUpdate()
    {
        if(!Stat.movable || !Stat.actable) agent.SetDestination(transform.position);

        Vector3 totalDirection = physicsDirection * Time.deltaTime;

        transform.position += totalDirection;
        float physicsSpeed = physicsDirection.magnitude;
        float currentDrag = linearDrag * Time.deltaTime;

        if(physicsSpeed > currentDrag)
        {
            physicsDirection = physicsDirection.normalized * (physicsSpeed - currentDrag);
        }
        else
        {
            physicsDirection = Vector3.zero;
        }
    }
}
