using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private bool isAttacking = false;
    Vector3 playerPos;
    Vector3 whereToAtk;
    public GameObject warning;
    public GameObject Atk1;

    Vector3 playerPo;
    Vector3 whereToSkill;
    public GameObject Ice_warning;
    public GameObject Ice_Atk1;

    Vector3 playerP;
    Vector3 whereToLaser;
    public GameObject Laser_Atk1;

    public GameObject boss1;
    public GameObject boss2;
    public GameObject boss3;

    public string enemyName;
    public int atkDmg;
    public float atkSpeed;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;
    public float attackDelay;
    public Animator bossAnimator;

    public Transform target;

    public bool check =true;
    

    private void SetBossStatus(string _enemyName,  int _atkDmg, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision)
    {
        enemyName = _enemyName;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
        fieldOfVision = _fieldOfVision;
    }

    void Start()
    {
        if(name.Equals("boss"))
        {
            SetBossStatus("boss", 10, 1.5f, 2 , 2.5f , 7f);
        }
        SetAttackSpeed(atkSpeed);
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        if(!target) //타겟을 찾지 못하면 멈춘다
        {
            bossAnimator.SetBool("moving", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position); //타겟을 찾는 것

        if (attackDelay == 0 && distance <= fieldOfVision)
        {
            //FaceTarget();

            if (distance <= atkRange)
            {
                //AttackTarget();
            }
        }
    }

    void SetAttackSpeed(float speed) 
    {
        bossAnimator.SetFloat("attackSpeed", speed);    
    }
    
    //보스 패턴 낙하 대미지
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){

            if(boss2 != null)
            {
                playerPos = other.transform.position;
                playerPos.y = 1.40f;
                StartCoroutine(BeforeAttack());
            }
            if(boss3 != null )
            {
                playerPo = other.transform.position;
                StartCoroutine(BeforeSkill());
            }
            if(boss1 != null && isAttacking == false)
            {
                int currentSkill = Random.Range(0,3);
                switch (currentSkill)
                {
                    case 0:
                        playerP.x = -29.3f;
                        playerP.y =  5.4f;
                        StartCoroutine(LaserSkill());
                        break;
                    case 1:
                        playerPo = other.transform.position;
                        StartCoroutine(BeforeSkill());
                        break;
                    default:
                        playerPos = other.transform.position;
                        playerPos.y = 1.40f;
                        StartCoroutine(BeforeAttack());
                    break;
                }
            }
        }
    }    
    
    IEnumerator BeforeAttack(){
        if(isAttacking == false ) {
            whereToAtk = playerPos;
            isAttacking = true;
            //Debug.Log("감지한 위치 : " + whereToAtk);
            bossAnimator.SetTrigger("Atk1");
            GameObject a = Instantiate(warning, whereToAtk, transform.rotation);
            yield return new WaitForSeconds(2f); // 몇초 동안 남을 건가?
            Destroy(a);
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack(){
        GameObject b = Instantiate(Atk1, whereToAtk, transform.rotation);
        BossAttack attack = b.GetComponent<BossAttack>(); 
        attack.boss = this;
        StartCoroutine(CoolTime(3f));
        yield return new WaitForSeconds(1f);
        Destroy(b);
    }

    IEnumerator BeforeSkill(){
        if(isAttacking == false ) {
            whereToSkill = playerPo;
            isAttacking = true;
            bossAnimator.SetTrigger("Atk2");
            GameObject c = Instantiate(Ice_warning, whereToSkill , transform.rotation);
            yield return new WaitForSeconds(2f); // 몇초 동안 남을 건가?
            Destroy(c);
            StartCoroutine("Skill");
        }
    }

    IEnumerator Skill(){
        GameObject d = Instantiate(Ice_Atk1 , whereToSkill, transform.rotation);
        Ice_Skill skill = d.GetComponent<Ice_Skill>();
        skill.boss = this;
        yield return new WaitForSeconds(1f);
        Destroy(d);
        isAttacking = false;
    }

    public void StartLaser()
    {
        StartCoroutine(LaserShot());
    }

    IEnumerator LaserShot()
    {
        GameObject e = Instantiate(Laser_Atk1, whereToLaser, transform.rotation);
        Laser_Skill laser = e.GetComponent<Laser_Skill>();
        laser.boss = this;
        yield return new WaitForSeconds(2f);
        Destroy(e);
    }

    IEnumerator LaserSkill()
    {
        if(isAttacking == false)  
        {
            bossAnimator.SetTrigger("Atk3");
            whereToLaser = playerP;
            isAttacking = true;
            yield return new WaitForSeconds(2f);
            StartCoroutine(CoolTime(6f));
        }
    }

    IEnumerator CoolTime (float cool)
    {
        while (cool > 1.0f)
        {
            Debug.Log(cool);
            cool -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        if(cool < 1.0f)
        {
            isAttacking = false;
        }
    }

        //1페
        //왼쪽 팔 나가서 떄리기

        //2페 
        //레이저 갈라지기

        //3페
        // 이속 감속
        // 머리 박치기
        // 머리로 바닥 쓸기
}
