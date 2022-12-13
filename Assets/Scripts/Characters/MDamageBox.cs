using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDamageBox : MonoBehaviour
{
    public CharacterBase owner;
    public Collider col;
    public AppearanceInfo info;

    public string boxName; //이름 
    public float damage; //얘는 데미지가 몇인지

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        info = GetComponentInParent<AppearanceInfo>(); //부모에 있기는 해야 주든가 말든가
        
        //damage = owner.Stat.AttackDamage; //발사 하는 시점에 오너가누구 인지

        if(col && info)
        {
            //사전같은 경우는 [키와 값]을 동시에 넣어주어야 함
            info.DamageBoxes.Add(boxName, col);
        }
    }

    public void ApplyDamage(CharacterBase other)
    {
        //상대방이 있을 때에만 데미지를 줍니다 그리고 동맹이 아니면
        if(other )
        {// 주인이 없으면 동맹을 확인 할 수 없어요 무조건 떄리기 ex) 투석
            other.ApplyDamage(damage, owner);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        //상대방의 캐릭터에게 데미지를 줍니다
        ApplyDamage(other.GetComponent<CharacterBase>());
    }

}
