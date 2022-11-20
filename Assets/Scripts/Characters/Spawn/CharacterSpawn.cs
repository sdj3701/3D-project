using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    public GameObject[] characterPrefab;
    public Vector3 maxDistance;
    public Vector3 minDistance;

    public float interval;
    float leftTime;

    protected int currentSpawn = 0;

    public int maxNumber;
    public List<GameObject> inst = new List<GameObject>();

    void Start()
    {
        
    }

    protected virtual void Update()
    {
        for(int i = 0; i < inst.Count; i++)
        {
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
        if(characterPrefab.IsOutSide(index)) return;

        if(inst.Count < maxNumber)
        {
            GameObject newInst = Instantiate(characterPrefab[index]);
            inst.Add(newInst);

            newInst.transform.position = wantPosition;
            leftTime = interval;
        }
    }

    public virtual void Spawn(int index)
    {
        Spawn(index, transform.position.GetRandomPosition(minDistance, maxDistance));
    }
}
