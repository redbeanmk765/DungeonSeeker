using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpPotion : MonoBehaviour
{

    public Sprite full;
    public Sprite empty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Full()
    {
        this.GetComponent<Image>().sprite = full;
    }

    public void Empty()
    {
        this.GetComponent<Image>().sprite = empty;
    }
}
