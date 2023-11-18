using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSpiderHead : MonoBehaviour
{
    public GameObject player;
    public GameObject giantSpider;
    public SpriteRenderer Head;
    public Material flashMaterial;
    public Material originalMaterial;
    public bool onFlash;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Attack"))
        {
            giantSpider.GetComponent<GiantSpider>().damaged = col.gameObject.GetComponent<HitBox>().Dmg;
            onFlash = true;
            StartCoroutine(FlashWhite());
        }
    }

    IEnumerator FlashWhite()
    {
        while (onFlash)
        {
            Head.material = flashMaterial;
            yield return new WaitForSecondsRealtime(0.1f);
            Head.material = originalMaterial;

            if (onFlash == false)
            {
                Head.material = originalMaterial;
                yield break;

            }
            onFlash = false;


        }
        if (onFlash == false)
        {
            Head.material = originalMaterial;
            yield break;
        }
    }
}
