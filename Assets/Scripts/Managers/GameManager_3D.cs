using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_3D : GameManager
{
    public static int maxWeaponNumber = 5;
    public static int maxPassiveNumber = 5;

    //게임의 남은 시간이예요!
    public static float leftTime = 600;
    public static float totalTime = 600; //30분 60 = 1800

    [Header("총 게임 시간")]
    public float setTime;

    public static float gameProgress => Mathf.Max(1 - (leftTime / totalTime), 0);

//     //조합 정보를 저장!
//     public static List<CombineInfo> combineInfos;

    
//     //무기 정보를 여기에다가 저장해놓도록 할게요!
//     public static Dictionary<string, WeaponInfo> weaponInfos;

//     //패시브 정보를 여기에다가 저장해놓도록 할게요!
//     public static Dictionary<string, PassiveInfo> passiveInfos;

//     //얻을 수 있는 업그레이드 중에 3개를 골라서 여기에 놓을 거예요!
//     public static UpgradeInfo[] selectedUpgrades;

//     //조합하는 대상을 넣어놓는 배열이예요!
//     public static UpgradeInfo[] combineTargets;

//     //조합한 결과물!
//     public static CombineInfo combineResult { get; protected set; }

//     //무기가 맘에 안들면 다시 돌립시다!
//     public static int rerollLeft;

    //게임 시작을 알리기!
    public static bool GameStart = true;

//     public static Addon_Weapon_VS GetPlayerWeapon(int index)
//     {
//         return GetPlayer(index)?.GetAddon<Addon_Weapon_VS>();
//     }

    public override void Update()
    {
        base.Update();
        if (GameStart) leftTime -= Time.deltaTime;
    }

//     //게임매니저를 초기화할 수 있게 합니다!
//     public override void Initialize()
//     {
//         GameStart = true;

//         //유니티에서 받아서
//         totalTime = setTime;
//         //게임 시간 설정!
//         leftTime = totalTime;

//         selectedUpgrades = new UpgradeInfo[maxWeaponNumber];
//         combineTargets = new UpgradeInfo[2];

//         //다섯번의 리롤 기회!
//         rerollLeft = 5;


//         ///////////////////////////////////////////무기//////////////////////////////////////////////
//         if(weaponInfos == null)
//         {
//             weaponInfos = new Dictionary<string, WeaponInfo>();

//             weaponInfos.Add
//             ("FireBreath",
//                 new WeaponInfo
//                 (
//                     typeof(VS_Weapon_FireBreath),
//                     ResourceManager_VampireSurvival.WeaponIcons["FireBreath"],
//                     ResourceManager_VampireSurvival.WeaponPrefs["FireBreath"],
//                     null,
//                     "화염숨결",
//                     "일정 시간마다 불을 뿜어 전방의 적을 공격합니다.",
//                     true
//                 )
//             );
//             weaponInfos.Add
//             ("PowerLaser",
//                 new WeaponInfo
//                 (
//                     typeof(VS_Weapon_PowerLaser),
//                     ResourceManager_VampireSurvival.WeaponIcons["PowerLaser"],
//                     ResourceManager_VampireSurvival.WeaponPrefs["PowerLaser"],
//                     ResourceManager.EffectPrefs["Muzzle_Laser"],
//                     "레이저건",
//                     "클릭한 방향으로 레이저를 발사해 공격합니다.",
//                     true
//                 )
//             );
//             weaponInfos.Add
//             ("RedArrow",
//                 new WeaponInfo
//                 (
//                     typeof(VS_Weapon_RedArrow),
//                     ResourceManager_VampireSurvival.WeaponIcons["RedArrow"],
//                     ResourceManager_VampireSurvival.WeaponPrefs["RedArrow"],
//                     ResourceManager.EffectPrefs["Muzzle_Arrow"],
//                     "붉은 화살",
//                     "일정 시간마다 화살을 위로 발사해 적을 공격합니다.",
//                     true
//                 )
//             );
//             weaponInfos.Add
//             ("SplitGun",
//                 new WeaponInfo
//                 (
//                     typeof(VS_Weapon_SplitGun),
//                     ResourceManager_VampireSurvival.WeaponIcons["SplitGun"],
//                     ResourceManager_VampireSurvival.WeaponPrefs["SplitGun"],
//                     ResourceManager.EffectPrefs["Muzzle_Stone"],
//                     "분열탄",
//                     "클릭한 방향으로 부딪히면 나눠지는 분열탄을 발사합니다.",
//                     true
//                 )
//             );
//             weaponInfos.Add
//             ("Whirl",
//                 new WeaponInfo
//                 (
//                     typeof(VS_Weapon_Whirl),
//                     ResourceManager_VampireSurvival.WeaponIcons["Whirl"],
//                     ResourceManager_VampireSurvival.WeaponPrefs["Whirl"],
//                     null,
//                     "회오리 장벽",
//                     "일정 시간마다 주변의 적을 밀쳐내는 장벽을 만듭니다.",
//                     true
//                 )
//             );
//             weaponInfos.Add
//             ("IceArea",
//                 new WeaponInfo
//                 (
//                     typeof(VS_Weapon_IceArea),
//                     ResourceManager_VampireSurvival.WeaponIcons["IceArea"],
//                     ResourceManager_VampireSurvival.WeaponPrefs["IceArea"],
//                     null,
//                     "빙결 지대",
//                     "지정한 위치에 빙결 지대를 만들어 적을 느리게 합니다."
//                 )
//             );

//             weaponInfos.Add
//             ("FireWhirl",
//                 new WeaponInfo
//                 (
//                     typeof(VS_Weapon_FireWhirl),
//                     ResourceManager_VampireSurvival.WeaponIcons["FireWhirl"],
//                     ResourceManager_VampireSurvival.WeaponPrefs["FireWhirl"],
//                     null,
//                     "화염 회오리",
//                     "플레이어 주위를 돌면서 공격하는 화염을 발사합니다."
//                 )
//             );

//             weaponInfos.Add
//             ("MirrorLaser",
//                 new WeaponInfo
//                 (
//                     typeof(VS_Weapon_MirrorLaser),
//                     ResourceManager_VampireSurvival.WeaponIcons["MirrorLaser"],
//                     ResourceManager_VampireSurvival.WeaponPrefs["MirrorLaser"],
//                     ResourceManager.EffectPrefs["Muzzle_Laser"],
//                     "반사 레이저",
//                     "상대방에게 닿으면 반사를 일으키는 레이저를 만듭니다."
//                 )
//             );

//             weaponInfos.Add
//             ("StraightArrow",
//                 new WeaponInfo
//                 (
//                     typeof(VS_Weapon_StraightArrow),
//                     ResourceManager_VampireSurvival.WeaponIcons["StraightArrow"],
//                     ResourceManager_VampireSurvival.WeaponPrefs["StraightArrow"],
//                     null,
//                     "화살 작렬",
//                     "대상을 향해 빠르게 날아가는 화살을 발사합니다."
//                 )
//             );
//         }

//         ///////////////////////////////////////////무기//////////////////////////////////////////////





//         ///////////////////////////////////////////패시브//////////////////////////////////////////////
//         if(passiveInfos == null)
//         {
//             passiveInfos = new Dictionary<string, PassiveInfo>();

//             SetNewPassive
//                 ("LightStep", 
//                 new VS_Passive_AddBuff(BuffType.MoveSpeed, 0.05f, 0.02f),
//                 ResourceManager_VampireSurvival.PassiveIcons["LightStep"],
//                 "가벼운 걸음",
//                 "발놀림이 가벼워져 속도가 빨라집니다.",
//                 true);

//             SetNewPassive
//                 ("GainExperience",
//                 new VS_Passive_AddBuff(BuffType.GainExperience, 0.25f, 0.25f),
//                 ResourceManager_VampireSurvival.PassiveIcons["GainExperience"],
//                 "노련함",
//                 "경험치 획득 범위가 증가합니다.",
//                 true);
//         }


//         ///////////////////////////////////////////패시브//////////////////////////////////////////////
        

        
        
//         ///////////////////////////////////////////조합//////////////////////////////////////////////
//         if(combineInfos == null)
//         {
//             combineInfos = new List<CombineInfo>();

//             combineInfos.Add
//                 (new CombineInfo(
//                     weaponInfos["FireWhirl"],
//                     weaponInfos["FireBreath"], weaponInfos["Whirl"]
//                 ));

//             combineInfos.Add
//                 (new CombineInfo(
//                     weaponInfos["MirrorLaser"],
//                     weaponInfos["SplitGun"], weaponInfos["PowerLaser"]
//                 ));

//             combineInfos.Add
//                 (new CombineInfo(
//                     weaponInfos["StraightArrow"],
//                     weaponInfos["RedArrow"], weaponInfos["PowerLaser"]
//                 ));
//         }
//         ///////////////////////////////////////////조합//////////////////////////////////////////////

//     }

//     public void SetNewPassive(string name, VS_PassiveBase passive, Sprite icon, string wantName, string context, bool isDropOnLevelUp = false)
//     {
//         passiveInfos.Add(name,
//                 new PassiveInfo(
//                     passive,
//                     icon,
//                     wantName,
//                     context,
//                     isDropOnLevelUp
//                 )
//             );
//         passiveInfos[name].currentPassive.info = passiveInfos[name];
//     }

//     //지금 나온 무기를 다시 되돌려요!
//     public static void ReRoll()
//     {
//         //얻을 수 있는 업글을 싹 다 긁어와봅시다!
//         List<UpgradeInfo> upgradeList = CalculateGettableUpgrade();


//         //무기 3개를 선택해서 유저한테 보여줘야 하기 때문에 3번 돌아줍시다!
//         for (int i = 0; i < selectedUpgrades.Length; i++)
//         {
//             //엇.. 이제 남은 무기가 없는데요 ㅜㅜ
//             if (upgradeList.Count <= 0)
//             {
//                 //그럼 이 칸은 비우고 담으로 넘어가죠!
//                 selectedUpgrades[i] = null;
//                 continue;
//             };

//             //그럼 무기를 뽑아야 하기 때문에 랜덤으로 돌릴 거예요!
//             int currentIndex = Random.Range(0, upgradeList.Count);

//             //랜덤으로 가져왔으니 칸에다가 넣고!
//             selectedUpgrades[i] = upgradeList[currentIndex];
//             //넣었으니까 업글 가능 리스트에선 빼주기!
//             upgradeList.RemoveAt(currentIndex);
//         };
//     }

//     //업그레이드 가능한 모든 리스트 불러오기!
//     public static List<UpgradeInfo> CalculateGettableUpgrade(int playerIndex = 0)
//     {
//         //일단 플레이어를 좀 찾아보고 없으면 나갑시다~
//         CharacterBase currentPlayer = GetPlayer(playerIndex);
//         if (currentPlayer == null) return null;

//         //결과물을 저장할 리스트!
//         List<UpgradeInfo> result = new List<UpgradeInfo>();

//         //대상 플레이어의 뱀파이어 서바이벌 무기를 찾읍시다!
//         Addon_Weapon_VS playerWeapon = currentPlayer.GetAddon<Addon_Weapon_VS>();

//         //없다구요? 그럼 만들어!
//         if (playerWeapon == null)
//         {
//             playerWeapon = new Addon_Weapon_VS(); 
//             playerWeapon.Attach(GetPlayer(playerIndex));
//         };

//         //무기들을 상점 리스트에 담으러 갑시다~
//         foreach(var currentInfo in weaponInfos)
//         {
//             //이거 레벨업할 때 안나온대요!          패스!
//             if (!currentInfo.Value.dropOnLevelUp) continue;

//             //무기를 쭉 둘러보는 중이에요! 이 무기 가지고 있나요?
//             VS_WeaponBase currentWeapon = playerWeapon.GetWeapon(currentInfo.Value.currentWeapon);

//             //                일단 만렙인지 체크해보고!               아닌가..      그럼 무기는 없는데 무기칸 꽉찬 것이 아니라면?
//             if ((currentWeapon != null && !currentWeapon.isMaxLevel) || (currentWeapon == null && playerWeapon.weapons.Count < maxWeaponNumber))
//             {
//                 result.Add(currentInfo.Value);
//             };
//         }

//         //패시브들도 가져다 놓읍시다!
//         foreach (var currentInfo in passiveInfos)
//         {
//             //이거 레벨업할 때 안나온대요!          패스!
//             if (!currentInfo.Value.dropOnLevelUp) continue;

//             //무기를 쭉 둘러보는 중이에요! 이 무기 가지고 있나요?
//             VS_PassiveBase currentPassive = playerWeapon.GetPassive(currentInfo.Value);

//             if ((currentPassive != null && !currentPassive.isMaxLevel) || (currentPassive == null && playerWeapon.passives.Count < maxPassiveNumber))
//             {
//                 //위 조건에 맞으면 추가해요!
//                 result.Add(currentInfo.Value);
//             };
//         }

//         return result;
//     }

//     //원하는 무기를 고르도록 합시다!
//     public static void SelectWeapon(int index)
//     {
//         //무기가 없으면 뭐.. 그냥 넘어가기
//         if (selectedUpgrades[index] == null) return;

//         //이게 무기라면?
//         if(selectedUpgrades[index].IsChildOf<WeaponInfo>() != null)
//         {
//             //플레이어에게 무기를 쥐어주고
//             GetPlayer(0)?.GetAddon<Addon_Weapon_VS>()?.AddWeapon((WeaponInfo)(selectedUpgrades[index]));
//         }
//         else//패시브라면?
//         {
//             //패시브를 넣어주기!
//             GetPlayer(0)?.GetAddon<Addon_Weapon_VS>()?.AddPassive(((PassiveInfo)selectedUpgrades[index]).currentPassive);
//         };

//         //레벨업 창을 없애기!
//         UIManager.Instance.ClaimUI("LevelUp", false);
//     }

//     public static void CombineStart()
//     {
//         //조합을 하려면... 일단 결과물은 알고 있어야겠죠>
//         if(combineResult != null)
//         {
//             Addon_Weapon_VS currentWeapon = GetPlayer(0)?.GetAddon<Addon_Weapon_VS>();

//             //그리고 플레이어도.. 있어야 조합을 하겠죠?
//             if(currentWeapon != null)
//             {
//                 //근데... 최대 레벨이시라구요?                           그럼 안해!
//                 if (combineResult.result.CheckMaxLevel(currentWeapon)) return;

//                 //재료를 다 뽑았으니 결과물을 제공해줍시다!
//                 combineResult.result.AddTo(currentWeapon);

//                 //플레이어를 찾았으니 재료가 될 무기들을 뽑아주기!
//                 foreach (var current in combineResult.ingrediant)
//                 {
//                     current.RemoveFrom(currentWeapon);
//                 };

//                 //조합에 성공하셨으니 이제 레벨업 하면 뜨게 해드리죠!
//                 combineResult.result.dropOnLevelUp = true;

//                 //조합 끝냈으니까! 조합대를 비워주도록 하죠!
//                 for (int i = 0; i < combineTargets.Length; i++)
//                 {
//                     combineTargets[i] = null;
//                 };

//                 ShowEquipPassive.ClaimUpdatePassive(currentWeapon);
//                 ShowEquipWeapon.ClaimUpdateWeapon(currentWeapon);
//                 CombineCheck();
//             };
//         };
//     }

//     public static void CombineCheck()
//     {
//         for(int i = 0; i < combineTargets.Length; i++)
//         {
//             //혹시 조합 대상중에 비어있는 녀석이 있으면 아무 것도 안하게!
//             if (combineTargets[i] == null)
//             {
//                 //조합 결과물은 없습니다!
//                 combineResult = null;
//                 //끗!
//                 return;
//             };
//         };
          
//         //자, 그럼 비어있는 녀석이 없으니까! 조합을 시작해봅시다!
//         foreach(var currentCombine in combineInfos)
//         {
//             //확인해봤더니 이거예요!
//             if(currentCombine.Check(combineTargets))
//             {
//                 //이게 결과물입니다!
//                 combineResult = currentCombine;

//                 //끝!
//                 return;
//             };
//         };

//         //다 확인해봤는데 조합 결과물은 없었습니다..
//         combineResult = null;
//     }

//     public static void CombineRemove(int index)
//     {
//         //배열 안쪽에 있는 내용을 지우고 싶으시다구요?
//         if (index >= 0 && index < combineTargets.Length)
//         {
//             //그러세요!
//             combineTargets[index] = null;
//             //뺐으니까! 결과물은 없습니다!
//             combineResult = null;
//         };

//         //조합한 결과를 확인!
//         CombineCheck();
//     }

//     public static void CombineAdd(UpgradeInfo wantTarget)
//     {
//         //넣으려고 하시는 것이... 이미 있는지 확인해보겠습니다!
//         for(int i = 0; i < combineTargets.Length; i++)
//         {
//             //여기 있네!
//             if (combineTargets[i] == wantTarget)
//             {
//                 //빼고
//                 combineTargets[i] = null;
//                 //조합한 결과를 확인!
//                 CombineCheck();
//                 //끝!
//                 return;
//             };
//         };

//         //그럼.. 이미 있는 건 아니니까 추가는 해볼게요!
//         for (int i = 0; i < combineTargets.Length; i++)
//         {
//             //여기 비었네!
//             if (combineTargets[i] == null)
//             {
//                 //넣어주고!
//                 combineTargets[i] = wantTarget;
//                 //조합한 결과를 확인!
//                 CombineCheck();
//                 //끝!
//                 return;
//             };
//         };
//     }
// }

// public class CombineInfo
// {
//     public UpgradeInfo[] ingrediant;
//     public UpgradeInfo   result;

//     public CombineInfo(UpgradeInfo wantResult, params UpgradeInfo[] ingrediants)
//     {
//         result = wantResult;
//         ingrediant = ingrediants;
//     }

//     public bool Check(params UpgradeInfo[] targets)
//     {
//         for(int i = 0; i < ingrediant.Length; i++)
//         {
//             //조합 재료에 지금 들고 온 재료가 포함되어있는지 확인해봅시다!
//             bool thisTime = false;

//             for(int j = 0; j < targets.Length; j++)
//             {
//                 //오! 여기에 있어요!
//                 if (ingrediant[i] == targets[j])
//                 {
//                     //있다고 표시!
//                     thisTime = true;
//                     break; //ㅇㅋ 확인 끝!
//                 };
//             };

//             //어허.. 조합용 재료에 포함되지 않은 녀석이 있었어요...
//             if (!thisTime) return false;
//         };

//         //모든 것이 조합용 재료에 다 있는 경우에! 맞다고 표시!
//         return true;
//     }
// }

// //업그레이드용 아이템들 아이콘, 이름, 설명을 써두어요!
// public class UpgradeInfo
// {
//     public Sprite icon;
//     public string name;
//     public string context;
//     public bool dropOnLevelUp;

//     public UpgradeInfo(Sprite wantIcon, string wantName, string wantContext, bool droppable)
//     {
//         icon = wantIcon;
//         name = wantName;
//         context = wantContext;
//         dropOnLevelUp = droppable;
//     }

//     public virtual bool CheckMaxLevel(Addon_Weapon_VS target){ return false; }
//     public virtual void AddTo(Addon_Weapon_VS target){}
//     public virtual void RemoveFrom(Addon_Weapon_VS target){}
// }

// //업그레이드 아이템이긴 한데... 무기인 경우에 사용할 것!
// public class WeaponInfo : UpgradeInfo
// {
//     public System.Type currentWeapon;
//     public GameObject shotPrefab;
//     public GameObject muzzlePrefab;

//     public WeaponInfo(System.Type wantWeapon, Sprite wantIcon, GameObject wantShotPrefab, GameObject wantMuzzlePrefab, string wantName, string wantContext, bool droppable = false) : base(wantIcon, wantName, wantContext, droppable)
//     {
//         currentWeapon = wantWeapon;
//         shotPrefab = wantShotPrefab;
//         muzzlePrefab = wantMuzzlePrefab;
//     }

//     public override bool CheckMaxLevel(Addon_Weapon_VS target) 
//     {
//         VS_WeaponBase resultWeapon = target.GetWeapon(currentWeapon);
//         if (resultWeapon == null)   return false;
//         else                        return resultWeapon.isMaxLevel; 
//     }
//     public override void AddTo(Addon_Weapon_VS target) { target.AddWeapon(this); }
//     public override void RemoveFrom(Addon_Weapon_VS target) { target.RemoveWeapon(currentWeapon); }
// }

// //업글레이드는 업그레이드인데, 패시브인 경우에 사용할 것!
// public class PassiveInfo : UpgradeInfo
// {
//     public VS_PassiveBase currentPassive;

//     public PassiveInfo(VS_PassiveBase wantPassive, Sprite wantIcon, string wantName, string wantContext, bool droppable = false) : base(wantIcon, wantName, wantContext, droppable)
//     {
//         currentPassive = wantPassive;
//     }

//     public override bool CheckMaxLevel(Addon_Weapon_VS target)
//     {
//         VS_PassiveBase resultPassive = target.GetPassive(this);
//         if (resultPassive == null) return false;
//         else return resultPassive.isMaxLevel;
//     }
//     public override void AddTo(Addon_Weapon_VS target) { target.AddPassive(currentPassive); }
//     public override void RemoveFrom(Addon_Weapon_VS target) { target.RemovePassive(this); }
}
