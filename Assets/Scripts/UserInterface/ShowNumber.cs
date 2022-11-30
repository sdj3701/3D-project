using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowNumber : MonoBehaviour
{
    protected Text target;
    [SerializeField]
    protected float value;

    protected string wantText;

    protected virtual void Start()
    {
        target = GetComponent<Text>();
        if(!target) Destroy(this);
        else wantText = $"{value}";
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        target.text = wantText;
    }
}
