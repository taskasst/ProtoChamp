using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] int id = 0;
    [SerializeField] string itemName = "NONE";
    
    
    private int amountOfItem = 1;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(){
        amountOfItem++;
        Debug.Log(amountOfItem);
    }
    public int GetID(){
        return id;
    }
}
