using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
    // Start is called before the first frame update
    private Dictionary<int, int> collectionList = new Dictionary<int, int>(); // <the index the panel,the id of the input gameobject> , dict for collection
    private Dictionary<int, int> amoutOFitems = new Dictionary<int, int>(); // Total Amout of each item save in the list
    [SerializeField] GameObject[] slots;
    private int currentKey = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutIn(GameObject items)
    {   
        int itemID = items.GetComponent<Plant>().GetID();
        if(collectionList.ContainsKey(itemID))
        {
            amoutOFitems[itemID] ++;
            UpdateCollection(itemID,items);
        }else{
            Debug.Log("current key is : "+currentKey+ "this items id is : "+ itemID);
            collectionList.Add(itemID,currentKey);
            amoutOFitems.Add(itemID,1);
            UpdateCollection(itemID,items);
            currentKey ++;
            
            
        }
      
    }
    private void UpdateCollection(int index, GameObject Plant){
       Transform childText =  slots[collectionList[index]].transform.Find("Text");
       Transform childImage =  slots[collectionList[index]].transform.Find("Image");
       childText.GetComponent<Text>().text  = amoutOFitems[index].ToString();
       childImage.GetComponent<Image>().sprite = Plant.GetComponent<Plant>().GetImage();
    }
}
