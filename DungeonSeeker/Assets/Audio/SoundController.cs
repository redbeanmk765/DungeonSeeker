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
        switch (player.GetComponent<Animator>().GetInteger("State"))
        { 
            case 2 :
                if (audioSource.GetComponent<AudioSource>().isPlaying) return;
                audioSource.PlayOneShot(clip[0]);
                audioSource.volume = 0.3f;
                break;
            default:
                audioSource.Stop();
                break;
        }
    }


}
