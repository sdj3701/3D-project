using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterStatus))]
public class MonsterBase : MonoBehaviour
{
    private MonsterStatus _stat =null;
    //스테이터스를 불러오는 것
    public MonsterStatus Stat 
    {
        get
        {
            if(!_stat) _stat = GetComponent<MonsterStatus>();

            return _stat;
        }
        protected set => _stat = value;
    }
    [SerializeField]
    protected ControllerType controller;

    protected Vector3 preferedDirection = Vector3.right; // 보고 싶어하는 방향

    public Vector3 FaceDirection {get => preferedDirection; } // get{return preferedDirection;}를 줄이기 위해 사용

    public Vector3 moveDirection {get; protected set; } //이동하는 방향
    public Vector3 physicsDirection {get; protected set;} //물리적으로 밀리고 있는 속도

    public Controller currentController;

    System.Action CharacterUpdate; // 함수를 담아둘 수 있는 녀석
    System.Action<string> Broadcast2Addon; // 문자메시지 전송 시스템 

    public bool ChangeFaceOnmove; //움직이면 방향도 바뀜
    public bool rotationOnMove; // 움직이면 캐릭터가 회전함

    public bool changeFaceOnController;//컨트롤러가 보고 싶은 방향으로 방향을 돌림
    public bool rotationOnController; //컨트롤러가 보는 방향으로 회전함

    public void ClaimUpdateForward(System.Action targetAction)
    {
        CharacterUpdate = targetAction + CharacterUpdate;
    }

    public void ClaimUpdateBackward(System.Action targetAction)
    {
        CharacterUpdate = CharacterUpdate + targetAction;
    }

    [SerializeField]
    protected float linearDrag;

    public Rigidbody rigid3D {get; protected set;}
    public Animator anim {get; protected set;}    

    public Transform focusTarget;// 찾아가는 대상
    public Vector3 focusPosistion; // 가려는 정확한 위치

    virtual public void Select(Transform selectTarget)
    {
        focusTarget = selectTarget;
        focusPosistion = selectTarget.position;
        transform.forward = focusPosistion - transform.position;
    }

    virtual public void MoveTo(Vector3 wantValue)
    {
        moveDirection = wantValue.normalized; //방향 = 목적지.출발지
    }

    virtual protected void MovementUpdate()
    {
        if(!Stat.movable || !Stat.actable) moveDirection = Vector3.zero;
        Vector3 totalDirection = (moveDirection * Mathf.Max(Stat.MoveSpeed, 0)) +physicsDirection;
    }

    private void OnTriggerEnter(Collider other) //콜라이더 안에 들어오면 공격
    {
        CharacterBase Player = other.GetComponent<CharacterBase>();
        Attack();

    }

    public void Attack()
    {
        anim.SetInteger("animation",3);
    }

    void OnTriggerExit(Collider other) //콜라이더 밖으로 넘어가면 대기상태
    {
        CharacterBase Player = other.GetComponent<CharacterBase>();
        Idle();
    }
    public void Idle()
    {
        anim.SetInteger("animation",1);
    }
    

    virtual protected void Start()
    {
        rigid3D = GetComponent<Rigidbody>();
        Stat = GetComponent<MonsterStatus>();
        anim = GetComponent<Animator>();
        Stat.owner1 = this;
    }

    void Update()
    {
        Select(focusTarget);
    }

    public virtual float ApplyDamage(float damage, MonsterBase from)
    {
        if(Stat.HealthCurrent < damage) damage = Stat.HealthCurrent;

        if( damage == 0 ) return 0;

        Stat.HealthCurrent -= damage;

        anim.SetInteger("animation", 5);

        if(Stat.HealthCurrent <= 0)
        {
            if(Broadcast2Addon != null) Broadcast2Addon("Die");
            Destroy(gameObject);
        }
        return damage;
    }

    //  virtual protected void AnimationUpdate() //움직이기 위한 에니메이션 업데이트
    // {
    //     anim.SetFloat("Velocity", moveDirection.magnitude);

    //     if(changeFaceOnController)
    //     {
    //         if(moveDirection.magnitude > 0)
    //         {
    //             //가고있는 방향과 보고싶은 방향
    //             float currentRotation = transform.rotation.eulerAngles.y;
    //             float moveRotation = -moveDirection.ToHorizontalAngle();

    //             Vector3 calculatDistance = (moveRotation - currentRotation + 90.0f).ToDirection();
    //             anim.SetFloat("Move_X", calculatDistance.x);
    //             anim.SetFloat("Move_Z", calculatDistance.y);

    //         }
    //     }
    //     else
    //     {
    //         //움직일 양만큼
    //             anim.SetFloat("Move_X", preferedDirection.x);
    //             anim.SetFloat("Move_Y", preferedDirection.y);
    //             anim.SetFloat("Move_Z", preferedDirection.z);

    //     }

    // }

}

// 콜라이더를 다중으로 쓸수 있는지? 
// 아니면 플레이어의 거리가 몇 이상이면 스킬을 사용하게 좋은지

//에셋 sd원거리 캐릭터와 보스 땅 스킬

// 도넛 도장을 찍을 수 있는지
//
