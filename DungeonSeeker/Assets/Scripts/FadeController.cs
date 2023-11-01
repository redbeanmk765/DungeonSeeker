using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public GameObject stageController;
    public GameObject player;
    public Image panel;
    float time = 0f;
    float fadeTime = 1f;

    void Start()
    {
        stageController = GameObject.Find("StageController");
        player = GameObject.Find("Player");
    }
    public void Fade()
    {
        StartCoroutine(FadeCor());
    }

    IEnumerator FadeCor()
    {
        panel.gameObject.SetActive(true);
        player.GetComponent<MoveController>().IsFade = true;
        Color color = panel.color;
        color.a = 0;
        time = 0f;
        while (color.a < 1f)
        {
            time +=  2 * Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(0, 1, time);
            panel.color = color;
            yield return 0;
        }
        stageController.GetComponent<StageController>().curRoom.SetActive(false);
        stageController.GetComponent<StageController>().nextRoom.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        time = 0f;

        while (color.a > 0f)
        {
           
            time += Time.deltaTime / fadeTime;
            color.a = Mathf.Lerp(1, 0, time);
            panel.color = color;
            yield return 0;
        }
        player.GetComponent<MoveController>().IsFade = false;
        panel.gameObject.SetActive(false);
        yield return 0;
    }
}