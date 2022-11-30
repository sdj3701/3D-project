using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerHealth : ShowHealth
{
    public int playerIndex;

    protected override void Update()
    {
        if(!stat)
        {
            CharacterBase target = GameManager.GetPlayer(playerIndex);

            if(target)
            {
                stat = target.Stat;
            }
        }
        base.Update();
    }
}
