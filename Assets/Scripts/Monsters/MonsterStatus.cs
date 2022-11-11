using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    public MonsterBase owner1 {get; set;}

    [Header("플레이어의 동맹")]
    public bool isAlly;

    [Header("생명력")]
    [SerializeField,Tooltip("캐릭터가 가지고 있는 현재 생명력입니다."), InspectorName("현재 생명력")]
    protected float _healthCurrent;

    public float HealthCurrent
    {
        get => _healthCurrent;

        set => _healthCurrent = Mathf.Clamp(value, 0 ,_healthMax);
    }


    [SerializeField, Tooltip("캐릭터가 최도로 가질 수 있는 생명력입니다."), InspectorName("최대 생명력")]
    protected float _healthMax;
    public float HealthMax
    {
        get => _healthMax;
        set
        {
            _healthMax = Mathf.Max(value,1);
            //최대생명력이 줄었을 떄, 현재 생명력이 제한이 됩니다.
            _healthCurrent = Mathf.Min(_healthCurrent, _healthMax);
        }
    }

    public float healthRate {get {return _healthCurrent/_healthMax; } }

    [Header("이동")]
    [SerializeField, Tooltip("캐릭터가 1 초당 이동할 수 있는 속도"), InspectorName("이동속도")]
    protected float moveSpeedBase;

    [SerializeField, Tooltip("캐릭터가 점프할 때에 사용하는 점프력 값입니다.")]
    protected float _jumpPower;
    public float JumpPower => _jumpPower;

    [Header("공격")]
    [SerializeField, Tooltip("캐릭터의 공격력을 설정"),InspectorName("공격력")]
    protected float _attackDamage;
    public float AttackDamage => _attackDamage;

    [SerializeField, Tooltip("캐릭터가 일반공격을 하고 다시 공격할 수 있을 때까지 걸리는 시간"), InspectorName("공격 딜레이")]
    protected float _attackDelay;
    public float AttackDelay => _attackDelay;

    [SerializeField, Tooltip("캐릭터의 일반공격이 닿을 수 있는 범위"), InspectorName("공격범위")]
    protected float _attackRange;
    public float AttackRange => _attackRange;

    [Header("각종배율")]
    public float moveSpeedMultiplier = 1.0f;
    public float MoveSpeed => moveSpeedBase * moveSpeedMultiplier;

    protected int actStack = 0; // 행동불가
    public bool actable
    {
        get{ return actStack <= 0;}
        set{ actStack = value ? --actStack : ++actStack; } //켜주세요 꺼주세요
    }
    
    protected int moveStack = 0;//이동불가
    public bool movable
    {
        get{ return moveStack <= 0;}
        set{ moveStack = value ? --moveStack : ++moveStack; }
    }

    protected int attackStack = 0; //공격불가
    public bool attackable
    {
        get{return attackStack <= 0;}
        set{attackStack = value ? --attackStack : ++attackStack; }
    }

    protected int controlStack = 0;
    public bool controlable
    {
        get{return controlStack <= 0;}
        set{ controlStack = value ? --controlStack : controlStack;}
    }

    void Update()
    {
        
    }
}
