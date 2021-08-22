using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsFall : MonoBehaviour
{
    public GameObject player;
    bool Isbreak;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Isbreak)
        {
            if (player.transform.position.y > transform.position.y + 16f)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(gameObject, 4f);

                //childreen
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = new Vector3(4f, -10f, 0f);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-4f, -10f, 0f);
                Isbreak = true;
            }
            
        }
    }
}
