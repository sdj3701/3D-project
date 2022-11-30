using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerExp : ShowRate
{
    public int index;
    protected PlayerStatus stat;

    protected override void Update()
    {
        if(!stat)
        {
            
            stat = (PlayerStatus)(GameManager.GetPlayer(index)?.Stat);

            if(!stat) return;
        }

        value = stat.ExpRate;
        base.Update();
    }
}
