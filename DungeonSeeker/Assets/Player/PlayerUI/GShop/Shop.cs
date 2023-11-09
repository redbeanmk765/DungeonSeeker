using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform Content;
    public RectTransform Select;
    public float count;
    public float conCount;
    public GameObject shop;
    public float max;
    public PlayerStat playerStat;
    public List<float> cost;
    public List<float> level;
    public List<float> levelMax;

    // Start is called before the first frame update
    void Start()
    {
        Content.anchoredPosition = new Vector2(0,0);
        Select.anchoredPosition = new Vector2(40, -320);
        count = 1;
        max = 10;
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
        cost = new List<float> { 0,0,0,0,0,0,0,0,0,0 };
        level = new List<float> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        CostCalc();
        levelMax = new List<float> {999,3,3,3,2,999,6,3,1,5 };

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            shop.SetActive(false);
            GameObject.Find("Player").GetComponent<MoveController>().IsFade = false;
        }


        if(Input.GetButtonDown("Vertical")){
            float ver = Input.GetAxisRaw("Vertical");
            count += -ver;
            if (count <= 0)
            {
                count = max;
            }
            else if(count > max)
            {
                count = 1;
            }

            conCount = count - 2;

            if (conCount < 0)
            {
                conCount = 0;
            }
            else if (conCount >= max - 2)
            {
                conCount = max - 3;
            }
        }

        Select.anchoredPosition = new Vector2(40, -20 - (300 * count));
        Content.anchoredPosition = new Vector2(0, 300 * conCount);

        if (Input.GetButtonDown("Interaction"))
        {
            Pray();
            CostCalc();
            Level();
        }
    }

    public void Pray()
    {
        int i = (int)(count - 1);
        switch (count) {
           

            case 1:
                if (playerStat.PlayerGold >= cost[0] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[0];
                    playerStat.maxHpTmp += 1;
                }
                break;

            case 2:
                if (playerStat.PlayerGold >= cost[1] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[1];
                    playerStat.defTmp += 1;
                }
                break;

            case 3:
                if (playerStat.PlayerGold >= cost[2] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[2];
                    playerStat.hitBoxTmp += 1;
                }
                break;

            case 4:
                if (playerStat.PlayerGold >= cost[3] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[3];
                    playerStat.dashCoolTimeTmp += 1;
                }
                break;

            case 5:
                if (playerStat.PlayerGold >= cost[4] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[4];
                    playerStat.AttackCoolTimeTmp += 1;
                }
                break;

            case 6:
                if (playerStat.PlayerGold >= cost[5] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[5];
                    playerStat.DmgTmp += 1;
                }
                break;

            case 7:
                if (playerStat.PlayerGold >= cost[6] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[6];
                    playerStat.skilldurationTmp += 1;
                }
                break;

            case 8:
                if (playerStat.PlayerGold >= cost[7] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[7];
                    playerStat.skillCoolTimeTmp += 1;
                }
                break;

            case 9:
                if (playerStat.PlayerGold >= cost[8] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[8];
                    playerStat.AirJumpCountMaxTmp += 1;
                }
                break;

            case 10:
                if (playerStat.PlayerGold >= cost[9] && level[i] < levelMax[i])
                {
                    playerStat.PlayerGold -= cost[9];
                    playerStat.HpPotionMaxTmp += 1;
                }
                break;

        }
    }

    public void CostCalc()
    {
        cost[0] = 50 + ( 50 * playerStat.maxHpTmp );
        cost[1] = 100 + ( 100 * playerStat.defTmp );
        cost[2] = 100 + (100 * playerStat.hitBoxTmp);
        cost[3] = 100 + (100 * playerStat.dashCoolTimeTmp);
        cost[4] = 100 + (100 * playerStat.AttackCoolTimeTmp);
        cost[5] = 50 + (50 * playerStat.DmgTmp);
        cost[6] = 50 + (50 * playerStat.skilldurationTmp);
        cost[7] = 50 + (50 * playerStat.skillCoolTimeTmp);
        cost[8] = 200;
        cost[9] = 50 + (50 * playerStat.HpPotionMaxTmp);
    }

    public void Level()
    {
        level[0] = playerStat.maxHpTmp;

        level[1] = playerStat.defTmp;

        level[2] = playerStat.hitBoxTmp;

        level[3] = playerStat.dashCoolTimeTmp;

        level[4] = playerStat.AttackCoolTimeTmp;

        level[5] = playerStat.DmgTmp;

        level[6] = playerStat.skilldurationTmp;

        level[7] = playerStat.skillCoolTimeTmp;

        level[8] = playerStat.AirJumpCountMaxTmp;

        level[9] = playerStat.HpPotionMaxTmp;
    }
}
