/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float DestroyTimeDelay = 5; //몇초뒤에 사라지길 원하는지
    public bool UseWorldSpacePosition; //
    public float Offset = 0; //위치
    public Vector3 rotationOffset = new Vector3(0,0,0); //회전위치
    public bool useOnlyRotationOffset = true; //
    public bool UseFirePointRotation; //사용자 불 포인트 회전
    public bool DestoyMainEffect = false; //삭제 메인 이펙트
    private ParticleSystem part; //속성 파트
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;
    public GameObject from;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {   
        if(other.tag == gameObject.tag || other == from) return;//내가 쏜게 나한테 맞는지 확인 나를 무시하고 날라가세요
        Debug.Log(other);

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);//collision 부딪히면 이벤트 발생
        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                if (!UseWorldSpacePosition) instance.transform.parent = transform;
                if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                else if (rotationOffset != Vector3.zero && useOnlyRotationOffset)  //회전값 설정이 0이 아니거나 참이면
                { 
                    instance.transform.rotation = Quaternion.Euler(rotationOffset); //그쪽 방행으로 발사
                }
                else
                {
                    instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                    instance.transform.rotation *= Quaternion.Euler(rotationOffset);
                }
                Destroy(instance, DestroyTimeDelay);
            }
        }
        if (DestoyMainEffect == true) //삭제 이펙트가 실행되면 삭제함
        {
            Destroy(gameObject, DestroyTimeDelay + 0.5f);
        }
    }
}
