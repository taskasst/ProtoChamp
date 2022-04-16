using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] assets ;
    private int currentLv = 0 ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBuilding()
    {
        assets[currentLv].SetActive(true); // need some constrains for this function
        currentLv++;

    }
}
