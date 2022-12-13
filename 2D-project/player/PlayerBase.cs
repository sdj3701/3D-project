using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    public int maxHp;
    public int nowHp;
    
    public int Combo;
    public int atkDmg;

    public float maxSpeed = 10.0f;

    public float jumpPower = 10.0f;

    public bool attacked = false;

    public Image nowHpbar;
    public Image img_Skill;

    FillAmount fillamount;

    public Vector3 faceDirection = Vector3.right;
    public GameObject ObjNaruto;
    
    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public float atkSpeed = 5.0f;
    public float MoveSpeed = 5.0f;

    public bool isDie;
    public float cool;

    public void naatk() //애니메이션에 들어가 add 이벤트를 원하는 타이밍에 추가햐여 데미지를 추가 
    {
        RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position + (faceDirection * 1), new Vector3(8, 4, 0), 0, Vector3.forward);
            Debug.Log(hit.Length);
            foreach (var current in hit)
            {
                if (current)
                {
                    Enemy hitenemy = current.transform.GetComponent<Enemy>();
                    if (hitenemy) hitenemy.Demage(atkDmg+20);
                }
                if (current)
                {
                    BossHead hitenemy = current.transform.GetComponent<BossHead>();
                    if (hitenemy) hitenemy.Demage(atkDmg+20);
                }
                if (current)
                {
                    BossLefthand hitenemy = current.transform.GetComponent<BossLefthand>();
                    if (hitenemy) hitenemy.Demage(atkDmg+20);
                }
                if (current)
                {
                    BossRighthand hitenemy = current.transform.GetComponent<BossRighthand>();
                    if (hitenemy) hitenemy.Demage(atkDmg+20);
                }
                
            }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void AttackTrue()
    {
        attacked = true;
    }
    void AttackFalse()
    {
        attacked = false;
    }
    void SetAttackSpeed(float MoveSpeed)
    {
        animator.SetFloat("attackSpeed", MoveSpeed);
    }

    public float getMoveSpeed()
    {
        return MoveSpeed;
    }

    public void SetMoveSpeed(float ace)
    {
        MoveSpeed = ace;
    }
    public float GetAttackSpeed()
    {
        return atkSpeed;
    }
    public void SetackSpeed(float aspeed)
    {
        atkSpeed = aspeed;
    }

    private void Start()
    {
        maxHp = 100;
        nowHp = 100;
        atkDmg = 10;

        transform.position = new Vector3(45, -7, 0); //시작위치
        //transform.position = new Vector3(-27, 1, 0);
        animator = GetComponent<Animator>();

        SetAttackSpeed(1.5f);

        StartCoroutine(CheckNarutoDeath());

    }

    private void Update()
    {

        if(isDie) return;//죽음

           cool -= Time.deltaTime; //쿨타임

        nowHpbar.fillAmount = (float)nowHp / (float)maxHp; //체력바 표시 하기
        Debug.Log(nowHp);

        float h = Input.GetAxis("Horizontal"); // 부드럽게 이동하기 
        if (h < 0)// 왼쪽 오른른쪽 바라보기 그리고 움직이는 애니메이션 작동 
        {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("moving", true);
        }
        else if (h > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("moving", true);
        }
        else // 멈추면 애니메이션 작동 중지
        {
            animator.SetBool("moving", false);
        }
        Vector3 currentMove = new Vector3(h, 0, 0);

        transform.Translate(currentMove * MoveSpeed * Time.deltaTime);

        if(currentMove.magnitude > 0) faceDirection = currentMove.normalized;

        if(Input.GetKeyDown(KeyCode.Z) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))//어택 이니메이션 콤보를 넣어 다양한 모션
        {
            
            animator.SetTrigger("attack");//누르면 자동으로 어택 애니메이션 작동
            animator.SetFloat("v",Combo);//어택 내부에 콤보 모션을 추가 하여 하나씩 더하면서 다양한 애니메이션 추가

            Combo += 1;
            Combo %= 3;

            //레이캐스트를 만들어 공격 범위를 시각화 하기 닿으면 데미지 들어가게 하기
            RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position + (faceDirection * 1), new Vector3(3, 1, 0), 0, Vector3.forward);

            foreach (var current in hit) //보스나 적을 때렸을 때 데미지 들어가게 하기
            {

                if (current)
                {
                    Enemy hitenemy = current.transform.GetComponent<Enemy>();
                    if (hitenemy) hitenemy.Demage(atkDmg);
                }
                if (current)
                {
                    BossHead hitenemy = current.transform.GetComponent<BossHead>();
                    if (hitenemy) hitenemy.Demage(atkDmg);
                }
                if (current)
                {
                    BossLefthand hitenemy = current.transform.GetComponent<BossLefthand>();
                    if (hitenemy) hitenemy.Demage(atkDmg);
                }
                if (current)
                {
                    BossRighthand hitenemy = current.transform.GetComponent<BossRighthand>();
                    if (hitenemy) hitenemy.Demage(atkDmg);
                }
            }
        }

        //나루토 스킬 추가
        if(Input.GetKeyDown(KeyCode.X) && !animator.GetCurrentAnimatorStateInfo(0).IsName("na") && cool <= 0)
        {
            cool = 10;
            animator.SetTrigger("na");
           
        }

        //점프 애니메이션 추가
        if (Input.GetButtonDown("Jump") && !animator.GetBool("jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            
            animator.SetBool("jump", true);
        }
        
    }
    
    // 바닥에 닿았을 떄 점프를 중지하는법
    private void OnCollisionStay2D(Collision2D other) 
    {
        foreach(var current in other.contacts)
        {
            if(current.point.y < transform.position.y)
            {
                animator.SetBool("jump", false);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
                animator.SetBool("jump", true);
        
    }

    private void Die() // 죽었들때
    {
        animator.SetTrigger("die");
        Destroy(gameObject, 2);
        isDie =true;
        Debug.Log("YOU DIE");
        SceneManager.LoadScene("SampleScene");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1);
        Gizmos.DrawCube(transform.position + (faceDirection * 1), new Vector3(3,1,0));
    }

    IEnumerator CheckNarutoDeath() //죽었을 때 다시 시작
    {
        while(true)
        {
        // 땅 밑으로 떨어졌다면
        if(transform.position.y < -15)
        {
            SceneManager.LoadScene("SampleScene"); // Scene 재시작
        }

        if(nowHp <= 0)
        {
            Die();
        }
        
        yield return new WaitForEndOfFrame(); // 매 프레임의 마지막 마다 실행
        }
    }


}