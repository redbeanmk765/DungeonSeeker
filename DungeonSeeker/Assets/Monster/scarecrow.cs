using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scarecrow : MonoBehaviour
{

    [SerializeField] public Material originalMaterial;
    [SerializeField] public Material flashMaterial;
    public bool onFlash = false;

    // Start is called before the first frame update
    void Start()
    {
        onFlash = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Attack"))
        {
            onFlash = true;
            StartCoroutine(FlashWhite());
        }
    }

    IEnumerator FlashWhite()
    {
        while (onFlash)
        {

            Debug.Log("test");
            this.GetComponent<SpriteRenderer>().material = flashMaterial;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<SpriteRenderer>().material = originalMaterial;



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
