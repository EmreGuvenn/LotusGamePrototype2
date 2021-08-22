using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMeshController : MonoBehaviour
{
    public GameObject[] ballsMesh;
    int principal;
    // Start is called before the first frame update
    void Start()
    {
        
        // active ball mesh
        for (int j = 0; j < ballsMesh.Length; j++)
        {
            if (GameManager.instant.getPricipalball(j) == 1)
                principal = j;
            ballsMesh[j].SetActive(false);
        }
        ballsMesh[principal].SetActive(true);
    }

}
