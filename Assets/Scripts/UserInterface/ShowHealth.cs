using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShowHealth : ShowRate
{
    protected CharacterStatus stat;
    
    protected override void Update()
    {
        if(stat)
        {
            value = stat.healthRate;
        }
        else
        {
            value = 0;
        }
        base.Update();
    }
}
