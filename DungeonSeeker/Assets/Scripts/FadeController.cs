using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Image panel;
    float time = 0f;
    float fadeTime = 1f;
    public void Fade()
    {
        StartCoroutine(FadeCor());
    }

    IEnumerator FadeCor()
    {
        panel.gameObject.SetActive(true);
        Color color = panel.color;
        while (color.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(0, 1, time);
            panel.color = color;
            yield return 0;
        }

        yield return new WaitForSeconds(1f);
        while (color.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(1, 0, time);
            panel.color = color;
            yield return 0;
        }

        panel.gameObject.SetActive(true);
        yield return 0;
    }
}
