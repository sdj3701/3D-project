using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_3D : UIManager
{
    [SerializeField] GameObject LevelUpUI;

    //레벨업 무시
    public void IgnoreLevelUp() 
    {
        ClaimUI("LevelUP", false);
    }

    //UI를 요청을 받음
    public override void ClaimUI(string name, bool open)
    {
        // 하려던 건 그대로 하고
        base.ClaimUI(name, open);

        //지금 보고 싶어하는 UI가 뭔가요?
        switch (currentFocusUI)
        {
            //래밸 업 이라면
            case "LevelUP";

            //래밸옵 UI를 켜줍시다
            LevelUpUI.SetActive(open);

            //ShowEquipPassive.ClaimUpdatePassive(GameManager.GetPlayer(0)?.GetAddon<Addon_Weapon_VS>());

            //레벨업 UI를 켜는 거면 게임을 멈추고 , 끄는 거면 다시 실행시키기
            GameManager.Pause = open;
            break;
        }
    }
}
