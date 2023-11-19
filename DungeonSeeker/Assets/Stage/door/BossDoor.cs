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
    public bool IsFix;
    public bool IsFixCall;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.Find("Enemy");
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
        IsFixCall = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStat.IsSafeZone == false)
        {
            Door.SetActive(true);
            bossHpBar.SetTrue();
            if (IsFix == false)
            {
                mapCon.BossCameraOn();
            }
            else
            {
                if (IsFixCall == false) 
                { 
                    mapCon.BossFixCameraOn();
                }
                IsFixCall = true;

            }

        }

        if(Enemy.transform.childCount == 0)
        {
            bossHpBar.SetFalse();
            
            if (IsFix == false)
            {
                mapCon.BossCameraOff();
            }
            else
            {
                mapCon.BossFixCameraOff();
            }
            Destroy(this.gameObject);

        }
    }
}
