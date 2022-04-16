using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSensor : MonoBehaviour
{
    private bool isEntering = false;
    [SerializeField] float enteringTime = 3f; // Time that player need to keep stay in the goal area
    private float enteringTimeTmp;
    // Start is called before the first frame update
    void Start()
    {
        enteringTimeTmp = enteringTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other) {
        if(enteringTime<0&&other.gameObject.tag=="Player"){ // when Player stays in the goal area
            isEntering = true;
            GameManager.GameOver(isEntering);
        }else{
            enteringTime -= Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag=="Player")
        enteringTime = enteringTimeTmp;
    }
}
    
