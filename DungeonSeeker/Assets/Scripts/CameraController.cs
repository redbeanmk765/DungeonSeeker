using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraPosition;
    public Image Result;
    public Image ResultClear;
    public GameObject Text;
    public GameObject TextClear;
    public Sprite result0;
    public Sprite result1;
    public GameObject player;
    public float cameraSize;
    public float bossCameraSize;

    public Vector2 mapCenter;
    public Vector2 mapSize;


    public float cameraMoveSpeed;
    public float height;
    public float width;

    public bool IsStop;
    public bool IsResult;
    public bool IsClose;
    public bool IsClear;

    public bool IsFix;
    void Start()
    {
        cameraSize = 6f;
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        Camera.main.orthographicSize = 6;
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
        IsStop = false;
        IsResult = false;
        IsClose = false;
        IsFix = false ;
        IsClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && IsClose == true)
        {
            if (IsClear == false)
            {
                GetComponent<FadeController>().Retry();
            }
            else if(IsClear == true)
            {
                GetComponent<FadeController>().Retry();
                IsClear = false;
            }

        }
    }

    private void FixedUpdate()
    {
        if (IsStop == false && IsFix == false )
        {
            Camera.main.orthographicSize = cameraSize;
            transform.position = Vector3.Lerp(transform.position, playerTransform.position + new Vector3(0, 2f, 0), Time.unscaledDeltaTime * cameraMoveSpeed);
            float lx = mapSize.x - width;
            float clampX = Mathf.Clamp(transform.position.x, -lx + mapCenter.x, lx + mapCenter.x);

            float ly = mapSize.y - height;
            float clampY = Mathf.Clamp(transform.position.y, -ly + mapCenter.y, ly + mapCenter.y);

            transform.position = new Vector3(clampX, clampY, -10f);
        }

        if(IsResult == true)
        {
           
            transform.position = Vector3.Lerp(transform.position, playerTransform.position + new Vector3(2.2f, 1f, -10f), Time.unscaledDeltaTime * cameraMoveSpeed);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3f, Time.unscaledDeltaTime * 1f);

            
        }

        if(IsFix == true)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, bossCameraSize, Time.unscaledDeltaTime ); 
            transform.position = Vector3.Lerp(transform.position,  new Vector3(mapCenter.x, mapCenter.y, -10f), Time.unscaledDeltaTime );

        }
    }

    public void ResultCall ()
    {
        IsStop = true;
        IsResult = true;
        IsFix = false;
        if (IsClear == false)
        {
            StartCoroutine(ShowResult());
        }

        else if (IsClear == true)
        {
            StartCoroutine(ShowResultClear());
        }

    }

    public void Retry()
    {
        IsResult = false;
        IsStop = false;
        IsClose = false;
        Text.SetActive(false);
        Result.gameObject.SetActive(false);
        TextClear.SetActive(false);
        ResultClear.gameObject.SetActive(false);
    }


    IEnumerator ShowResult()
    {
        yield return new WaitForSecondsRealtime(1f);
        Result.gameObject.SetActive(true);
        Result.sprite = result0;
        yield return new WaitForSecondsRealtime(2f);
        if (Vector2.Distance(transform.position, playerTransform.position) <= 2.417f)
        {
            Result.sprite = result1;
            Text.SetActive(true);
            IsClose = true;
        }
    }

    IEnumerator ShowResultClear()
    {
        yield return new WaitForSecondsRealtime(1f);
        ResultClear.gameObject.SetActive(true);
        ResultClear.sprite = result0;
        yield return new WaitForSecondsRealtime(2f);
        if (Vector2.Distance(transform.position, playerTransform.position) <= 2.417f)
        {
            ResultClear.sprite = result1;
            TextClear.SetActive(true);
            IsClose = true;
        }
    }
}
