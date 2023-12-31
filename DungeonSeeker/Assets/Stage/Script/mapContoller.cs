using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapContoller : MonoBehaviour
{
    public Room room;
    public Camera MainCamera;
    public GameObject enter;
    public GameObject exit;
    public GameObject player;
    public GameObject playerUI;
    public bool IsCity;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = GameObject.Find("Grid").transform;
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        MainCamera.GetComponent<CameraController>().mapCenter = room.MapCenter;
        MainCamera.GetComponent<CameraController>().mapSize = room.MapSize;
        enter =  this.transform.Find("Enter").gameObject;
        exit = this.transform.Find("Exit").gameObject;
        player = GameObject.Find("Player");
        player.transform.position = enter.transform.position - new Vector3(0, 0.4f,0);
        player.GetComponent<PlayerStat>().IsSafeZone = true;
        playerUI = GameObject.Find("PlayerUI");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.Find("Enemy").gameObject.transform.childCount == 0)
        {
            exit.GetComponent<door>().Open();

        }

        if(IsCity == true)
        {
            playerUI.GetComponent<CoinController>().state = 2;
        }
        else
        {
            playerUI.GetComponent<CoinController>().state = 1;
        }
    }

    public void BossCameraOn()
    {
        MainCamera.GetComponent<CameraController>().mapCenter = room.BossMapCenter;
        MainCamera.GetComponent<CameraController>().mapSize = room.BossMapSize;
    }

    public void BossCameraOff()
    {
        MainCamera.GetComponent<CameraController>().mapCenter = room.MapCenter;
        MainCamera.GetComponent<CameraController>().mapSize = room.MapSize;
    }

    public void BossFixCameraOn()
    {
        MainCamera.GetComponent<CameraController>().mapCenter = room.BossMapCenter;
        MainCamera.GetComponent<CameraController>().bossCameraSize = room.BossCameraSize;
        MainCamera.GetComponent<CameraController>().IsFix = true;
        MainCamera.GetComponent<CameraController>().IsStop = true;
    }

    public void BossFixCameraOff()
    {
        MainCamera.GetComponent<CameraController>().mapCenter = room.MapCenter;
        MainCamera.GetComponent<CameraController>().IsFix = false;
        MainCamera.GetComponent<CameraController>().IsStop = false;
    }
}
