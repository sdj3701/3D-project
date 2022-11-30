using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRate : MonoBehaviour
{
    protected Image targetImage;
    protected float value;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        targetImage = GetComponent<Image>();
        if(!targetImage) Destroy(this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        targetImage.fillAmount = value;
    }
}
