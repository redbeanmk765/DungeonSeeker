using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageController : MonoBehaviour
{
    public Stage stage;
    public GameObject curRoom;
    public GameObject nextRoom;
    public GameObject mainCamera;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        curRoom = stage.roomList[0];
        nextRoom = stage.roomList[1];
        count = 1;
        curRoom = Instantiate(curRoom);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GoNextRoom()
    {
        mainCamera.GetComponent<FadeController>().Fade();   
       
        return;
    }

    public void RoomShift()
    {
        count++;
        if (count >= stage.roomList.Count - 1)
        {
            count = stage.roomList.Count - 1;
        }
        
        nextRoom = stage.roomList[count];
        

        if (count >= stage.roomList.Count - 1)
        {
            count = stage.roomList.Count - 1;
        }
        Debug.Log(count);
        
        Debug.Log(stage.roomList.Count);
        return;
    }
}
