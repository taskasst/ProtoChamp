using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDraging : MonoBehaviour
{
    [SerializeField] float horizontalSpeed = 2.0F;
    [SerializeField] float verticalSpeed = 2.0F;
    void Start()
    {
        
    }

     void Update() {
       

     }
     private void OnMouseDrag()
     {
        if(Input.GetMouseButton(0)){
         float h = horizontalSpeed * Input.GetAxis("Mouse X");
         float v = verticalSpeed * Input.GetAxis("Mouse Y");
         transform.Rotate(v, h, 0);
         }
     }
}
