using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerLevel : ShowNumber
{
    public int index;

    protected override void Start()
    {
        base.Start();
        wantText = $"LV.{value}";
    }

    protected override void Update()
    {
        PlayerStatus player = (PlayerStatus)GameManager.GetPlayer(index)?.Stat; //있을 때만

        value = player? player.Level : 0;

        wantText = $"LV.{value}";

        base.Update();
    }
}
