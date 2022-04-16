using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planting_plants : MonoBehaviour
{
    public GameObject target;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("碰撞位置："+hit.normal);
            target.transform.position = hit.point;
            target.transform.up = hit.normal;
            target.transform.Translate(Vector3.up * 0.5f*target.transform.localScale.y, Space.Self);
        }
      
    }
}
