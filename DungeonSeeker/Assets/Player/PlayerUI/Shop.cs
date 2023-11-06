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
    // Start is called before the first frame update
    void Start()
    {
        Content.anchoredPosition = new Vector2(0,0);
        Select.anchoredPosition = new Vector2(40, -320);
        count = 1;
        max = 6;
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
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
                playerStat.maxHpTmp += 20;
                break;
            
            case 2:

                break;

        }
    }
}
