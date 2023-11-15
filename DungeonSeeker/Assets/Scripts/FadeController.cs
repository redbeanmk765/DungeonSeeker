using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public GameObject stageController;
    public GameObject player;
    public GameObject nextRoom;
    public Image panel;
    public Image theWorldpanel;
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

    public void Retry()
    {
        StartCoroutine(RetryCor());
    }

    public void TheWorldOn()
    {
        theWorldpanel.gameObject.SetActive(true);
    }

    public void TheWorldOff()
    {
       
        theWorldpanel.gameObject.SetActive(false);
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
            time +=  2 * Time.unscaledDeltaTime / fadeTime;
            color.a = Mathf.Lerp(0, 1, time);
            panel.color = color;
            yield return 0;
        }
        DestroyImmediate(stageController.GetComponent<StageController>().curRoom,true);
        nextRoom = Instantiate(stageController.GetComponent<StageController>().nextRoom);
        stageController.GetComponent<StageController>().curRoom = nextRoom;

        yield return new WaitForSecondsRealtime(0.5f);
        time = 0f;

        while (color.a > 0f)
        {
           
            time += Time.unscaledDeltaTime / fadeTime;
            color.a = Mathf.Lerp(1, 0, time);
            panel.color = color;
            yield return 0;
        }
        player.GetComponent<MoveController>().IsFade = false;
        stageController.GetComponent<StageController>().RoomShift();
        panel.gameObject.SetActive(false);
        yield return 0;
    }

    IEnumerator RetryCor()
    {
        panel.gameObject.SetActive(true);
        player.GetComponent<MoveController>().IsFade = true;
        Color color = panel.color;
        color.a = 0;
        time = 0f;
        while (color.a < 1f)
        {
            time += 2 * Time.unscaledDeltaTime / fadeTime;
            color.a = Mathf.Lerp(0, 1, time);
            panel.color = color;
            yield return 0;
        }
        DestroyImmediate(stageController.GetComponent<StageController>().curRoom, true);
        stageController.GetComponent<StageController>().count = 1;
        nextRoom = Instantiate(stageController.GetComponent<StageController>().stage.roomList[0]);
        stageController.GetComponent<StageController>().nextRoom = stageController.GetComponent<StageController>().stage.roomList[1];
        GetComponent<CameraController>().Retry();
        yield return new WaitForSecondsRealtime(0.5f);
        time = 0f;

        while (color.a > 0f)
        {

            time += Time.unscaledDeltaTime / fadeTime;
            color.a = Mathf.Lerp(1, 0, time);
            panel.color = color;
            yield return 0;
        }
        player.GetComponent<MoveController>().IsFade = false;
        panel.gameObject.SetActive(false);
        yield return 0;
    }


}
