using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGcontroller : MonoBehaviour
{

    public int state;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
    }

    public void Sound()
    {
        if (audioSource.GetComponent<AudioSource>().isPlaying) audioSource.Stop(); ;
        audioSource.Play();
    }

    // Update is called once per frame
 
}
