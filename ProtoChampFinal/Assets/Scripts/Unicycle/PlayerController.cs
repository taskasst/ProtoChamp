using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     private bool isPedaling;
     public float speed = 0f; // Max speed currently is 10f
     public float maxSpeed = 20f;
     public float strenght = 10f; 
     public float resistance = 2f;
     public KeyCode lastInput =KeyCode.D;
     
     void Start(){
         
     }
     void FixedUpdate(){
          if(Input.GetKeyDown(KeyCode.B)&&speed>=0){ // brake
              speed-=3;
          }
          if (Input.GetKey(KeyCode.A)&&lastInput==KeyCode.D){ // move forward
                isPedaling = true;
                lastInput=KeyCode.A;
                transform.Translate(Vector3.forward*speed*Time.deltaTime);
                if(speed<maxSpeed){
                    speed += Time.deltaTime*strenght;
                }
          }else if(Input.GetKey(KeyCode.D)&&lastInput==KeyCode.A){
                isPedaling = true;
                lastInput=KeyCode.D;
                transform.Translate(Vector3.forward*speed*Time.deltaTime);
                if(speed<maxSpeed){
                    speed += Time.deltaTime*strenght;
                }
          }
          else{  // resistance when not pedaling
              transform.Translate(Vector3.forward*speed*Time.deltaTime);
              if(speed>=0){
                    speed -= Time.deltaTime *resistance;
              }
          } 
     }
}
