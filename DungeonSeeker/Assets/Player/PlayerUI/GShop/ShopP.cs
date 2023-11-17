using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopP : MonoBehaviour
{
    public RectTransform Content;
    public RectTransform Select;
    public float count;
    public float conCount;
    public GameObject shop;
    public float max;
    public List<float> cost;
    public List<float> level;
    public List<float> levelMax;
    public ShopSound SS;

    // Start is called before the first frame update
    void Start()
    {
        Content.anchoredPosition = new Vector2(0,0);
        Select.anchoredPosition = new Vector2(40, -320);
        count = 1;
        max = 10;
        cost = new List<float> { 0,0,0,0,0,0,0,0,0,0 };
        level = new List<float> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        CostCalc();
        Level();
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
            SS.Move();
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
                if (DataController.Instance.data.PlayerPlat >= cost[0] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[0];
                    DataController.Instance.data.maxHpPer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

            case 2:
                if (DataController.Instance.data.PlayerPlat >= cost[1] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[1];
                    DataController.Instance.data.defPer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

            case 3:
                if (DataController.Instance.data.PlayerPlat >= cost[2] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[2];
                    DataController.Instance.data.hitBoxPer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

            case 4:
                if (DataController.Instance.data.PlayerPlat >= cost[3] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[3];
                    DataController.Instance.data.dashCoolTimePer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

            case 5:
                if (DataController.Instance.data.PlayerPlat >= cost[4] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[4];
                    DataController.Instance.data.AttackCoolTimePer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

            case 6:
                if (DataController.Instance.data.PlayerPlat >= cost[5] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[5];
                    DataController.Instance.data.DmgPer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

            case 7:
                if (DataController.Instance.data.PlayerPlat >= cost[6] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[6];
                    DataController.Instance.data.skilldurationPer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

            case 8:
                if (DataController.Instance.data.PlayerPlat >= cost[7] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[7];
                    DataController.Instance.data.skillCoolTimePer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

            case 9:
                if (DataController.Instance.data.PlayerPlat >= cost[8] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[8];
                    DataController.Instance.data.AirJumpCountMaxPer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

            case 10:
                if (DataController.Instance.data.PlayerPlat >= cost[9] && level[i] < levelMax[i])
                {
                    DataController.Instance.data.PlayerPlat -= cost[9];
                    DataController.Instance.data.HpPotionMaxPer += 1;
                    SS.Sucess();
                }
                else
                {
                    SS.Fail();
                }
                break;

        }
    }

    public void CostCalc()
    {
        cost[0] = 50 + ( 50 * DataController.Instance.data.maxHpPer);
        cost[1] = 100 + ( 100 * DataController.Instance.data.defPer );
        cost[2] = 100 + (100 * DataController.Instance.data.hitBoxPer);
        cost[3] = 100 + (100 * DataController.Instance.data.dashCoolTimePer);
        cost[4] = 100 + (100 * DataController.Instance.data.AttackCoolTimePer);
        cost[5] = 50 + (50 * DataController.Instance.data.DmgPer);
        cost[6] = 50 + (50 * DataController.Instance.data.skilldurationPer);
        cost[7] = 50 + (50 * DataController.Instance.data.skillCoolTimePer);
        cost[8] = 200;
        cost[9] = 50 + (50 * DataController.Instance.data.HpPotionMaxPer);
    }

    public void Level()
    {
        level[0] = DataController.Instance.data.maxHpPer;

        level[1] = DataController.Instance.data.defPer;

        level[2] = DataController.Instance.data.hitBoxPer;

        level[3] = DataController.Instance.data.dashCoolTimePer;

        level[4] = DataController.Instance.data.AttackCoolTimePer;

        level[5] = DataController.Instance.data.DmgPer;

        level[6] = DataController.Instance.data.skilldurationPer;

        level[7] = DataController.Instance.data.skillCoolTimePer;

        level[8] = DataController.Instance.data.AirJumpCountMaxPer;

        level[9] = DataController.Instance.data.HpPotionMaxPer;
    }
}
