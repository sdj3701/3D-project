using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowByFace : MonoBehaviour
{
    CharacterBase targetCharacter;
    public Vector3 offset; // 위치설정
    public float distance; // 거리 범위

    void Start()
    {

    }

    void Update()
    {
        if(targetCharacter == null)//대상을 찾지 못하면
        {
            targetCharacter = GameManager.GetPlayer(0);                
            if(targetCharacter == null)
            {
                return;
            }
        }

    distance -=Input.GetAxis("Mouse ScrollWheel"); // 마우스 휠을 사용 하여 줌인 아웃이 가능
    distance = Mathf.Clamp(distance, 2.0f, 5.0f);

    transform.position = targetCharacter.transform.position + offset; // 카메라의 위치 = 대상캐릭터의 위치 + 옮기고 싶은 위치

    transform.LookAt(transform.position + targetCharacter.FaceDirection); // 이쪽을 보고싶다 제 위치 + 캐틱터가 보는 방향

    float calculatDistance;

    RaycastHit hit;

    Ray currentRay = new Ray();

    currentRay.direction = -targetCharacter.FaceDirection;

    currentRay.origin = transform.position;

    Physics.Raycast(currentRay, out hit, distance, 1);

    if(hit.collider == null) // 안부딪혔는데 
    {
        calculatDistance = distance; //그냥 카메라 볌위 그대로 출력
    }
    else 
    {
        calculatDistance = hit.distance; // hit를 사용하면 카메라가 가까이 출력됨
    }
    transform.position += targetCharacter.FaceDirection * -calculatDistance;
    }

}
