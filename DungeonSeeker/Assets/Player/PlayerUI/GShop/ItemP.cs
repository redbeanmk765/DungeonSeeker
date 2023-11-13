using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemP : MonoBehaviour
{
    public int num;
    public float level;
    public float levelMax;
    public float cost;
    public ShopP shop;
    public GameObject itemList;
    public Text costText;
    public Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        shop = GameObject.Find("ContentP").GetComponent<ShopP>();
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(75, -(300 + 300 * (num)));

    }

    // Update is called once per frame
    void Update()
    {
        cost = shop.cost[num];
        level = shop.level[num];
        levelMax = shop.levelMax[num];
        costText.text = cost.ToString();
        
        if(level == levelMax)
        {
            levelText.text = "LV.Max";
            costText.text = "-";
        }
        else
        {
            levelText.text = "LV. " + (level + 1).ToString();
        }
    }
}
