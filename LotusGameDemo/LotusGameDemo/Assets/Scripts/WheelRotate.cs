using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    public bool rotateRight;
    public bool rotateLeft;
    public float speed = 60f;

    public GameObject player;
    Player PlayerScript;
    Quaternion iniRot;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = player.GetComponent<Player>();
        iniRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateRight)
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        if(rotateLeft)
            transform.Rotate(Vector3.back * speed * Time.deltaTime);

        
    }

    private void LateUpdate()
    {
        if (PlayerScript.IsAnimate && transform.childCount > 0)
        {
            player.transform.rotation = iniRot;
        }
    }
}
