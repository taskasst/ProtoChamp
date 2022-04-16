using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] float growthValue;
    [SerializeField] float growthRate = 0.1f;
    [SerializeField] float growthLimit= 5f;
    [SerializeField] Image progressBar;
    [SerializeField] int goldInthisPlant = 10;
    [SerializeField] int EXPInthisPlant = 5;
    [SerializeField] int id = 0;
    [SerializeField] string itemName = "NONE";

    [SerializeField] Sprite collectionImage ;

    [SerializeField] ParticleSystem particleS;
    private bool isGrowing = true;
    private float sunPower;
    private float playerPower;
    private float standard_time = 0f;

    private PlantManager plantManager;
    private Transform _transform;
    private MeshRenderer meshRend;

    //------------------- for testing---------------------

    [SerializeField] Material oldMaterial ;
    [SerializeField] Material newMaterial ;

    //---------------------------------------------------
    
    private new Vector3 tmp;

    void Start()
    {
        sunPower = GameObject.Find("PowerSource").GetComponent<PowerSource>().powerFromSun ;
        playerPower = GameObject.Find("PowerSource").GetComponent<PowerSource>().powerFromPlayer ;
        growthValue = 0;

        plantManager = PlantManager.Instance;
        _transform = gameObject.GetComponent<Transform>();
        meshRend = gameObject.GetComponent<MeshRenderer>();

        tmp = _transform.localScale;
        _transform.localScale = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {  
        playerPower = GameObject.Find("PowerSource").GetComponent<PowerSource>().powerFromPlayer ;
        standard_time  = Time.deltaTime *10;
        progressBar.fillAmount = growthValue/growthLimit;

        growthRate = plantManager.growthSpeed;

        if(isGrowing){
            growthValue += (sunPower+playerPower) * growthRate *  standard_time ;
            _transform.localScale = (tmp/growthLimit)*growthValue;
        }
        if(growthValue>=growthLimit){
            isGrowing = false;
            meshRend.material = newMaterial;
        }
    }

    void GainSource(){
        if(!isGrowing){
            GameObject player = GameObject.Find("Player");
            player.GetComponent<Player>().gainGold(goldInthisPlant);
            player.GetComponent<Player>().gainEXP(EXPInthisPlant);
            player.GetComponent<Collection>().PutIn(this.gameObject);
            growthValue = 0;
            isGrowing = true;
            particleS.Play();
            meshRend.material = oldMaterial;
        }
    }
    private void OnMouseDown()
    {
        GainSource();
    }
    public int GetID()
    {
        return id;
    }
    public Sprite GetImage(){
        return collectionImage;
    }
}
