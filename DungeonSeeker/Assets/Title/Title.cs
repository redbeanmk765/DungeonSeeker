using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Enter"))
        {
            SceneManager.LoadScene("InGame");
            Debug.Log("enter");
        }
    }
}
