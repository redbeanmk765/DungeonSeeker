using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public GameObject SlimeBall;
    public GameObject BallGenController;
    public int EvenOdd;
    public int state;
    public bool IsSpawn;
    // Start is called before the first frame update
    void Start()
    {
        state = BallGenController.GetComponent<BGcontroller>().state;
        SlimeBall.SetActive(false);
        IsSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        state = BallGenController.GetComponent<BGcontroller>().state;

        if (EvenOdd == state && IsSpawn == false)
        {
            SlimeBall.SetActive(true);
            IsSpawn = true;
        }

        if(state == 0)
        {
            IsSpawn = false;
        }
    }
}
