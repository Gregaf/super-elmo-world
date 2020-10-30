using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public float fadeTime;

    Image imageToFade;

    private Color tempColor;

    private void Start()
    {
        imageToFade = this.GetComponent<Image>();

        tempColor = imageToFade.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            FadeIn();
        else if (Input.GetKeyDown(KeyCode.O))
            FadeOut();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0));
    }

    IEnumerator Fade(float start, float end)
    {
        float timer = 0;

        while (!Mathf.Approximately(tempColor.a, end))
        {
            timer += Time.deltaTime;

            tempColor.a = Mathf.Lerp(start, end, timer / fadeTime);
            imageToFade.color = tempColor;
            
            yield return null;
        }


        yield return null;
    }


}
