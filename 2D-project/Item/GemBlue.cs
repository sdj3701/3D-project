using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBlue : MonoBehaviour
{
    // 10초간 이동속도 100% 증가 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            PlayerBase naruto = col.GetComponent<PlayerBase>();
            StartCoroutine(IncreaseMoveSpeed(naruto));
            
            GetComponent<SpriteRenderer>().enabled = false;  // Destroy를 하면 코루틴이 정지되므로, 임시로 그림만 없앴음.
            GetComponent<Collider2D>().enabled = false;      // 충돌체 제거
        }
    }

    IEnumerator IncreaseMoveSpeed(PlayerBase naruto)
    {
        float MoveSpeed = naruto.getMoveSpeed();
        naruto.SetMoveSpeed(MoveSpeed * 2f);

        yield return new WaitForSeconds(10);

        naruto.SetMoveSpeed(MoveSpeed);
        Destroy(gameObject);
        Debug.Log("이동속도 2배 증가");
    }
}
