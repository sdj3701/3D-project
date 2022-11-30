using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller 
{
   public CharacterBase targetCharacter {get; protected set;} //컨트롤러가 빙의하고 있는 대상

   public Vector3 direction; // 내가 보고싶은 방향 

   public virtual bool Possess(CharacterBase wantCharacter) // 빙의할 타겟을 정함
   {
        if(wantCharacter.currentController != null) return false; //조정하는사람 찾기 있으면 찾기 멈추기

        targetCharacter = wantCharacter; //없으면 캐릭터 등록

        targetCharacter.currentController = this; // 내가 널 조정 하겠다

        targetCharacter.ClaimUpdateForward(Control);

        return true;
   }

   public abstract void Control();
}
