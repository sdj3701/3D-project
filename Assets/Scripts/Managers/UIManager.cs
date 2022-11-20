using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    protected static UIManager _instance;
    public static UIManager Instance => _instance;

    protected static Canvas _mainCanvas = null;
    public static Canvas MainCanvas
    {
        get
        {
            if (!_mainCanvas)
            {
                //              태그로 찾아줘!                            있으면      캔버스 가져와봐!
                _mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas")?.GetComponent<Canvas>();

                //그래도 없는데?
                if (!_mainCanvas)
                {
                    //                                     그럼 리소스 폴더에서 가져오자!                          가져왔으면  캔버스도 내놔!
                    _mainCanvas = Instantiate(Resources.Load<GameObject>("Prefabs/UserInterface/MainUserInterface"))?.GetComponent<Canvas>();
                };
            };
            return _mainCanvas;
        }
    }

    protected static RectTransform objectRightBottom = null;
    protected static RectTransform objectLeftTop = null;

    public static string currentFocusUI;

    public static Vector3 screenRightBottom 
    {
        get
        {
            //Rect Transform은 일반적인 위치가 아니라 "앵커"를 두는 녀석이었습니다!
            //기준점을 중심으로 하는 위치도 있습니다!  anchoredPosition!
            if (!objectRightBottom)
            {
                //                     오브젝트 새로 만들어서         RectTransform을 넣어주기!
                objectRightBottom = new GameObject("RightBottom").AddComponent<RectTransform>();

                //UI요소는 캔버스가 부모로 있어야 제대로 작동하기 때문에 메인 캔버스를 준비해주기!
                //게임오브젝트의 부모자식은 Transform으로 연결됩니다!
                objectRightBottom.SetParent(MainCanvas.transform);

                //앵커를 오른쪽 아래로!
                objectRightBottom.anchorMin = new Vector2(1, 0);
                objectRightBottom.anchorMax = new Vector2(1, 0);

                //영점 맞추기!
                objectRightBottom.anchoredPosition = Vector3.zero;
            };

            //이녀석의 실제 위치가 화면의 어디 부분에 해당하는지!
            return objectRightBottom.position;//Camera.main.WorldToScreenPoint(objectRightBottom.position);
        }
    }
    public static Vector3 screenLeftTop 
    {
        get
        {
            //Rect Transform은 일반적인 위치가 아니라 "앵커"를 두는 녀석이었습니다!
            //기준점을 중심으로 하는 위치도 있습니다!  anchoredPosition!
            if (!objectLeftTop)
            {
                //                     오브젝트 새로 만들어서         RectTransform을 넣어주기!
                objectLeftTop = new GameObject("LeftTop").AddComponent<RectTransform>();

                //UI요소는 캔버스가 부모로 있어야 제대로 작동하기 때문에 메인 캔버스를 준비해주기!
                //게임오브젝트의 부모자식은 Transform으로 연결됩니다!
                objectLeftTop.SetParent(MainCanvas.transform);


                //앵커를 오른쪽 아래로!
                objectLeftTop.anchorMin = new Vector2(0, 1);
                objectLeftTop.anchorMax = new Vector2(0, 1);

                //영점 맞추기!
                objectLeftTop.anchoredPosition = Vector3.zero;
            };

            //이녀석의 실제 위치가 화면의 어디 부분에 해당하는지!
            return objectLeftTop.position;//Camera.main.WorldToScreenPoint(objectLeftTop.position);
        }
    }

    void Awake()
    {
        this.Singleton(ref _instance);
    }

    public void ClaimUI(string name){ ClaimUI(name, true); }
    public virtual void ClaimUI(string name, bool open){ currentFocusUI = name; }
}