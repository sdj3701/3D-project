using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAtk : Character_Nav
{
    public float maxDistance = 1;

    

    protected override void Attack()
    {
        if(Delay >= 0)
        {
            return;
        }
        RaycastHit hit;
        bool atk = Physics.SphereCast (transform.position, transform.lossyScale.x / 2, transform.forward, out hit, maxDistance);

        if(hit.collider != null)//충돌된 콜라이더가 있는지를 확인 해줘야함
        {
            CharacterBase target = hit.collider.gameObject.GetComponent<CharacterBase>();

            if(target != null && target.controller == ControllerType.LocalPlayer)
            {
                anim.SetTrigger("Attack");
                target.ApplyDamage(Stat.AttackDamage, this);
                Delay = Stat.AttackDelay;
            }
        }
    }

    protected override void Update() // 딜레이는 항상 돌아야 하므로 Update로 사용하기
    {
        Vector3 dir = playercharacter.transform.position - transform.position;
        float distance = Vector3.Distance(playercharacter.transform.position, transform.position);
        //Debug.Log(distance);

        if(distance >= 5)
        {
            Skill();
            // Vector3 a = new Vector3(playercharacter.transform.position.x - transform.position.x -3f, 0,playercharacter.transform.position.y - transform.position.y -3f);
            // transform.position += a * 1 * Time.deltaTime;
        }

        if(Delay <= 0)
        {
            Attack();
        }
        Delay -= Time.deltaTime;
        base.Update();
    }
}
