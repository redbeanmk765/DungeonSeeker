using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public SaveData saveData;
    public float maxHp;
    public float maxHpTmp;
    public float nowHp;
    public float def;
    public float defTmp;
    public float defPer;
    public float hitBox;
    public float hitBoxTmp;
    public float dashCoolTime;
    public float dashCoolTimeTmp;
    public float AttackCoolTime;
    public float AttackCoolTimeTmp;
    public float Dmg;
    public float DmgTmp;
    public bool onFlash;
    public float damaged;
    public float cure;
    public float PlayerGold;
    public float PlayerPlat;
    public float skillduration;
    public float skilldurationTmp;
    public float skillCoolTime;
    public float skillCoolTimeTmp;
    public Image item1;
    public Image item2;
    public Image item3;
    public Image PlayerHpBar;
    public Text PlayerHpText;
    public Text PlayerGoldText;
    public bool IsSafeZone;
    public float AirJumpCountMax;
    public float AirJumpCountMaxTmp;
    public float HpPotionCount;
    public float HpPotionMax;
    public float HpPotionMaxPer;
    public float HpPotionMaxTmp;

    [SerializeField] public Material originalMaterial;
    [SerializeField] public Material flashMaterial;
    // Start is called before the first frame update
    void Start()
    {
        statUpdate();
        statReset();
        onFlash = false;
        nowHp = maxHp;
        PlayerGold = 10000;
        IsSafeZone = true;
        HpPotionCount = HpPotionMax;

    }

    // Update is called once per frame
    void Update()
    {
        statUpdate();
        PlayerGoldText.text = PlayerGold.ToString();
        GameObject.Find("Item1Count").GetComponent<Text>().text = HpPotionCount.ToString();


        if (Input.GetButtonDown("Item1") && HpPotionCount > 0){
            HpPotionCount -= 1;
            cure += 30;
        }
        if(HpPotionCount == 0)
        {
            GameObject.Find("item1Image").gameObject.GetComponent<HpPotion>().Empty();
        }
        else
        {
            GameObject.Find("item1Image").gameObject.GetComponent<HpPotion>().Full();
        }
        if (this.damaged != 0)
        {
            if (!onFlash && !this.GetComponent<MoveController>().IsDash)
            {
                nowHp = nowHp - (damaged - def);
                onFlash = true;
                StartCoroutine(FlashWhite());
            }
            this.damaged = 0;


        }
        nowHp = nowHp + cure;


        PlayerHpText.text = nowHp + "  /  " + maxHp;

        float hpRatio = nowHp / maxHp;
        PlayerHpBar.fillAmount = Mathf.Lerp(PlayerHpBar.fillAmount, hpRatio, Time.deltaTime * 10);


        //if (nowHp <= 0)
        //{
        //    playerDie = true;
        //    player.GetComponent<Animator>().SetBool("isDie", true);
        //    player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //}

        cure = 0;
        damaged = 0;

        if (nowHp > maxHp)
        {
            nowHp = maxHp;
        }
    }

    public void statUpdate()
    {
        saveData = DataController.Instance.data;
        maxHp = 100 + (saveData.maxHpPer + maxHpTmp) * 20 ;
        def = (saveData.defPer + defTmp) * 1 ;
        hitBox = saveData.hitBoxPer + hitBoxTmp;
        dashCoolTime = 2 - (saveData.dashCoolTimePer + dashCoolTimeTmp) * 0.1f ;
        AttackCoolTime = 0.2f - (saveData.AttackCoolTimePer + AttackCoolTimeTmp) * 0.03f ;
        Dmg = 6 + (saveData.DmgPer + DmgTmp) * 2 ;
        PlayerPlat = saveData.PlayerPlat;
        skillduration = 6 + (saveData.skilldurationPer + skilldurationTmp) * 2f ;
        skillCoolTime = 60 - (saveData.skillCoolTimePer + skillCoolTimeTmp) - 5f;
        AirJumpCountMax = 1 + (saveData.AirJumpCountMaxPer + AirJumpCountMaxTmp) ;
        HpPotionMax = 3 + saveData.HpPotionMaxPer + HpPotionMaxTmp;
    }

    public void statReset()
    {
        maxHpTmp = 0;
        defTmp = 0;
        hitBoxTmp = 0;
        dashCoolTimeTmp = 0;
        AttackCoolTimeTmp = 0;
        DmgTmp = 0;
        skilldurationTmp = 0;
        skillCoolTimeTmp = 0;
        AirJumpCountMaxTmp = 0;
        HpPotionMaxTmp = 0;
    }




    IEnumerator FlashWhite()
    {
        while (onFlash)
        {
            this.GetComponent<SpriteRenderer>().material = this.flashMaterial;
            yield return new WaitForSecondsRealtime(0.1f);
            this.GetComponent<SpriteRenderer>().material = this.originalMaterial;
            yield return new WaitForSecondsRealtime(0.1f);

            for (int i = 0; i <= 4; i++)
            {

                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
                yield return new WaitForSecondsRealtime(0.1f);
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                yield return new WaitForSecondsRealtime(0.2f);
            }

            this.GetComponent<SpriteRenderer>().material = this.originalMaterial;




            if (onFlash == false)
            {
                yield break;
            }
            onFlash = false;


        }
        if (onFlash == false)
        {
            yield break;
        }
    }
}
