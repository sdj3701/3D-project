using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Character_Nav : CharacterBase
{
    protected NavMeshAgent agent;

    protected override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        base.Start();
    }

    public override void MoveTo(Vector3 wantValue)
    {
        //이쪽을 보면 되는 건가?
        moveDirection = (wantValue - transform.position).normalized;
        preferedDirection = moveDirection.normalized;

        //저쪽으로 가야해
        agent.SetDestination(wantValue);
    }

    protected override void MovementUpdate()
    {
        //목적지 설정 (그쪽으로 가기)
        if(!Stat.movable || !Stat.actable) agent.SetDestination(transform.position);

        if(playercharacter != null) //플레이어 캐릭터가 있으면 
        {
            agent.SetDestination(playercharacter.transform.position); //에이전트는 가야한다 (플레이어 캐릭터의 위치로)
        }
        //이동은 NavMexh가 해줌 그래서 물리 작용만 계산하기
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
//내비를 사용하기 위해서는 유니티 윈도우에서 AI 내비를 들어가서 bake를 눌러주면 됨