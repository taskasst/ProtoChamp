using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Generator : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] GameObject plant_01;
    [SerializeField] GameObject[] plant = new GameObject[2];
    [SerializeField] int GenerateFreq = 2;
    [SerializeField] float radius;
    [SerializeField] Vector3 centre;
    private float timer = 0f;
    private int preTime = 0;
    private int postTime = 0;
    void Start()
    {
        centre = this.gameObject.GetComponent<SphereCollider>().center;
        radius = this.gameObject.GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if((int)timer%GenerateFreq==0&&preTime-postTime!=0){
            CreatePlant(); 
            postTime = preTime;
        }
        timer += Time.deltaTime;
        preTime = (int)timer;


    }

    private Vector3 GenerateSphere(float radius, Vector3 centre, int theta , int phi){ // theta  and  phi just randomly generate two int in radian 

        Vector3 c = new Vector3();

        c= new Vector3(radius*Mathf.Sin(theta)*Mathf.Cos(phi),radius*Mathf.Sin(theta)*Mathf.Sin(phi),radius*Mathf.Cos(theta));
			
        return  c ;
    }
    public void CreatePlant(){ // radius is the radius of the collider , and centre is the sphere's centre

        if(GameObject.Find("Player").GetComponent<Player>().playerGold>5){
        Vector3 Po = GenerateSphere(radius,centre,Random.Range(0,100),Random.Range(0,100));
        GameObject gm =  Instantiate(plant[Random.Range(0,2)],Po,new Quaternion(0,0,0,0),this.gameObject.GetComponent<Transform>().transform);
        gm.GetComponent<Transform>().localPosition = Po;
        Vector3 v = Po- centre;
        Quaternion q = Quaternion.FromToRotation(transform.forward,v);
        gm.GetComponent<Transform>().localRotation= q * transform.rotation;
        //gm.GetComponent<Transform>().up = Po;
        //gm.GetComponent<Transform>().Translate(Po*gm.transform.localScale.y, Space.Self);
        //gm.GetComponent<Transform>().transform.localRotation = Quaternion.LookRotation(Po);
        //gm.GetComponent<Transform>().transform.localRotation = Quaternion.Euler(90,0,0); // unfix the bug of rotate the plant follow the vector of centre

        GameObject.Find("Player").GetComponent<Player>().gainGold(-5); // when player create a plant, cost amount of gold.
        }
    }
 
}
