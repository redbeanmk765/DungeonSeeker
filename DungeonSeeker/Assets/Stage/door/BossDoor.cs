using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public PlayerStat playerStat;
    public GameObject Enemy;
    public GameObject Door;
    public BossHpBar bossHpBar;
    public mapContoller mapCon;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.Find("Enemy");
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStat.IsSafeZone == false)
        {
            Door.SetActive(true);
            bossHpBar.SetTrue();
            mapCon.BossCameraOn();

        }

        if(Enemy.transform.childCount == 0)
        {
            Destroy(this.gameObject);
            mapCon.BossCameraOff();
        }
    }
}
