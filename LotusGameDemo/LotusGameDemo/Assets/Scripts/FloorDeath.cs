using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDeath : MonoBehaviour
{
    public GameObject restartPanel;
    public GameObject player;
    Player playerscript;
    // Start is called before the first frame update
    void Start()
    {
        playerscript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.instant.setScore(0);
            playerscript.gameRun = false;
            Invoke("Restart" , .5f);
        }
    }

    public void Restart()
    {
        restartPanel.SetActive(true);
    }
}
