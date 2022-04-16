using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int isPedaling = 0; // Use this to set whether the player is pedaling and I pass this int to powerSource.cs to read

    public float playerGold = 0;
    private int playerEXP = 0;
    private int playerLevel  =1;
    [SerializeField] float levelUpEXP = 30f;
    [SerializeField] float limitGold = 500f;
    [SerializeField] Image resourceBar;
    [SerializeField] Text resourceText;
    [SerializeField] Text limitText;
    [SerializeField] Text levelUpEXPText;
    [SerializeField] Image levelBar;
    [SerializeField] Text EXPText;
    [SerializeField] Text levelText;
    [SerializeField] GameObject collectionPanel;
    
    private bool isLevelUp =false; 
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        resourceBar.fillAmount = playerGold/limitGold;
        levelBar.fillAmount = playerEXP/levelUpEXP;
        levelText.text = playerLevel.ToString();
        levelUpEXPText.text = " / "+levelUpEXP.ToString();
        resourceText.text   = playerGold.ToString();
        EXPText.text = playerEXP.ToString();
        limitText.text   = " / "+limitGold.ToString();
        if(Input.GetKey(KeyCode.A)){
            isPedaling = 1;
        }else{
            isPedaling = 0;
        }
        if(playerEXP==30){
            isLevelUp =true;
            LevelUp();
        }
    }


    public void gainGold(int amout){
        playerGold += amout;
    }
    public void gainEXP(int exp){
        playerEXP += exp;
    }

    private void LevelUp(){
        playerLevel +=1;
        playerEXP = 0;
        isLevelUp =false;
    }
    public void SwitchPanel(){
        if(collectionPanel.activeInHierarchy){
            collectionPanel.SetActive(false);
            Debug.Log("swtich panel t");
        }else{
            collectionPanel.SetActive(true);
            Debug.Log("swtich panel f");
        }
        
        
    }
}
