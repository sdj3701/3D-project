using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnOutsideScreen : CharacterSpawn
{
    public bool spawnWithPlayerHeight;
    //public bool is3D;

    public override void Spawn(int index)
    {
        CharacterBase player = GameManager.GetPlayer(0);
        
        if (player == null) return;

        //스포너의 위치를     플레이어의 위치로!
        Vector3 spawnerPos = player.transform.position;
        // 플레이어 높이에서 만들라구요? 플레이어의 z축위치로 이동!
        if (spawnWithPlayerHeight) spawnerPos.z = player.transform.position.z;

        //위에서 구상했으니 실제로 적용하는 부분!
        transform.position = spawnerPos;

        //Vector3 rightBottom; 
    
        float randomX = Random.Range(-30f, 30f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
        float randomZ = Random.Range(-30f, 30f); // 적이 나타날 Z좌표를 랜덤으로!

        Vector3 result = new Vector3(spawnerPos.x + randomX, spawnerPos.y, spawnerPos.z + randomZ); //캐릭터 중심으로 x,z 10 만큼 떨어져 나옴

        base.Spawn(index, result);

    }
}
