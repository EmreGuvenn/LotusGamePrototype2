using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instant;

    private void Awake()
    {

        //for (int i = 0; i <= 19; i++)
        //{
        //    PlayerPrefs.DeleteKey("principalball" + i);
        //    PlayerPrefs.DeleteKey("activeball" + i);
        //}

        //PlayerPrefs.DeleteKey("firstTime");
        if (instant != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instant = this;
            DontDestroyOnLoad(gameObject);
        }

        gameFirstTime();
    }

    void Start()
    {

    }

    void gameFirstTime()
    {
        if (!PlayerPrefs.HasKey("firstTime"))
        {
            for (int i = 1; i < 20; i++)
            {
                PlayerPrefs.SetInt("activeball" + i, 0);
                PlayerPrefs.SetInt("principalball" + i, 0);
            }
            PlayerPrefs.SetInt("principalball0", 1);
            PlayerPrefs.SetInt("activeball0", 1);
            PlayerPrefs.SetInt("price", 100);

            PlayerPrefs.SetInt("bestscore", 0);
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("coin", 0);
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("firstTime", 1);

        }
    }

    public int getPlayerTryGame()
    {
        return PlayerPrefs.GetInt("firstTime");
    }

    public void setLevel(int lv)
    {
        PlayerPrefs.SetInt("level", lv);
    }
    public int getlevel()
    {
        return PlayerPrefs.GetInt("level");
    }

    public void setBestScore(int bestScore)
    {
        PlayerPrefs.SetInt("bestscore", bestScore);
    }
    public int getBestScore()
    {
        return PlayerPrefs.GetInt("bestscore");
    }

    public void setScore(int score)
    {
        PlayerPrefs.SetInt("score", score);
    }
    public int getScore()
    {
        return PlayerPrefs.GetInt("score");
    }

    public void setCoin(int coin)
    {
        PlayerPrefs.SetInt("coin", coin);
    }
    public int getCoin()
    {
        return PlayerPrefs.GetInt("coin");
    }

    public void setActiveBall(int nbr , int isactive)
    {
        PlayerPrefs.SetInt("activeball" + nbr, isactive);
    }
    public int getActiveBall(int nbr)
    {
        return PlayerPrefs.GetInt("activeball" + nbr);
    }
    public void setPricipalBall(int nbr, int principal)
    {
        PlayerPrefs.SetInt("principalball" + nbr, principal);
    }
    public int getPricipalball(int nbr)
    {
        return PlayerPrefs.GetInt("principalball" + nbr);
    }
    
    public void setPriceBall(int price)
    {
        PlayerPrefs.SetInt("price", price);
    }
    public int GetPriceBall()
    {
        return PlayerPrefs.GetInt("price");
    }
}
