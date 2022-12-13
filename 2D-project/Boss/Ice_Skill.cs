using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Skill : MonoBehaviour
{
    public Transform target;
    public Boss boss;

        void AttackTarget()
        {
            target.GetComponent<PlayerBase>().nowHp -= boss.atkDmg;
            
        }
        void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.gameObject.name == "naruto") 
                {
                    Debug.Log(other.gameObject.name);
                }
        }
        void OnTriggerEnter2D(Collider2D other){
            if(other.gameObject.tag == "Player" && other.GetComponent<PlayerBase>())
                {
                    target = other.transform;
                    AttackTarget();
                }
        }
}
