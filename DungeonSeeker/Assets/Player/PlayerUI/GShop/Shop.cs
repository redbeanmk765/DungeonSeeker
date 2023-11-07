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
    public List<int> cost;
    // Start is called before the first frame update
    void Start()
    {
        Content.anchoredPosition = new Vector2(0,0);
        Select.anchoredPosition = new Vector2(40, -320);
        count = 1;
        max = 6;
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
        cost = new List<int> {30, 30, 100, };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            shop.SetActive(false);
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
        }
    }

    public void Pray()
    {
        switch (count) {

            case 1:
                if (playerStat.PlayerGold > cost[0])
                {
                    playerStat.PlayerGold -= cost[0];
                    playerStat.DmgTmp += 1;
                }
                break;

            case 2:
                if (playerStat.PlayerGold > 30)
                {
                    playerStat.PlayerGold -= 30;
                    playerStat.defTmp += 1;
                }
                break;

            case 3:
                if (playerStat.PlayerGold > 30)
                {
                    playerStat.PlayerGold -= 30;
                    playerStat.DmgTmp += 1;
                }
                break;

            case 4:
                if (playerStat.PlayerGold > 30)
                {
                    playerStat.PlayerGold -= 30;
                    playerStat.DmgTmp += 1;
                }
                break;

            case 5:
                if (playerStat.PlayerGold > 30)
                {
                    playerStat.PlayerGold -= 30;
                    playerStat.DmgTmp += 1;
                }
                break;

            case 6:
                if (playerStat.PlayerGold > 30)
                {
                    playerStat.PlayerGold -= 30;
                    playerStat.DmgTmp += 1;
                }
                break;

            case 7:
                if (playerStat.PlayerGold > 30)
                {
                    playerStat.PlayerGold -= 30;
                    playerStat.DmgTmp += 1;
                }
                break;

            case 8:
                if (playerStat.PlayerGold > 30)
                {
                    playerStat.PlayerGold -= 30;
                    playerStat.DmgTmp += 1;
                }
                break;

            case 9:
                if (playerStat.PlayerGold > 30)
                {
                    playerStat.PlayerGold -= 30;
                    playerStat.DmgTmp += 1;
                }
                break;

            case 10:
                if (playerStat.PlayerGold > 30)
                {
                    playerStat.PlayerGold -= 30;
                    playerStat.DmgTmp += 1;
                }
                break;
        }
    }
}
