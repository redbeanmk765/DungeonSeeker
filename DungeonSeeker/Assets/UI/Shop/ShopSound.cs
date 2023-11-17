using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        audioSource.PlayOneShot(clip[0]);
    }

    public void Sucess()
    {
        audioSource.PlayOneShot(clip[1]);
    }

    public void Fail()
    {
        audioSource.PlayOneShot(clip[2]);
    }
}
