using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTheEnd : MonoBehaviour
{
    bool isEnd;
    public Transform stick;
    Player playerScript;

    private void Start()
    {
        playerScript = stick.GetComponent<Player>();
    }
    private void Update()
    {
        if (!isEnd)
        {
            Vector3 temp1 = transform.position;
            temp1.x = stick.position.x;
            transform.position = temp1;
        }
        

        if (isEnd)
        {
            Vector3 temp = stick.position;
            temp.z = -11f;
            temp.x = 0f;
            stick.position = Vector3.Lerp(stick.position, temp, Time.smoothDeltaTime);
            if (stick.position.z >= -10f)
                isEnd = false;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerScript.stopRotate = true;
            isEnd = true;
            print("trigger");
            if (col.GetComponent<Rigidbody>().velocity.y < 30)
            {
                col.GetComponent<Rigidbody>().velocity = Vector3.up * 50f;
            }
            if(col.GetComponent<Rigidbody>().velocity.y > 60)
            {
                col.GetComponent<Rigidbody>().velocity = Vector3.up * 60f;
            }
        }
    }
}
