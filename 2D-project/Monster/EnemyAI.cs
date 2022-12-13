using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    float attackDelay;
    public float Combo1;

    Enemy enemy;
    Animator enemyAnimator;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        if(!target) //타겟을 찾지 못하면 멈춘다
        {
            enemyAnimator.SetBool("moving", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position); //타겟을 찾는 것

        if (attackDelay == 0 && distance <= enemy.fieldOfVision)
        {
            FaceTarget();

            if (distance <= enemy.atkRange)
            {
                AttackTarget();
            }
            else
            {
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    MoveToTarget();
                }
            }
        }
        else
        {
            enemyAnimator.SetBool("moving", false);
        }
    }

    void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * enemy.moveSpeed * Time.deltaTime);
        enemyAnimator.SetBool("moving", true);
    }

    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void AttackTarget()
    {
        target.GetComponent<PlayerBase>().nowHp -= enemy.atkDmg;
        enemyAnimator.SetTrigger("attack"); // 공격 애니메이션 실행
        enemyAnimator.SetFloat("v",Combo1);

            Combo1 += 1;
            Combo1 %= 3;

        attackDelay = enemy.atkSpeed; // 딜레이 충전
        
        
    }
}