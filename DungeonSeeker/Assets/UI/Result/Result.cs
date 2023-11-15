using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Text killScore;
    public Text stage;
    public Text gold;
    public Text plat;
    public PlayerStat playerStat;

    // Start is called before the first frame update
    void Start()
    {
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
        if (playerStat != null && GameObject.Find("StageController").GetComponent<StageController>().curRoom != null)
        {
            killScore.text = ": " + playerStat.killScore.ToString();
            stage.text = ": " + GameObject.Find("StageController").GetComponent<StageController>().curRoom.GetComponent<mapContoller>().room.Name;
            gold.text = ": " + playerStat.totalGold.ToString();
            plat.text = ": " + (playerStat.totalGold * 0.5f).ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
