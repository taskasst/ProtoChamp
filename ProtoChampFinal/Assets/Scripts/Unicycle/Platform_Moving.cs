using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform_Moving : MonoBehaviour
{
    public Vector3 stopPos;
    private float moveTime =1f;
    private float downStayTime =1f;
    private float upStayTime =3f;
    public float fallDepth = 5f;
    private Vector3 startPos;
    private float moveSpeed;
    private bool toStop  = true;

    [SerializeField] bool on = true;
    
    void Start()
    {
        startPos = GameObject.FindGameObjectWithTag("StartPos").transform.position;
        stopPos = startPos;
        stopPos.y = startPos.y + fallDepth;
        moveSpeed = Vector3.Distance(startPos,stopPos)/moveTime;

        StartCoroutine(PlatformMove(stopPos));
    }

    void Update()
    {
        //PlatformSwitch(on);
       
    }

    void PlatformSwitch(bool on){
        if(!on){return;}
		StartCoroutine(PlatformMove(stopPos));

    }

    IEnumerator PlatformMove( Vector3 stopPostion){
        while (true)
        {
            transform.DOMoveY(stopPos.y, moveTime);
            yield return new WaitForSeconds(upStayTime + moveTime);
            transform.DOMoveY(startPos.y, moveTime);
            yield return new WaitForSeconds(downStayTime + moveTime);
        }
  //      if (toStop ){
		//	tempPosition = Vector3.MoveTowards(tempPosition, stopPostion, moveSpeed*Time.deltaTime);
		//	transform.position = tempPosition;
		//	if (transform.position == stopPostion){
		//		yield return new WaitForSeconds(stayTime);
		//		toStop  = false;
		//	}
		//}
		//else if (!toStop ){
		//	tempPosition = Vector3.MoveTowards(tempPosition, startPos, moveSpeed*Time.deltaTime);
		//	transform.position = tempPosition;
		//	if (transform.position == startPos){
		//		yield return new WaitForSeconds(stayTime);
		//		toStop  = true;
		//	}
		//}
	}
}
