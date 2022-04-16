using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> GrassBoxes;
    public List<GameObject> CurrentObjects;
    private GameObject startPos;

    public GameObject end;

    private float speed = 0;

    private float tailPos = 0;

    private float boxWidth;

    private bool generateEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = GameObject.FindGameObjectWithTag("StartPos");
        boxWidth = GrassBoxes[0].GetComponent<Renderer>().bounds.size.z;

        StartCoroutine(GenerateObject());

        end.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < CurrentObjects.Count; i++)
        {
            GameObject box = CurrentObjects[i];
            if(box.transform.position.z < -80)
            {
                CurrentObjects.RemoveAt(i);
                i--;
                Destroy(box);
            }
            else
            {
                box.transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.World);
            }
            tailPos = Mathf.Max(tailPos, box.transform.position.z);
        }
    }

    IEnumerator GenerateObject()
    {
        while (true)
        {
            if (speed > 3)
            {
                int blockNum = Random.Range(3, 5);
                for (int i = 0; i < blockNum; i++)
                {
                    int blockID = Random.Range(0, GrassBoxes.Count);
                    GameObject newBox = Instantiate(GrassBoxes[blockID]);

                    Vector3 newPos = startPos.transform.position;
                    newPos.z = tailPos + i * boxWidth;
                    newBox.transform.position = newPos;
                    //newBox.transform.DOMoveY(startPos.transform.position.y, 1);
                    CurrentObjects.Add(newBox);

                    if (i == 3 && generateEnd)
                    {
                        Vector3 endPos = newPos;
                        endPos.y = end.transform.position.y;
                        end.SetActive(true);
                        CurrentObjects.Add(end);
                        generateEnd = false;
                    }
                    int rand = Random.Range(0, 4);
                    if (rand < 2)
                    {
                        newBox.GetComponent<Platform_Moving>().enabled = false;
                    }
                }
                Debug.Log(Random.Range(4, 6) * boxWidth / speed);
                Debug.Log(boxWidth);
                yield return new WaitForSeconds(Random.Range(2, 3) * boxWidth / speed);
            }
           else
                yield return new WaitForSeconds(0.5f);
        }
    }

    public void SetSpeed(float inSpeed)
    {
        speed = inSpeed * 5;
    }

    public void GenerateEnd()
    {
        generateEnd = true;
    }
}
