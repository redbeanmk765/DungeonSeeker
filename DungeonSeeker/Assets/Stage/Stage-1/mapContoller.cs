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
    // Start is called before the first frame update
    void Start()
    {
        MainCamera.GetComponent<CameraController>().mapCenter = room.MapCenter;
        MainCamera.GetComponent<CameraController>().mapSize = room.MapSize;
        enter =  this.transform.Find("Enter").gameObject;
        exit = this.transform.Find("Exit").gameObject;
        player = GameObject.Find("Player");
        player.transform.position = enter.transform.position - new Vector3(0, -0.4f,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
