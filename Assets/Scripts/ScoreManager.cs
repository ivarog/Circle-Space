using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Para manejar eventos de score
    public delegate void IncreasedScore();
    public static event IncreasedScore OnIncreased;
    public delegate void EndingGame();
    public static event EndingGame OnEndGame;

    int score = 0;
    public int Score
    {
        get{return score;}
    }

    [SerializeField] Animator buttonAnimator;
    [SerializeField] Text scoreUI;
    [SerializeField] Animator canvasAnimator;
    [SerializeField] GameObject maxScore;
    [SerializeField] GameObject points;

    private void Start() 
    {
        score = 0;    
        
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Incrementa el score actual y lo muestra en pantalla                                                          //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void IncreaseScore()
    {
        score++;
        scoreUI.text = score.ToString();
        buttonAnimator.Play("IncreaseScore", -1, 0f);
        if(OnIncreased != null)
            OnIncreased();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Si el juego termina regresa el score a 0, muestra el botón de again y destruye el planet space actual        //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void EndGame()
    {
        Debug.Log("Game Ended");
        if(OnEndGame != null)
            OnEndGame();

        buttonAnimator.Play("Show Again Button");
        maxScore.SetActive(true);
        points.SetActive(true);
        canvasAnimator.Play("MxScoreIn");
        
        if(score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", score);
        }
        GameObject.Find("Points").GetComponent<Text>().text = PlayerPrefs.GetInt("highScore").ToString();
        score = 0;    
        scoreUI.text = score.ToString();

        Destroy(GameObject.Find("Planets Space"), 0.5f);
    }

}
