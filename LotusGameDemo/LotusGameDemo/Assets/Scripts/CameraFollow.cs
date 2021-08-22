using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform theEnd;
    public Transform target;
    Vector3 ofsset;
    Player playerScript;
    Quaternion newQuat;
    // Start is called before the first frame update
    void Start()
    {
        ofsset = transform.position - target.position;
        playerScript  = target.GetComponent<Player>();
        newQuat = Quaternion.Euler(new Vector3(14f, -30.7f, 0f));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position.y < theEnd.position.y +15f && playerScript.gameRun)
        {
            Vector3 distance = target.position + ofsset;
            //distance.y = Mathf.Clamp(distance.y, 0, 400f);
            distance.z -= (playerScript.rotationZ * 2f);
            distance.z = Mathf.Clamp(distance.z, -70f, -60f);
            transform.position = Vector3.Lerp(transform.position, distance, 80 * Time.deltaTime);
        }

        if (playerScript.gameEnd)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3 (13.6f,transform.position.y,-32), 2f * Time.deltaTime);
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(14f, -30.7f, 0f), 2 * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, newQuat, 1f * Time.deltaTime);
        }
    }
}
