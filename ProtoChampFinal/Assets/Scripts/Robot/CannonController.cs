using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : GunController
{
    // Start is called before the first frame update
    public GameObject fireParticleSystem;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (lc.gameObject.GetComponent<RobotController>().CouldMove())
            {
                this.fireParticleSystem.GetComponent<ParticleSystem>().Play();

                Ray ray = new Ray(this.transform.position + -this.transform.up, -this.transform.up * 1000);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        Debug.Log("eliminated");
                        Destroy(hit.transform.gameObject);
                        lc.EliminateEnemy();
                    }
                }
            }
            else
            {
                this.fireParticleSystem.GetComponent<ParticleSystem>().Stop();
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
}
