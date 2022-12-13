using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGreen : MonoBehaviour
{
    // 10초간 공속 50% 증가
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            PlayerBase naruto = col.GetComponent<PlayerBase>();
            StartCoroutine(IncreaseAttackSpeed(naruto));

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator IncreaseAttackSpeed(PlayerBase naruto)
    {
        float AttackSpeed = naruto.GetAttackSpeed();
        naruto.SetackSpeed(AttackSpeed * 1.5f);

        yield return new WaitForSeconds(10);

        naruto.SetackSpeed(AttackSpeed);
        Destroy(gameObject);
        Debug.Log("공격 속도 1.5배 증가");
    }
}
