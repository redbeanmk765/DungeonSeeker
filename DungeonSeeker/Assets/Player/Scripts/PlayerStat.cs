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
    public int PlayerGold;
    public int PlayerPlat;
    public bool skillOn;
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
    public int AirJumpCountMax;
    public int AirJumpCountMaxTmp;
    public int HpPotionCount;
    public int HpPotionMax;

    [SerializeField] public Material originalMaterial;
    [SerializeField] public Material flashMaterial;
    // Start is called before the first frame update
    void Start()
    {
        statUpdate();
        onFlash = false;
        nowHp = maxHp;
        PlayerGold = 0;
        IsSafeZone = true;
        skillCoolTimeTmp = -30f;
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
                nowHp = nowHp - damaged;
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
        maxHp = saveData.maxHpPer + maxHpTmp;
        def = saveData.defPer + defTmp;
        hitBox = saveData.hitBoxPer + hitBoxTmp;
        dashCoolTime = saveData.dashCoolTimePer + dashCoolTimeTmp;
        AttackCoolTime = saveData.AttackCoolTimePer + AttackCoolTimeTmp;
        Dmg = saveData.DmgPer + DmgTmp;
        PlayerPlat = saveData.PlayerPlat;
        skillOn = saveData.skillOn;
        skillduration = saveData.skilldurationPer + skilldurationTmp;
        skillCoolTime = saveData.skillCoolTimePer + skillCoolTimeTmp;
        AirJumpCountMax = saveData.AirJumpCountMaxPer + AirJumpCountMaxTmp;
        HpPotionMax = saveData.HpPotionMax;
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
