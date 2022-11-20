using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터는 스텟이 항상 같이 따라다녀야 해요
[RequireComponent(typeof(CharacterStatus))]
public class CharacterBase : MonoBehaviour
{
    private CharacterStatus _stat =null;
    //스테이터스를 불러오는 것
    public CharacterStatus Stat 
    {
        get
        {
            if(!_stat) _stat = GetComponent<CharacterStatus>();

            return _stat;
        }
        protected set => _stat = value;
    }

    protected Vector3 preferedDirection = Vector3.right; // 보고 싶어하는 방향

    public Vector3 FaceDirection {get => preferedDirection; } // get{return preferedDirection;}를 줄이기 위해 사용
    public Vector3 moveDirection {get; protected set; } //이동하는 방향
    public Vector3 physicsDirection {get; protected set;} //물리적으로 밀리고 있는 속도

    [SerializeField]
    protected GameObject appearance;
    public GameObject BaseAttack;
    
    //밀리고 있는 속도를 점진적으로 줄이기 위한 값
    [SerializeField]
    protected float linearDrag;

    [SerializeField]
    protected ControllerType controller; //따라갈 컨트롤러찾기

    public Rigidbody rigid3D {get; protected set;}
    public Animator anim {get; protected set;} 
    public AppearanceInfo appearInfo{get; protected set;}

    public Transform focusTarget;// 찾아가는 대상 내 위치를 변수로 저장

    public Vector3 focusPosistion; // 가려는 정확한 위치
    
    public Controller currentController; //이캐릭터를 조종하고 있는 컨트롤러

    System.Action CharacterUpdate; // 함수를 담아둘 수 있는 녀석
    System.Action<string> Broadcast2Addon; // 문자메시지 전송 시스템 

    public bool ChangeFaceOnmove; //움직이면 방향도 바뀜
    public bool rotationOnMove; // 움직이면 캐릭터가 회전함

    public bool changeFaceOnController;//컨트롤러가 보고 싶은 방향으로 방향을 돌림
    public bool rotationOnController; //컨트롤러가 보는 방향으로 회전함

    protected List<GameObject> stepList = new List<GameObject>(); //내가 밟고 있는 녀석들의 리스트

    public static CharacterBase playercharacter; 

    public float DestroyTimeDelay = 1.25f; // 죽을 때 사라지는 시간

    //땅을 밟고 있는가
    public bool isGround 
    {
        get
        {
            for(int i = 0; i < stepList.Count; i++)
            {
                //해당칸이 비어 있다면 해당 칸을 삭제합니다
                if(stepList[i] == null) stepList.RemoveAt(i);
            }
            return stepList.Count > 0;
        }
    }

    public void RemoveUpdate(System.Action targetAction)
    {
        CharacterUpdate -= targetAction;
    }

    public void ClaimUpdateForward(System.Action targetAction)
    {
        CharacterUpdate = targetAction + CharacterUpdate;
    }

    public void ClaimUpdateBackward(System.Action targetAction)
    {
        CharacterUpdate = CharacterUpdate + targetAction;
    }

    protected virtual void Start()
    {
        if(controller == ControllerType.LocalPlayer) //컨트롤러가 == 컨트롤러 타입이 로컬 플레이면은
        {
            playercharacter = this; //플레이어 캐릭터는 나다
            new PlayerController().Possess(this);
        }

        if(controller == ControllerType.AI_FollowPlayer)
        {
            new MonsterController().Possess(this);
        }

        CharacterUpdate += MovementUpdate; //캐릭터가 새로 갱신 한게 있으면 움직이자
        if(appearance) //appearance가 있으면 레퍼런스 안에있는 애니메이션을 불러오고
        {
            anim = appearance.GetComponent<Animator>();
            appearInfo = appearance.GetComponent<AppearanceInfo>();
        }
        else{ //만약 없으면 바로 실행할 수 있도록 하였음 
            anim = GetComponent<Animator>();
            appearInfo = GetComponent<AppearanceInfo>();
        }
        if(anim != null) CharacterUpdate += AnimationUpdate; //만약 애니메가 널이 아니면 캐릭터 업데이트는 갱신
        //else anim를 사용하여 값이 잘 보내지는지 확인 해볼수 있음

        rigid3D = GetComponent<Rigidbody>(); // 초기화
        GameManager.AddPlayer(this); //게임 메니저가 플레이어가 없어 플레이어를 추가 누구를 나를 
        PlayerController.mouseFix = (true); //마우스를 바로 잠금수 있게
        //Stat.owner = this;
    }

    virtual protected void Update()
    {
        //currentController.Control();
        transform.position += (moveDirection * Stat.MoveSpeed * Time.deltaTime); //위치에 방향을 더해서 간다 (방향 * 스피드 * 시간)

        if(changeFaceOnController) preferedDirection = currentController.direction;
        //만약 컨트롤러가 보고 싶은 방향으로 방향을 돌리 참이면 
        //                         preferedDirection은 보고싶어하는 방향 
        //                                              currentController는 조종하는 컨트롤direction은 내가 보고 싶은 방향
        if(rotationOnController) //보는 방향으로 회전
        {
            Vector3 lookPosition = transform.position + currentController.direction; //바라보는 포지션이 내 포지션에 조종 바라보는 방향을 움직임
            lookPosition.y = transform.position.y; //눕는 현상을 방지 해줌
            transform.LookAt(lookPosition); // 보는 바향으로 움직임
        }
        if(CharacterUpdate != null) CharacterUpdate(); //캐릭터 업데이트가 null이 아니면 캐릭터 업데이트를 불러온다
    }

    virtual public void Select(Transform selectTarget)
    {
        focusTarget = selectTarget;
        focusPosistion = selectTarget.position;
        transform.forward = focusPosistion - transform.position; // 정해진 타겟을 정면으로 보게 하는것
    }

    virtual public void MoveTo(Vector3 wantValue)
    {
        moveDirection = wantValue.normalized; //방향 = 목적지.출발지 실제로 움직이는것 moveDirection
    }

    virtual protected void MovementUpdate()
    {
        if(!Stat.movable || !Stat.actable) moveDirection = Vector3.zero;

        Vector3 totalDirection = (moveDirection * Mathf.Max(Stat.MoveSpeed, 0)) +physicsDirection;
    }

    virtual protected void Attack()
    {
        if(controller == ControllerType.LocalPlayer)
        {
            anim.SetTrigger("Attack");
            Vector3 firePos = transform.position + anim.transform.forward + new Vector3(0f, 1f, 0f); // transform은 위치, 회전, 크기를 담고 있는 컴포넌트
            GameObject fire = Instantiate(BaseAttack, firePos, transform.rotation);
            //Instantiate에 첫번째 인자는 아까 생성한 BaseAttack 프리팹 오브젝트입니다. 
            //두번째는 오브젝트를 생성할 위치인데 플레이어 기체에서 발사될 것이므로 플레이어 기체의 위치값을 넣습니다. 
            //세번째 마지막은 오브젝트의 회전값입니다. transform.rotation은 회전하는 방향으로 돌아갑니다. 특별히 회전시키지않고 기본으로 사용할 것이므로 Quaternion.identity로 줍니다. 

            ParticleCollisionInstance BaseAtk = fire.GetComponent<ParticleCollisionInstance>();
            BaseAtk.from = gameObject;
        }
         
        if(Broadcast2Addon != null) Broadcast2Addon("Attack");

    }

    virtual public void Attack(Vector3 wantPosition)
    {
        focusPosistion = wantPosition;
        Attack();
    }

    virtual public void Attack(CharacterBase wantTarget)
    {
        Select(wantTarget.transform);
        Attack();
    }

    public virtual float ApplyDamage(float damage, CharacterBase from)
    {
        if(Stat.HealthCurrent < damage) damage = Stat.HealthCurrent;

        if( damage == 0 ) return 0;

        Stat.HealthCurrent -= damage;

        anim.SetTrigger("Hit");
        Debug.Log(damage);

        if(Stat.HealthCurrent <= 0)
        {
            anim.SetTrigger("Die");
            if(Broadcast2Addon != null) Broadcast2Addon("Die");
            Destroy(gameObject,DestroyTimeDelay);
        }
        return damage;
    }

    virtual protected void AnimationUpdate()
    {
        
        anim.SetBool("Ground",isGround);

        if(moveDirection.magnitude > 0) //이동방향에 거리가 0보다 크면
        {
            anim.SetFloat("Velocity",1f);
        }
        else
        {
            anim.SetFloat("Velocity",0f);
        }
        
    }

    public virtual void Jump()
    {
        if(isGround)// 땅을 밝고 있어야 점프 가능
        {
            if(rigid3D) //rigidbody에 영향을 받고 있다면
            {
                rigid3D.AddForce(Vector3.up * Stat.JumpPower);// 점프를 시켜주는 부분 리지드바디에 힘을추가하면 
                stepList.Clear(); //점프를 하면 리스트를 지움
            }
        }
    }

    //들어오면 추가
    private void OnTriggerEnter(Collider other)
    {
        //밝고 있지 않으면 새로 추가
        if(!stepList.Contains(other.gameObject)) stepList.Add(other.gameObject); 
    }

    private void OnTriggerExit(Collider other) 
    {
        stepList.Remove(other.gameObject);
    }

}
