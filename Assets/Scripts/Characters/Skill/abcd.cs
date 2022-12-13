// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class abcd : MonoBehaviour
// {
//     public GameObject[] SkillPrefabs; //찍어낼 게임 오브젝트를 넣어요
//                                  //배열로 만든 이유는 게임 오브젝트를
//                                  //다양하게 찍어내기 위해서 입니다
//     private Collider area;

//     CharacterBase tryskill;

//     public int interval = 10;      //찍어낼 게임 오브젝트 갯수
    
//     float Delay = 10f;
//     float liftTime;
    
//     public List<GameObject> inst = new List<GameObject>();
    
//     void Update()
//     {
//         if(Delay <= 0)
//         {
//             for(int i = 0 ; i < interval ; i++)
//             {
//                 Spawn();//생성 + 스폰위치를 포함하는 함수
//             }
//             Delay = 10f;
//         }
//         Delay -= Time.deltaTime;
//     }
//     private Vector3 GetRandomPosition()
//     {
//         Vector3 basePosition = transform.position;
        
//         float randomX = Random.Range(-15f, 15f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
//         float randomZ = Random.Range(-15f, 15f); // 적이 나타날 Z좌표를 랜덤으로!
        
//         Vector3 spawnPos = new Vector3(randomX, 1, randomZ);
        
//         return spawnPos;
//     }

//     private void Spawn()
//     {   
//         int selection = Random.Range(0, SkillPrefabs.Length);
        
//         GameObject selectedPrefab = SkillPrefabs[selection];
        
//         Vector3 spawnPos = GetRandomPosition();//랜덤위치함수
        
//         GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
//         inst.Add(instance);
//     }

    
// }
