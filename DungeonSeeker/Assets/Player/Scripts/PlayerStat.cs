using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float maxHp;
    public float nowHp;
    public float Dmg;
    public bool onFlash;
    public float damaged;
    public float cure;
    [SerializeField] public Material originalMaterial;
    [SerializeField] public Material flashMaterial;
    // Start is called before the first frame update
    void Start()
    {
        Dmg = 5;
        onFlash = false;
        maxHp = 100;
        nowHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
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
