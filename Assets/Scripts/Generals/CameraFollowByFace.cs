using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowByFace : MonoBehaviour
{
    CharacterBase targetCharacter;
    public Vector3 offset; // 위치설정
    public float distance; // 거리 범위

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    Physics.Raycast(currentRay, out hit, distance);

    if(hit.collider == null)
    {
        calculatDistance = distance;
    }
    else
    {
        calculatDistance = hit.distance;
    }

    transform.position += targetCharacter.FaceDirection * -calculatDistance;
    }
}
