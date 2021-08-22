using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSubObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.down * 40f;
        }
    }
}