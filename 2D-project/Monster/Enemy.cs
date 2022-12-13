using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;
    

    public GameObject prfHpbar;
    public GameObject canvas;
    RectTransform hpbar;
    public float height = 1.7f;

    public PlayerBase naruto;
    Image nowHpbar;
    public Animator enemyAnimator;

    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
        fieldOfVision = _fieldOfVision;
    }


    void Start()
    {
        //ü�¹ٸ� ���������� canvas�� �ڽ����� �����ϰ�, ü�¹��� ��ġ ������ �����ϱ� ���� hpbar�� ����
        //GetComponent�� �پ��ִ� ������Ʈ���� ������ ����
        hpbar = Instantiate(prfHpbar, canvas.transform).GetComponent<RectTransform>();

        if(name.Equals("enemy"))
        {
            SetEnemyStatus("enemy", 100, 10, 1.5f, 2 , 2.5f , 7f);
        }
        nowHpbar = hpbar.transform.GetChild(0).GetComponent<Image>();

        SetAttackSpeed(atkSpeed);

    }

    void Update()
    {
        //���� ��ǥ�� ��ũ�� ��ǥ ��, UI�·�� �ٲ��ִ� �Լ�
        Vector3 _hpbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        //�ٲ���� ��ǥ ������ ü�¹ٰ� �̵�
        hpbar.position = _hpbarPos;
        //Debug.Log(_hpbarPos); ü�¹� ��ġ Ȯ��

        //fillAmount ü�¹� ǥ��
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;
    }

    void SetAttackSpeed(float speed) 
    {
        enemyAnimator.SetFloat("attackSpeed", speed);    
    }
    
    public void Demage(int hit)
    {
        nowHp -= hit;
        Debug.Log(nowHp);
        if(nowHp <= 0) 
        {
            enemyAnimator.SetTrigger("die");            // die 애니메이션 실행
            GetComponent<EnemyAI>().enabled = false;    // 추적 비활성화
            GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
            Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
            Destroy(gameObject, 3);
            Destroy(hpbar.gameObject,3);
        }
    }

}
