using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraPosition;
    public Image Result;
    public GameObject Text;
    public Sprite result0;
    public Sprite result1;
    public GameObject player;

    public Vector2 mapCenter;
    public Vector2 mapSize;


    public float cameraMoveSpeed;
    public float height;
    public float width;

    public bool IsStop;
    public bool IsResult;
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        Camera.main.orthographicSize = 6;
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
        IsStop = false;
        IsResult = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && IsResult == true)
        {
            Debug.Log("push");
            GetComponent<FadeController>().Retry();

        }
    }

    private void FixedUpdate()
    {
        if (IsStop == false)
        {
            Camera.main.orthographicSize = 6f;
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
    }

    public void ResultCall ()
    {
        IsStop = true;
        IsResult = true;

        StartCoroutine(ShowResult());
        
    }

    public void Retry()
    {
        IsResult = false;
        IsStop = false;
        Text.SetActive(false);
        Result.gameObject.SetActive(false);

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
        }
    }
}
