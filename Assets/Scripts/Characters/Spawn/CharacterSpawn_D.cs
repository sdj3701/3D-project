using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn_D : CharacterSpawnOutsideScreen
{
    public override void Spawn(int index)
    {
        currentSpawn = Mathf.FloorToInt(GameManager_3D.gameProgress * characterPrefab.Length); //몬스터 진화

        currentSpawn = Random.Range(Mathf.FloorToInt(currentSpawn * 0.5f), currentSpawn); //몇 번쨰 몬스터 소환

        base.Spawn(index);
    }

}
