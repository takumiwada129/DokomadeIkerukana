using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI fadealphaText;
    private float alpha;
    private bool fadein;
    private bool fadeout;

    // Start is called before the first frame update
    void Start()
    {
        fadealphaText.text = "タッチでスタート!!";
        fadein = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadein == true)
        {
            FadeIn();
        }
        if (fadeout == true)
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        alpha -= 0.01f;
        fadealphaText.color = new Color(0, 0, 0, alpha);
        if (alpha <= 0)
        {
            fadein = false;
            fadeout = true;
        }
    }

    void FadeOut()
    {
        alpha += 0.01f;
        fadealphaText.color = new Color(0, 0, 0, alpha);
        if (alpha >= 1)
        {
            fadein = true;
            fadeout = false;
        }
    }
}