using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    [SerializeField] Animator buttonAnimator;
    [SerializeField] Text scoreUI;

    private void Start() 
    {
        score = 0;    
    }

    public void IncreaseScore()
    {
        score++;
        scoreUI.text = score.ToString();
        buttonAnimator.Play("IncreaseScore", -1, 0f);
    }

    public void EndGame()
    {
        Debug.Log("Game Ended");
        score = 0;    
        scoreUI.text = score.ToString();
        buttonAnimator.Play("Show Again Button");
        Destroy(GameObject.Find("Planets Space"), 1f);
    }

}
