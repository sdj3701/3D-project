using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    [Header("경험치")]

    [SerializeField]
    public float expRange;

    [SerializeField]
    protected int _expCurrent; //현재 경험치

    public int ExpCurrent
    {
        get => _expCurrent;
        set
        {
            _expCurrent = value;

            while (_expCurrent >= ExpMax)
            {
                _expCurrent -= ExpMax;

                ++_level;
                if(owner)owner.LevelUp();
            }
        }
    }
    [SerializeField]
    protected int expPerLevel;
    [SerializeField]
    protected int expBase;

    // 기본 경험치 + 성장 경험치
    public int ExpMax => expBase + (_level * expPerLevel);

    //경험치의 비율
    public float ExpRate => _expCurrent / (float)ExpMax; 

    public override void GetExperience(int value)
    {
        ExpCurrent += value;
    }
}
