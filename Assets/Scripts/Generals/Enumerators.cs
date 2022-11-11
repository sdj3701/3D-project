//현재 캐릭터의 ApperanceType이 어떤 상태인지 확인
public enum AppearanceType
{
    Sprite, //그림

    Mesh //모양
}

public enum AddonType
{
    CollectExp,
    LevelUp
}

public enum ControllerType
{
    None, // 멍때림
    LocalPlayer, // 이 컴퓨터에서 플레이하고 있는 플레이어
    AI_FollowPlayer // 그냥 캐릭터를 따라옴
}