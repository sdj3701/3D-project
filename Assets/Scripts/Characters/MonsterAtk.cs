using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAtk : Character_Nav
{
    public GameObject[] SkillPrefabs; //찍어낼 게임 오브젝트를 넣어요
                                 //배열로 만든 이유는 게임 오브젝트를
                                 //다양하게 찍어내기 위해서 입니다
    public int interval = 10;      //찍어낼 게임 오브젝트 갯수
    
    float skillDelay = 3f;
    float liftTime;
    
    public List<GameObject> inst = new List<GameObject>();
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

        if(distance >= 5 )
        {
            if(skillDelay <= 0)
            {
                Skill();
                for(int i = 0 ; i < interval ; i++)
                {
                    Spawn();//생성 + 스폰위치를 포함하는 함수
                }
                skillDelay = 10f;
            }
            skillDelay -= Time.deltaTime;
        }

        if(Delay <= 0)
        {
            Attack();
        }
        Delay -= Time.deltaTime;
        base.Update();
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        
        float randomX = Random.Range(-15f, 15f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
        float randomZ = Random.Range(-15f, 15f); // 적이 나타날 Z좌표를 랜덤으로!
        
        Vector3 spawnPos = new Vector3(randomX, 1, randomZ);
        
        return spawnPos;
    }

    private void Spawn()
    {   
        int selection = Random.Range(0, SkillPrefabs.Length);
        
        if(SkillPrefabs.Length <= 0)
        {
            return;
        }
        GameObject selectedPrefab = SkillPrefabs[selection];
        
        Vector3 spawnPos = GetRandomPosition();//랜덤위치함수
        
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        inst.Add(instance);
    }

}
