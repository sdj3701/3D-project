using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get // 인스턴스 찾기
        {
            if(_instance == null) //없으면
            {
                GameObject managerObject = GameObject.Find("GameManager"); // 게임매니저라고 하는애를 찾습니다.

                if(managerObject == null) // 오브젝트는 있니?
                {
                    managerObject = new GameObject("GameManager"); //둘다 없으면 메니저랑 오브젝트까지 생성
                }
                _instance = managerObject.AddComponent<GameManager>();//오브젝트에 컴포넌트 추가
            }
            return _instance;
        }
    }

    public List<CharacterBase> player = new List<CharacterBase>();

    static int pauserClaim = 0;

    public static bool Pause
    {
        get => pauserClaim > 0; //멈추는 애가 있는지 확인
        set => pauserClaim = Mathf.Max(value ? pauserClaim + 1 : pauserClaim - 1 , 0); 
    }

    public static void ForcePause(bool value) //강제로 게임을 멈추거나 실행시키는 함수
    {
        pauserClaim = value ? Mathf.Max(1, pauserClaim) : 0 ; //멈추고 싶으면 최소 1로 고정
    }

    void OnEnable() 
    {
        this.Singleton(ref _instance);
        Initialize();
    }

    public virtual void Update()
    {
        Time.timeScale = Pause ? 0 : worldTimeScale;
    }

    public static float worldTimeScale = 1.0f;

    public static CharacterBase FindNearestPlayer(Vector3 position)
    {
        if(Instance.player.Count <= 0) return null;

        if(Instance.player.Count == 1) return Instance.player[0];

        return Instance.player.GetNearset(position, 100000);
            
    }

    public static void AddPlayer(CharacterBase target)
    {
        Instance.player.Add(target);
    }

    public static CharacterBase GetPlayer(int index)
    {
        if(index < Instance.player.Count && index >=0 ) return Instance.player[index];

        return null;
    }

    public virtual void Initialize(){ }

}
