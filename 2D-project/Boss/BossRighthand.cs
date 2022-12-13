using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossRighthand : MonoBehaviour
{
    public string enemyName;
    public int maxHp;
    public int nowHp;

    public GameObject prfHpbar;
    public GameObject canvas;
    RectTransform hpbar;
    public float height = 1.7f;

    public PlayerBase naruto;
    Image nowHpbar;

    public Transform target;
    Enemy enemy;

    private void SetBossRighthandStatus(string _enemyName, int _maxHp)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;

    }

    void Start()
    {
        
        hpbar = Instantiate(prfHpbar, canvas.transform).GetComponent<RectTransform>();

        if(name.Equals("bossrighthand"))
        {
            SetBossRighthandStatus("bossrighthand", 50);
        }
        nowHpbar = hpbar.transform.GetChild(0).GetComponent<Image>();

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


    public void Demage(int hit)
    {
        nowHp -= hit;
        Debug.Log(nowHp);
        if(nowHp <=0)
        {
            //GetComponent<EnemyAI>().enabled = false;    // 추적 비활성화
            GetComponent<Collider2D>().enabled = false; // 충돌체 비활성화
            Destroy(GetComponent<Rigidbody2D>());       // 중력 비활성화
            Destroy(gameObject, 1);
            Destroy(hpbar.gameObject,1);
        }
    }
}
