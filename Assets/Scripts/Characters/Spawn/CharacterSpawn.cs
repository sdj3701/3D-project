using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    public GameObject[] characterPrefab; //몬스터를 배열로 등록하기
    public Vector3 maxDistance;
    public Vector3 minDistance;

    public float interval; // 소환 마리수
    float leftTime;

    protected int currentSpawn = 0;

    public int maxNumber; //최대 소환 수
    public List<GameObject> inst = new List<GameObject>();

    void Start()
    {
        
    }

    protected virtual void Update()
    {
        for(int i = 0; i < inst.Count; i++)
        {
            //없거나    비     활성화됨                     해당칸 삭제
            if(!inst[i] || !inst[i].activeInHierarchy) inst.RemoveAt(i);
        }

        leftTime -= Time.deltaTime;
        if(leftTime <= 0)
        {
            Spawn(currentSpawn);
        }
    }

    public virtual void Spawn(int index, Vector3 wantPosition)
    {
        //배열 바깥쪽에서 찾으려면 당연히 없습니다
        if(characterPrefab.IsOutSide(index)) return;

        //일정 수 이상은 만들 수 없어요
        if(inst.Count < maxNumber)
        {
            GameObject newInst = Instantiate(characterPrefab[index]);
            //새로운 캐릭터 만들었으니까 리스트에 넣어주기
            inst.Add(newInst);

            newInst.transform.position = wantPosition;
            //쿨탐 초기화
            leftTime = interval;
        }
    }

    public virtual void Spawn(int index)
    {
        Spawn(index, transform.position.GetRandomPosition(minDistance, maxDistance));
        
    }
}
