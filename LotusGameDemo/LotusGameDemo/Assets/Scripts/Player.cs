using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
   
    public Transform[] bonesStick;
    public Transform LastBone;
    public GameObject stick;
    public GameObject ball;
    Rigidbody rb;
    public float speedRotate;
    public bool resetStick;

    
    Vector2 startPosition, actualPosition;
    float DIstanceOnDrag;
    public float rotationZ;

    
    public bool CanBendStick;
   
    public GameObject FireParticle;
    public GameObject HitParticle;
    public GameObject HitParticle2;
    public GameObject Hole;
    public GameObject StickExplosion;
    public GameObject StickParticle;
    public GameObject boxExplosion;
    public bool stopRotate;
        
    public Material matPower;
    public Material matEmpty;
    public bool activePower;
    public bool activeFire;
    float timer;
    float timerFire;
    public float SpeedBall = 18f;
    public const float SpeedBallPower = 1.5f;
        
    public bool gameRun;
    public bool gameEnd;
    public bool IsAnimate; 
        
    float lastPositionY = 4.6f;
    public float distanceCurrentLast;
    public bool showDistance;
       
    public GameObject controleui;
    ControleUi controleuiScript;
       
    public GameObject choseballpanel;
        
    public GameObject restart;
    FloorDeath deathScript;
    
    void Start()
    {
        deathScript = restart.GetComponent<FloorDeath>();
        controleuiScript = controleui.GetComponent<ControleUi>();
        gameRun = true;
        rb = GetComponent<Rigidbody>();
        CanBendStick = true;

       
    }

   
    void Update()
    {
        if (!gameRun)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
           
            if(!CanBendStick)
            detectTypeOfObject();
        }
        else if (Input.GetMouseButton(0))
        {
            BendStick();
        }
        if (Input.GetMouseButtonUp(0))
        {
            shootingBall();
        }
       
        rotateBall();

        
        //ResetBallFire();
        Debug.DrawRay(transform.position, Vector3.forward *10 , Color.red);
        
    }

    
    void BendStick()
    {
        if (!CanBendStick)
            return;
        actualPosition = Input.mousePosition;
        DIstanceOnDrag = startPosition.y - actualPosition.y;
        rotationZ += DIstanceOnDrag * Time.deltaTime * speedRotate;
        rotationZ = Mathf.Clamp(rotationZ, 0f, 5.2f);
        for (int i = 0; i < bonesStick.Length; i++)
        {

            bonesStick[i].localEulerAngles = new Vector3(bonesStick[i].localEulerAngles.x, bonesStick[i].localEulerAngles.y, rotationZ);
            //bonesStick[i].Rotate(Vector3.forward * Time.deltaTime * DIstanceOnDrag * speedRotate);
        }
        ball.transform.position = new Vector3(ball.transform.position.x, LastBone.position.y - .2f, ball.transform.position.z);
        
        startPosition = actualPosition;
    }

    void shootingBall()
    {
        if (!CanBendStick)
            return;
        
        if (rotationZ > 1.8f)
        {
           
            if (IsAnimate)
            {
                IsAnimate = false;
                transform.parent = null;
            }

            
            if (choseballpanel.activeSelf)
                choseballpanel.SetActive(false);

           
            if (activePower)
            {
                rb.velocity = Vector3.up * SpeedBall * rotationZ * SpeedBallPower;
                print(Vector3.up * SpeedBall * rotationZ * SpeedBallPower);
            }

            else
                rb.velocity = Vector3.up * SpeedBall * rotationZ;

         
            CanBendStick = false;
           
            rb.isKinematic = false;
          
           // ballpowerFireActive();
            
            ballPowerLightReset();
                      
            stick.SetActive(false);
        }
        ball.transform.localPosition = Vector3.zero;
        rotationZ = 0f;
        for (int i = 0; i < bonesStick.Length; i++)
        {
            bonesStick[i].localRotation = Quaternion.identity;
        }
    }

    void detectTypeOfObject()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9))
        {
            if(hit.collider.tag == "ground")
            {
               // calculDistance();
                CanBendStick = true;
                stick.SetActive(true);
                rb.isKinematic = true;
                // hole
                GameObject go = Instantiate(Hole, hit.point, Hole.transform.rotation);
                //sound
                // particle System
                GameObject go2 = Instantiate(HitParticle2, hit.point, HitParticle.transform.rotation);

            }
            else if(hit.collider.tag == "power")
            {
                activePower = true;
                calculDistance();
                CanBendStick = true;
                stick.SetActive(true);
                rb.isKinematic = true;
                // hole
                GameObject go = Instantiate(Hole, hit.point, Hole.transform.rotation);
                //sound
                // particle System
                GameObject go2 = Instantiate(HitParticle2, hit.point, HitParticle.transform.rotation);
            }
            else if (hit.collider.tag == "subpower")
            {
               calculDistance();
                CanBendStick = true;
                stick.SetActive(true);
                rb.isKinematic = true;
                // hole
                GameObject go = Instantiate(Hole, hit.point, Hole.transform.rotation);
                //sound
                // particle System
                GameObject go2 = Instantiate(HitParticle2, hit.point, HitParticle.transform.rotation);
            }
            else if(hit.collider.tag == "solid")
            {
                rb.velocity = Vector3.zero;
                
                GameObject go = Instantiate(HitParticle , hit.point , HitParticle.transform.rotation);
                StartCoroutine(showstickSecond());
                
            }
            else if (hit.collider.tag == "enemy")
            {
                gameRun = false;
                stick.SetActive(false);
                rb.velocity = Vector3.zero;
                stopRotate = true;
                GameObject go = Instantiate(StickExplosion, transform.position, transform.rotation);
                GameObject go2 = Instantiate(StickParticle, hit.point, StickParticle.transform.rotation);
                GameManager.instant.setScore(0);
               
                Invoke("restartLevel", .5f);
            }
            else if (hit.collider.tag == "explosion")
            {
                rb.velocity = Vector3.zero;
                StartCoroutine(showstickSecond());
                Destroy(hit.transform.gameObject);
                GameObject go = Instantiate(boxExplosion, hit.point, boxExplosion.transform.rotation);
            }
            
            else if(hit.collider.tag == "animation")
            {
                IsAnimate = true;
               calculDistance();
                CanBendStick = true;
                stick.SetActive(true);
                rb.isKinematic = true;
                
                GameObject go = Instantiate(Hole, hit.point, Hole.transform.rotation);
                
                GameObject go2 = Instantiate(HitParticle2, hit.point, HitParticle.transform.rotation);
                go.transform.SetParent(hit.transform);
                transform.SetParent(hit.transform);
            }
        }
        else
        {
            StartCoroutine(showstickSecond());
        }
    }
    
    IEnumerator showstickSecond()
    {
        stick.SetActive(true);
        yield return new WaitForSeconds(.2f);
        stick.SetActive(false);
    }
    
    void rotateBall()
    {
        if (!CanBendStick && !stopRotate)
            ball.transform.Rotate(Vector3.right * Time.deltaTime * 600f);
    }
   
    void ballPowerLightReset()
    {
        activePower = false;
        if (!activePower)
            ball.gameObject.GetComponent<MeshRenderer>().material = matEmpty;
    }
    //void ballpowerFireActive()
    //{
    //    if (activePower)
    //        activeFire = true;
    //    if (activeFire)
    //    {
          
    //        ball.gameObject.GetComponent<TrailRenderer>().enabled = false;
    //    }
    //}
    //void ResetBallFire()
    //{
    //    if (activeFire)
    //    {
    //        timerFire += Time.deltaTime;
    //        if (rb.velocity.y <= 5f && timerFire >= 1f)
    //        {
    //            activeFire = false;
               
    //            ball.gameObject.GetComponent<TrailRenderer>().enabled = true;
    //            timerFire = 0f;
    //        }
    //    }
            
    //}
    
    
    void calculDistance()
    {
        distanceCurrentLast = (transform.position.y) - lastPositionY;
        lastPositionY = transform.position.y;
        showDistance = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "end")
        {
            gameEnd = true;
            print("finaaaaaaaal");
            
        }
    }

    void nextLevel()
    {
        GameManager.instant.setLevel(GameManager.instant.getlevel() + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void restartLevel()
    {
        deathScript.Restart();
       
    }
}
