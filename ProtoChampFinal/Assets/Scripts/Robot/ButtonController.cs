using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject body;
    public GameObject cannon;
    public GameObject block;
    public GameObject saw;

    public GameObject customizeCanvas;
    public GameObject fightCanvas;
    public Text energyText;

    public GameObject core;

    public GameObject holdingObject;

    private Vector3 screenPos;
    private Vector3 offset;

    public GameObject level1;

    public GameObject list1;
    public GameObject list2;

    private int currentEnergy = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (holdingObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray.direction);
            RaycastHit hit;

            int layerMask = LayerMask.NameToLayer("Board");
            //Debug.Log(layerMask);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000, Color.white);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("hit");
                if (hit.transform.gameObject.tag == "Slot")
                {
                    holdingObject.transform.position = hit.transform.position;
                    if (holdingObject.name.Contains("Block"))
                    {
                        holdingObject.transform.parent = core.transform;
                        holdingObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        holdingObject.transform.parent = null;
                        holdingObject.transform.rotation = hit.transform.rotation;
                    }
                }
                else
                {
                    holdingObject.transform.position = hit.point;
                    holdingObject.transform.rotation = Quaternion.identity;
                }

            }
        }
        else
        {

            if (Input.GetMouseButton(0))
            {
                //Debug.Log("hia");
                float y = Input.GetAxis("Mouse X") * Time.deltaTime * 200.0f;
                float x = Input.GetAxis("Mouse Y") * Time.deltaTime * 200.0f;
                core.transform.Rotate(new Vector3(x, -y, 0), Space.World);
            }

        }


    }

    public void HoldCannon()
    {
        holdingObject = Instantiate(cannon);

    }

    public void HoldBlock()
    {
        holdingObject = Instantiate(block);
    }

    public void HoldSaw()
    {
        holdingObject = Instantiate(saw);
    }

    public void ReleaseObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(ray.direction);
        RaycastHit hit;

        //int layerMask = LayerMask.NameToLayer("Board");
        //Debug.Log(layerMask);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 1000, Color.white);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("hit");
            if (hit.transform.gameObject.tag == "Slot")
            {
                holdingObject.transform.parent = hit.transform.parent;
                BoxCollider[] bcs = holdingObject.GetComponentsInChildren<BoxCollider>();
                foreach (BoxCollider bc in bcs)
                {
                    bc.enabled = true;
                }
                hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;

                if (holdingObject.name.Contains("Block"))
                {
                    currentEnergy += 1;
                }
                else if (holdingObject.name.Contains("Saw"))
                {
                    currentEnergy += 5;
                }
                else
                {
                    currentEnergy += 10;
                }
                energyText.text = currentEnergy.ToString();

                holdingObject = null;
            }
            else
                Destroy(holdingObject);
        }

    }

    public void EnterLevel1()
    {
        level1.SetActive(true);
        holdingObject = null;

        customizeCanvas.SetActive(false);
        fightCanvas.SetActive(true);

        //core.transform.rotation = Quaternion.identity;
        core.transform.eulerAngles = new Vector3(-90, -180, 0);

        core.AddComponent<Rigidbody>();
        core.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        this.GetComponent<RobotController>().enabled = true;
        this.GetComponent<RobotController>().SetEnergy(currentEnergy);
        this.GetComponent<LevelController>().enabled = true;
        foreach (GunController gc in core.GetComponentsInChildren<GunController>())
        {
            gc.enabled = true;
        }
        foreach (BoxCollider bc in core.GetComponentsInChildren<BoxCollider>())
        {
            if (bc.gameObject.tag == "Slot")
                bc.enabled = false;
        }
        this.GetComponent<ButtonController>().enabled = false;
    }

    public void SwitchToList2()
    {
        Debug.Log("Aa)");
        list1.SetActive(false);
        list2.SetActive(true);
    }

    public void SwitchToList1()
    {
        list2.SetActive(false);
        list1.SetActive(true);
    }
}
