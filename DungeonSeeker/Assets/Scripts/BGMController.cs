using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clip;
    public StageController sCon;
    public int stage;
    public int lastStage;
    // Start is called before the first frame update
    void Start()
    {
        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lastStage = stage;
        switch(sCon.count - 1)
        {
            case <= 1:
                stage = 0;
                break;

            case <= 2:
            case >= 6:
                stage = 1;
                break;
        }

        if(lastStage != stage)
        {
            audioSource.Stop();
        }
        if (audioSource.GetComponent<AudioSource>().isPlaying) return;
            audioSource.PlayOneShot(clip[stage]);
    }


}
