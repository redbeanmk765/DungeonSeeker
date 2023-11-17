using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] clip;
    public AudioSource audioSource;
    public GameObject slime;
    public Image panel;
    public float time;
    public bool Enter;

    // Update is called once per frame
    private void Start()
    {
        Enter = false;
        Screen.SetResolution(1600, 900, false) ;
    }
    void Update()
    {
        if (Input.GetButtonDown("Enter") && Enter == false)
        {
            Enter = true;
            audioSource.PlayOneShot(clip[0]);
            slime.GetComponent<Animator>().SetBool("Wake", true);
            StartCoroutine(FadeCor());
            
            Debug.Log("enter");

        }
    }

    IEnumerator FadeCor()
    {
        panel.gameObject.SetActive(true);
        Color color = panel.color;
        color.a = 0;
        time = 0f;
        while (color.a < 1f)
        {
            time += 0.5f * Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0, 1, time);
            panel.color = color;
            yield return 0;
        }

        yield return new WaitForSecondsRealtime(0.5f);
        time = 0f;
        SceneManager.LoadScene("InGame");
        yield return 0;
    }
}
