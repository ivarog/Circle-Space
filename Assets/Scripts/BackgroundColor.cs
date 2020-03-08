using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    ScoreManager scoreManager;
    [SerializeField] Color color0 = new Color(0xFF, 0x1E, 0x24);
    [SerializeField] Color color10;
    [SerializeField] Color color20;
    [SerializeField] Color color30;
    [SerializeField] Color color40;
    [SerializeField] Color color50;
    [SerializeField] Color color60;
    [SerializeField] Color color70;
    [SerializeField] Color color80;
    [SerializeField] Color color90;
    [SerializeField] Color color100;
    Color colorA;
    Color colorB;
    Color[] arrayColor;
    [SerializeField] float timeTransition = 1.0f;
    bool canChangeColor = false;
    float timeCounter = 0;
    int actualIndexColor = 0;

    void OnEnable()
    {
        ScoreManager.OnIncreased += ScoreHasIncreased;
        ScoreManager.OnEndGame += ResetColor;
    }

    void OnDisable()
    {
        ScoreManager.OnIncreased -= ScoreHasIncreased;
        ScoreManager.OnEndGame -= ResetColor;
    }

    void ScoreHasIncreased()
    {
        // Debug.Log("He incrementado");
        ChangeColor(scoreManager.Score);
    }

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        arrayColor = new Color[]{color0, color10, color20, color30, color40, color50, color60, color70, color80, color90, color100};
    }

    // Update is called once per frame
    private void Update() 
    {
        if(canChangeColor)
        {
            SmooothTransitionColor(colorA, colorB);
        }
    }

    void ChangeColor(int score)
    {
        int length = arrayColor.Length;

        

        if(score % 5 == 0)
        {
            actualIndexColor++;

            if(actualIndexColor > length -1)
            {
                actualIndexColor = 0;
            }
            
            colorA = Camera.main.backgroundColor;
            colorB = arrayColor[actualIndexColor];
            canChangeColor = true;

        }
   
    }

    void SmooothTransitionColor(Color colorA, Color colorB)
    {
        
        if(timeCounter < timeTransition)
        {
            Camera.main.backgroundColor = Color.Lerp(colorA, colorB, timeCounter / timeTransition);
            timeCounter += Time.deltaTime;
        }
        else
        {
            Camera.main.backgroundColor = colorB;
            timeCounter = 0f;
            canChangeColor = false;
        }
    }

    void ResetColor()
    {
        colorA = Camera.main.backgroundColor;
        colorB = color0;
        canChangeColor = true;
    }

}
