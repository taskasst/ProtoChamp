using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SawController : GunController
{
    // Start is called before the first frame update
    public GameObject saw;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Rotate());
        this.GetComponent<BoxCollider>().enabled = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            if (lc.gameObject.GetComponent<RobotController>().CouldMove())
            {
                saw.transform.DOLocalRotate(saw.transform.localEulerAngles + new Vector3(0, 0, 90), 0.02f);
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Enemy" && lc.gameObject.GetComponent<RobotController>().CouldMove())
        {
            Debug.Log("eliminated");
            Destroy(other.transform.gameObject);
            lc.EliminateEnemy();
        }
    }
}
