using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject Player;
    public float Dmg;
    public AudioSource sound;
    public AudioClip[] clip;
    public SoundController sCon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        sCon.Swish();
    }

    // Update is called once per frame
    void Update()
    {

        Dmg = Player.GetComponent<PlayerStat>().Dmg;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.CompareTag("Monster"))
        {
            //sCon.Swish();
            sound.PlayOneShot(clip[0],0.8f);
        }

         
    }
}
