using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControleUi : MonoBehaviour
{
    // balls menu
    public GameObject ballsMenuPanel;
    public GameObject choseBallPanel;
    // animation points pointsPower Messages
    public GameObject points;
    public GameObject pointsPower;
    public GameObject messages;
    List<string> Listmessages;
    float timer;
    bool stopAnimation;

    //filled ui
    public Text currentLevelTxt;
    public Text nexLevelTxt;
    public Image barLine;

    // score
    public Text score;
    int myscore;
    int scorePower;
    int scoreTotal;

    //best score
    public Text bestscore;

    // coin 
    public Text coinText;

    // player and circle positions
    public Transform player;
    public Transform circleEnd;

    //player script
    Player playerScript;

    // scene index
    int index;
    

    // Start is called before the first frame update
    void Start()
    {
        Listmessages = new List<string> { "Amazing" , "Insane" , "Perfect" , "Great"};
        playerScript = player.GetComponent<Player>();
        // current level
        currentLevelTxt.text = (SceneManager.GetActiveScene().buildIndex ).ToString();
        // next level
        nexLevelTxt.text = (SceneManager.GetActiveScene().buildIndex +1).ToString();
        // score
        score.text = GameManager.instant.getScore().ToString();
        // coin
        coinText.text = GameManager.instant.getCoin().ToString();
        //best score
        StartCoroutine(bestscorewait());
        // level number *** change depends scenes**********
        index = SceneManager.GetActiveScene().buildIndex;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (stopAnimation)
        {
            timer += Time.deltaTime;
            if(timer >= 2f)
            {
                timer = 0f;
                stopAnimation = false;
                points.GetComponent<Animator>().SetBool("simplePoint", false);
                pointsPower.GetComponent<Animator>().SetBool("pointspower", false);
                messages.GetComponent<Animator>().SetBool("message", false);
            }
        }
        
        barlineStatut();
        showScore();
    }

    void barlineStatut()
    {
        float amount = player.position.y / circleEnd.position.y;
        if (player.position.y >= circleEnd.position.y)
            amount = 1f;
        barLine.fillAmount = amount;
    }

    void showScore()
    {
        
        if (playerScript.showDistance)
        {
            // points power
            if (playerScript.activePower)
            {
                if (playerScript.distanceCurrentLast < 60f)
                    myscore = (5 * index * 10);
                if (playerScript.distanceCurrentLast >= 60f && playerScript.distanceCurrentLast < 80f)
                    myscore = (10 * index * 10);
                if (playerScript.distanceCurrentLast >= 80f)
                    myscore = (15 * index * 10);

                pointsPower.GetComponent<Text>().text = "+" + myscore;
                pointsPower.GetComponent<Animator>().SetBool("pointspower", true);
            }


            //simple points
            if (playerScript.distanceCurrentLast > 35f && playerScript.distanceCurrentLast < 60f)
                scorePower = (5 * index);
            if (playerScript.distanceCurrentLast >= 60f && playerScript.distanceCurrentLast < 80f)
                scorePower = (10 * index);
            if (playerScript.distanceCurrentLast >= 80f)
                scorePower = (15 * index);

            points.GetComponent<Text>().text = "+" + scorePower;
            if (playerScript.distanceCurrentLast > 35f)
                points.GetComponent<Animator>().SetBool("simplePoint", true);

            // messages
            if (playerScript.distanceCurrentLast >= 80f || playerScript.activePower)
            {
                messages.GetComponent<Text>().text = Listmessages[Random.Range(0, Listmessages.Count)];
                messages.GetComponent<Animator>().SetBool("message", true);
            }
                
            stopAnimation = true;
            playerScript.showDistance = false;

            //** game manager score **
            scoreTotal = myscore + scorePower + GameManager.instant.getScore();
            score.text = scoreTotal.ToString();
            GameManager.instant.setScore( scoreTotal);
            if (scoreTotal > GameManager.instant.getBestScore())
                GameManager.instant.setBestScore(scoreTotal);
        }
    }

    public void increaseCoin()
    {
        // game manager increase
        int rand = Random.Range(10,20);
        int coinTotal = GameManager.instant.getCoin() + rand;
        GameManager.instant.setCoin(coinTotal);
        coinText.text = coinTotal.ToString();
    }

    public void btnCHOSEBall()
    {
        playerScript.gameRun = false;
        choseBallPanel.SetActive(false);
        ballsMenuPanel.SetActive(true);
    }

    IEnumerator bestscorewait()
    {
        bestscore.text = "Best Score : " + GameManager.instant.getBestScore();
        yield return new WaitForSeconds(3f);
        bestscore.gameObject.SetActive(false);
    }

    public void RestartBTN()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
