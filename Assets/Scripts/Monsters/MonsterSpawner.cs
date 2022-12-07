using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    public float spawnTime;
    float currTime;
    public GameObject Monster;

    void Start()
    {
         // 오브젝트를 몇초마다 생성할 것인지 조건문으로 만든다. 여기서는 10초로 했다. spawnTime에 띠라 생성 한 번만 하기 위해서 스타트에 적용함
        if (currTime > spawnTime)
            {
                float randomX = Random.Range(-30f, 30f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
                float randomZ = Random.Range(-30f, 30f); // 적이 나타날 Z좌표를 랜덤으로!

                // 생성할 오브젝트를 불러온다.
                GameObject monster = Instantiate(Monster);

                // 불러온 오브젝트를 랜덤하게 생성한 좌표값으로 위치를 옮긴다.
                Monster.transform.position = new Vector3(randomX, 0, randomZ);

                // 시간을 0으로 되돌려주면, 10초마다 반복된다.
                currTime = 0;
            }   
    }

    void Update()
    {
        // 시간이 흐르게 만들어준다.
        currTime += Time.deltaTime;
    }
}
