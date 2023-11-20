using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public GameObject armadillo;
    public GameObject tmp;
    public bool IsLeft;

    // Start is called before the first frame update

    private void OnEnable()
    {
        Spawn();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
       tmp = Instantiate(armadillo);
       tmp.transform.position = this.transform.position;
        tmp.GetComponent<armadillo>().RollStart = true;
       if(IsLeft == true)
        {
            tmp.GetComponent<armadillo>().Turn();
        }
    }
}
