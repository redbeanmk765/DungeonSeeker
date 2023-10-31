using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public float maxHp;
    public float nowHp;
    public float Dmg;
    public bool onFlash;
    public float damaged;
    public float cure;
    public int PlayerGold;
    public Image item1;
    public Image item2;
    public Image item3;
    public Image PlayerHpBar;
    public Text PlayerHpText;
    public Text PlayerGoldText;
    public bool IsSafeZone;

    [SerializeField] public Material originalMaterial;
    [SerializeField] public Material flashMaterial;
    // Start is called before the first frame update
    void Start()
    {
        Dmg = 20;
        onFlash = false;
        maxHp = 100;
        nowHp = maxHp;
        PlayerGold = 0;
        IsSafeZone = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerGoldText.text = PlayerGold.ToString();
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






    IEnumerator FlashWhite()
    {
        while (onFlash)
        {
            this.GetComponent<SpriteRenderer>().material = this.flashMaterial;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<SpriteRenderer>().material = this.originalMaterial;
            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i <= 4; i++)
            {

                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
                yield return new WaitForSeconds(0.1f);
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.2f);
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
