using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MouseCode
{
    LeftClick, //0
    RightClick, //1
    WheelClick, //2
    ThumbDown, //3
    ThumbUp, //4

    Length
}

public enum KeyState
{
    Off,
    Down,
    On,
    Up
}


public class InputManager : MonoBehaviour
{
    private static KeyState[] mouseState = new KeyState[(int)MouseCode.Length]; //마우스에 있는 키 개수만큼 배열을 만들어줌

    public static GameObject mouseHitInterFace {get; protected set;}//마우스가 올라가있는 UI
    public static Vector2 mouseChangePosition   {get; protected set;} //마우스의 위치 변화량
    public static Vector3 mouseHitPosition {get; protected set;} //마우스가 부딪힌 위치가 어디인지
    
    public static Vector3 moveDirection         {get; protected set;} //움직이는 방향
    public static float moveMagnitude         {get; protected set;} //움직이는 양

    public static float mouseSensitive = 2.7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentMove = Vector3.zero;

        //방향키 입력
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))      currentMove += Vector3.left;
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))      currentMove += Vector3.down;
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))     currentMove += Vector3.right;
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))        currentMove += Vector3.up;

        currentMove.Normalize(); //움직임을 1로 조정하여 빨라 지지 않도록 조정 대각선 때문에 사용

        if(currentMove.magnitude <= 0) //이동속도가 0 이면 멈춤
        {
            moveDirection = Vector3.zero;
        }
        else
        {
            moveDirection = currentMove;
        }

        moveMagnitude = currentMove.magnitude; //움직임의 크기 크기는 1보다 커질 수 있음

        if(moveMagnitude > 1)
        {
            moveDirection = currentMove.normalized; //너무 빠르면 속도 제한
        }
        else
        {
            moveDirection = currentMove;
        }
        
        mouseChangePosition = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // 마우스는 2D로 움직이기 때문 x,y를 사용

        for(int i = 0; i < mouseState.Length; i++)
        {
            if(Input.GetMouseButtonDown(i)) mouseState[i] = KeyState.Down;
            else if(Input.GetMouseButtonUp(i)) mouseState[i] = KeyState.Up;
            else if(Input.GetMouseButton(i)) mouseState[i] = KeyState.On;
            else mouseState[i] = KeyState.Off;
        }

    }

    public static KeyState GetMouseState(MouseCode target)
    {
        return mouseState[(int)target]; //마우스 배열을 불러옴
    }
}
