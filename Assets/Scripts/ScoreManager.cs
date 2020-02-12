using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    public int Score
    {
        get{return score;}
    }

    [SerializeField] Animator buttonAnimator;
    [SerializeField] Text scoreUI;

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
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Si el juego termina regresa el score a 0, muestra el botón de again y destruye el planet space actual        //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void EndGame()
    {
        Debug.Log("Game Ended");
        if(score > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", score);
            GameObject.Find("Max Score").GetComponent<Text>().text = "Max: " + PlayerPrefs.GetInt("highScore").ToString();
        }
        score = 0;    
        scoreUI.text = score.ToString();
        buttonAnimator.Play("Show Again Button");
        Destroy(GameObject.Find("Planets Space"), 1f);
    }

}
