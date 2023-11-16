using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{

    
    void Start()
    {
        DataController.Instance.LoadGameData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        DataController.Instance.SaveGameData();
    }
}
