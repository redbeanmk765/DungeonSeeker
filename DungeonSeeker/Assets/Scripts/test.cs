using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class test : MonoBehaviour
{

    public Text Text;
    public TMP_Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        Text.transform.position = this.transform.position;
        nameText.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
