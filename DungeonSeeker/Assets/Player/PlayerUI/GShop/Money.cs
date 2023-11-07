using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = GameObject.Find("Player").gameObject.GetComponent<PlayerStat>().PlayerGold.ToString();
    }
}
