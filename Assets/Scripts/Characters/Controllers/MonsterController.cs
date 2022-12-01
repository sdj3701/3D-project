using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : Controller
{
    float horizontalAngle; // 수평
    float verticalAngle; // 수직
    
    // 마우스 고정
    public static bool mouseFix 
    {
        get
        {
            return Cursor.lockState == CursorLockMode.Locked; // 잠긴 상태
        }

        set
        {
            if(value)
            {
                Cursor.lockState = CursorLockMode.Locked; //화면 고정
                Cursor.visible = false; // 마우스 안보이게
            }
            else
            {
                Cursor.lockState = CursorLockMode.None; //고정 해제
                Cursor.visible = true; //마우스 다시 보이게
            }
        }
        
    }

    public void SetAngles(float hor,float ver)
    {
        direction = Extensions.ToDirection(hor,ver);

        horizontalAngle = hor;
        verticalAngle = ver;
    }

    public override void Control()
    {
        if(Time.timeScale <= 0 ) return; //시간이 멈춰

        CharacterBase target = GameManager.GetPlayer(0);
        if(target && (targetCharacter.transform.position - target.transform.position).magnitude < 2) //거리가 1정도면 될까?
        {
            targetCharacter.Attack(target);
        }
        

        if(mouseFix && InputManager.mouseChangePosition.magnitude > 0) //Inputmanager을 사용하기 위해서는 GameManager를 생성하여 사용해야함
        {
            horizontalAngle -= InputManager.mouseChangePosition.x * InputManager.mouseSensitive;
            verticalAngle += InputManager.mouseChangePosition.y * InputManager.mouseSensitive;

            verticalAngle = Mathf.Clamp(verticalAngle, -80.0f, 80.0f);

            SetAngles(horizontalAngle,verticalAngle);
        }

        Vector3 moveDir = new Vector3();

        // moveDir += horizontalAngle.ToDirection() * InputManager.moveDirection.y; //가려는 수평 방향 방향키 누른 위치
        // moveDir += (horizontalAngle - 90.0f).ToDirection() * InputManager.moveDirection.x; // 가려는 오른쪽 방향

        // moveDir.z = moveDir.y; //y와 z를 바꾸기 y는 3d에서는 점프 이기 때문에
        // moveDir.y = 0;
        moveDir = targetCharacter.transform.position; //플레이어를 따라 가게 했음

        targetCharacter.MoveTo(moveDir);
    }
}
