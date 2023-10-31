using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraPosition;

    public Vector2 mapCenter;
    public Vector2 mapSize;


    public float cameraMoveSpeed;
    public float height;
    public float width;
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition, Time.unscaledDeltaTime * cameraMoveSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + mapCenter.x, lx + mapCenter.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + mapCenter.y, ly + mapCenter.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    public void test()
    {
        Debug.Log("TTEESSTT");
    }
}
