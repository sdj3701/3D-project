using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRed : MonoBehaviour
{
    // 체력 50 회복
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerBase naruto = col.GetComponent<PlayerBase>();
            naruto.nowHp += 50;
            if (naruto.nowHp > naruto.maxHp) 
                naruto.nowHp = naruto.maxHp;
            Destroy(gameObject);
            Debug.Log("체력 +50 증가 ");
        }
    }
}
