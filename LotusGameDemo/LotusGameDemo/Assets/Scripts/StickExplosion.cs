using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickExplosion : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(-.5f, .5f), 0, 0);
    }
    
}
