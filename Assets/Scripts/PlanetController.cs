using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    float planetScale;
    int actualScore;
    bool rightMovement = true;
    bool movePlanet = false;
    float velocityMovement = 0f;

    private void Update() 
    {
        if(movePlanet)
        {
            PlanetTraslation(1f);
        }
    }

    public void InitPlanet(int createdPlanets)
    {
        PlanetScale();
        PlanetPosition(createdPlanets);
        PlanetRandomTraslation();
        PlanetName(createdPlanets);
    }

    void PlanetScale()
    {
        actualScore = FindObjectOfType<ScoreManager>().Score;    
        if(actualScore <= 5)
        {
            planetScale = Random.Range(0.7f, 0.8f);
        }
        else if(actualScore > 5 && actualScore <= 10)
        {
            planetScale = Random.Range(0.6f, 0.7f);
        }
        else if(actualScore > 10 && actualScore <= 20)
        {
            planetScale = Random.Range(0.5f, 0.6f);
        }
        else if(actualScore > 20 && actualScore <= 30)
        {
            planetScale = Random.Range(0.4f, 0.5f);
        }
        else if(actualScore > 30 && actualScore <= 50)
        {
            planetScale = Random.Range(0.3f, 0.4f);
        }
        else if(actualScore > 50)
        {
            planetScale = Random.Range(0.2f, 0.3f);
        }
        transform.localScale = new Vector3(planetScale, planetScale, 1f);
    }

    void PlanetRandomTraslation()
    {
        actualScore = FindObjectOfType<ScoreManager>().Score;
        if(actualScore > 3 && actualScore <= 10)
        {
            movePlanet = Random.Range(1, 5) == 1 ? true : false;
            velocityMovement = Random.Range(0.3f, 0.5f);
        }
        else if(actualScore > 10 && actualScore <= 20)
        {
            movePlanet = Random.Range(1, 4) == 1 ? true : false;
            velocityMovement = Random.Range(0.4f, 0.6f);
        }
        else if(actualScore > 20 && actualScore <= 30)
        {
            movePlanet = Random.Range(1, 3) == 1 ? true : false;
            velocityMovement = Random.Range(0.8f, 1f);
        }
        else if(actualScore > 30 && actualScore <= 50)
        {
            movePlanet = Random.Range(1, 2) == 1 ? true : false;
            velocityMovement = Random.Range(1f, 1.2f);
        }
        else if(actualScore > 50)
        {
            movePlanet = Random.Range(1, 2) == 1 ? true : false;
            velocityMovement = Random.Range(1.2f, 1.5f);
        }
    }

    void PlanetTraslation(float velocity)
    {
        float leftLimit = -2f + planetScale;
        float rightLimit = 2f - planetScale;
        if(rightMovement)
        {
            if(transform.position.x < rightLimit)
            {
                transform.position += new Vector3(velocity * Time.deltaTime, 0f, 0f);
            }
            else
            {
                rightMovement = false;
            }
        }
        else
        {
            if(transform.position.x > leftLimit)
            {
                transform.position -= new Vector3(velocity * Time.deltaTime, 0f, 0f);
            }
            else
            {
                rightMovement = true;
            }
        }
    }

    void PlanetPosition(int createdPlanets)
    {
        float planetPos = Random.Range(-2f, 2f);
        if(planetPos + planetScale > 2f)
        {
            planetPos = 2f - planetScale; 
        }
        else if(planetPos - planetScale < -2f)
        {
            planetPos = -2f + planetScale; 
        }
        Vector3 positionLastPlanet = GameObject.Find("Planet_" + (createdPlanets - 1).ToString()).transform.position;
        transform.position = new Vector3(planetPos, positionLastPlanet.y - 6f, 0f);
    }

    void PlanetName(int createdPlanets)
    {
        gameObject.name = "Planet_" + createdPlanets;
    }
       
}
