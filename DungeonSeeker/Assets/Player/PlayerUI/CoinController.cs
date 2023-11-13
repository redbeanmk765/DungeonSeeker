using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public GameObject Gold;
    public GameObject Plat;
    public int state;
    public PlayerStat playerStat;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 1)
        {
            Plat.SetActive(false);
            Gold.SetActive(true);
            playerStat.GoldUpdate();
        }

        else if(state == 2)
        {
            Gold.SetActive(false);
            Plat.SetActive(true);
            playerStat.PlatUpdate();
        }
    }
}
