using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PedalController : MonoBehaviour
{
    private float speed;
    public GameObject rotatingPart;
    public List<GameObject> pedals;

    public GameObject ribbon;
    //public Text speedText;

    //public GameObject ribbonTemplate;
    //public int ribbonCount;
    //public float ribbonLength = 20f;
    //private List<GameObject> ribbons;

    private float totalS = 0;
    // Start is called before the first frame update
    void Start()
    {
        //ribbons = new List<GameObject>();
        //GameObject ribbon = Instantiate(ribbonTemplate, transform);
        //ribbon.transform.localPosition = new Vector3(0, -3, 0);
        //ribbons.Add(ribbon);
        //for (int i = 1; i < ribbonCount; i++)
        //{
        //    ribbon = Instantiate(ribbonTemplate, transform);
        //    ribbon.transform.localPosition = new Vector3(0, 0, ribbonLength)
        //        + ribbons[ribbons.Count - 1].transform.localPosition;
        //    ribbons.Add(ribbon);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        rotatingPart.transform.Rotate(Vector3.right, speed * 5, Space.Self);
        foreach (GameObject go in pedals)
        {
            go.transform.eulerAngles = new Vector3(90, 0, 0);
        }

        //for (int i = 0; i < ribbonCount; i++)
        //{
        //    if(ribbons[i].transform.localPosition.z < -ribbonLength)
        //    {
        //        int last = (i == 0) ? ribbonCount - 1 : i - 1;
        //        ribbons[i].transform.localPosition = new Vector3(0, 0, ribbonLength)
        //        + ribbons[last].transform.localPosition;
        //    }
        //    ribbons[i].transform.Translate(new Vector3(1, 0, 0) * speed, Space.Self);
        //}

        //this.transform.Translate(Vector3.forward * speed, Space.Self);
        totalS += speed * Time.deltaTime;
        ribbon.GetComponent<MeshRenderer>().material.SetFloat("_Translation", totalS);
        //speedText.text = "Speed: " + speed;

        //mg.SetSpeed(speed);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed / 10.0f;
    }
}
