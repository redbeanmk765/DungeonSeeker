using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageController : MonoBehaviour
{
    public Stage stage1;
    public GameObject curRoom;
    public GameObject nextRoom;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
       mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GoNextRoom()
    {
        mainCamera.GetComponent<FadeController>().Fade();
       // curRoom.SetActive(false);
       // nextRoom.SetActive(true);
       
        return;
    }
}
