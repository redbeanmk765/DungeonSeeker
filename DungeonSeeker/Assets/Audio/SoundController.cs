    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clip;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //switch (player.GetComponent<Animator>().GetInteger("State"))
        //{ 
        //    case 0 :
        //        if (audioSource.GetComponent<AudioSource>().isPlaying) return;
             
        //        break;
        //    default:
        //        audioSource.Stop();
        //        break;
        //}
    }

    public void Jump()
    {
        audioSource.PlayOneShot(clip[1],0.7f);
    }

    public void Swish()
    {
        audioSource.PlayOneShot(clip[3]);
    }
    
    public void Dash()
    {
        audioSource.PlayOneShot(clip[4],0.5f);
    }

    public void Clock()
    {
        audioSource.PlayOneShot(clip[5], 1f);
    }

    public void Damaged()
    {
        audioSource.PlayOneShot(clip[5], 1f);
    }



}
