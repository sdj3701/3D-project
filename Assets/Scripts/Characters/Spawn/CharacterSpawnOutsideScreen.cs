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


        //3D라면
        // if (is3D)
        // {

        //     Vector3 rightTop;
        //     Vector3 leftTop;
        //     Vector3 leftBottom;

        //     RaycastHit hit;
        //     Ray currentRay;

        //     rightBottom = UIManager.screenRightBottom;
        //     leftTop = UIManager.screenLeftTop;

        //     rightTop = rightBottom;
        //     rightTop.y = leftTop.y;

        //     leftBottom = leftTop;
        //     leftBottom.y = rightBottom.y;

        //     //오른쪽 아래의 위치를 확인!
        //     currentRay = Camera.main.ScreenPointToRay(rightBottom); //각 커런트레이를 캐릭터 x,y축으로 바꾸기?
        //     Physics.Raycast(currentRay, out hit);
        //     rightBottom = hit.point;

        //     //오른쪽 위의 위치를 확인!
        //     currentRay = Camera.main.ScreenPointToRay(rightTop);
        //     Physics.Raycast(currentRay, out hit);
        //     rightTop = hit.point;


        //     //왼쪽 아래의 위치를 확인!
        //     currentRay = Camera.main.ScreenPointToRay(leftBottom);
        //     Physics.Raycast(currentRay, out hit);
        //     leftBottom = hit.point;

        //     //왼쪽 위의 위치를 확인!
        //     currentRay = Camera.main.ScreenPointToRay(leftTop);
        //     Physics.Raycast(currentRay, out hit);
        //     leftTop = hit.point;

        //     float randomValue = Random.value;

        //     Vector3 result = transform.position;

        //     switch(Random.Range(0,4))
        //     {
        //         case 0:
        //             result = leftBottom + (randomValue * (leftTop - leftBottom));
        //             break;
        //         case 1:
        //             result = leftBottom + (randomValue * (rightBottom - leftBottom));
        //             break;
        //         case 2:
        //             result = leftTop + (randomValue * (rightTop - leftTop));
        //             break;
        //         case 3:
        //             result = rightTop + (randomValue * (rightBottom - rightTop));
        //             break;
        //     };

        //     if (spawnWithPlayerHeight) result.z = GameManager.GetPlayer(0).transform.position.z;

        //     base.Spawn(index, result);
        // }
    }
}
