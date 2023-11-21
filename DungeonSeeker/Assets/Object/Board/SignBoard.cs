using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBoard : MonoBehaviour
{
    public GameObject keyUI;
    public Sprite full;
    public Sprite empty;
    public bool IsNear;
    public bool IsInter;
    public PlayerStat playerStat;
    public CameraController MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        IsInter = false;
        IsNear = false;
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
        MainCamera = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && IsNear == true && IsInter == false)
        {
            DataController.Instance.data.PlayerPlat += playerStat.totalGold * 0.5f;
            MainCamera.IsClear = true;
            MainCamera.ResultCall();
            IsInter = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            keyUI.SetActive(true);
            IsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            keyUI.SetActive(false);
            IsNear = false;
        }
    }
}
