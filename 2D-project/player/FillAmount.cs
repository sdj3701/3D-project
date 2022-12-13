using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmount : MonoBehaviour
{
    public Image img_Skill;
    public PlayerBase playerbase;

    // Start is called before the first frame update
    void Start()
    {
        playerbase = FindObjectOfType<PlayerBase>();
    }

    // Update is called once per frame
    void Update()
    {
        img_Skill.fillAmount = (playerbase.cool / 10.0f);
    }


IEnumerator CoolTime ()
    {
        while (playerbase.cool > 1.0f)
        {
            playerbase.cool -= Time.deltaTime;
            img_Skill.fillAmount = (1.0f / playerbase.cool);
            yield return new WaitForFixedUpdate();
        }
    }
}
