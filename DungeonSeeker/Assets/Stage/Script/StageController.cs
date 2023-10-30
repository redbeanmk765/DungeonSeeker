using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public Stage stage1;
    public GameObject curRoom;
    public GameObject nextRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoNextRoom()
    {
        curRoom.SetActive(false);
        nextRoom.SetActive(true);
        
        return;
    }
}
