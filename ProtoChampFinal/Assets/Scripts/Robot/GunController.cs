using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    protected LevelController lc;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        lc = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Debug.DrawRay(this.transform.position + -this.transform.forward * 0.6f, -this.transform.up * 1000, Color.white);
    }


}
