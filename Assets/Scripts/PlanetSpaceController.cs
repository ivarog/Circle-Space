using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpaceController : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] GameObject planet;
    [SerializeField] float spaceSpeed;
    Animator canvasAnimator;
    Rigidbody2D shipRB;
    int planetasCreados = 2;
    Queue<GameObject> planets = new Queue<GameObject>();
    public bool moveNextPlanet = false;
    Vector3 movementSum = Vector3.zero;

    private void Start() 
    {
        shipRB = ship.GetComponent<Rigidbody2D>();
        planets.Enqueue(transform.Find("Planet_1").gameObject);    
        planets.Enqueue(transform.Find("Planet_2").gameObject);
        canvasAnimator = GameObject.Find("Canvas").GetComponent<Animator>();    
    }

    private void Update() 
    {
        if(moveNextPlanet)
        {
            MovingNextPlanet();
        }    
    }

    public void ZoomMainPLanetsEnded()
    {
        shipRB.simulated = true;
        CreateNextPlanet();
        canvasAnimator.Play("Score In");
    }

    void CreateNextPlanet()
    {
        GameObject newPlanet = Instantiate(planet, gameObject.transform);
        float planetScale = Random.Range(0.5f, 1.0f);
        newPlanet.transform.localScale = new Vector3(planetScale, planetScale, 1f);
        float planetPos = Random.Range(-2f, 2f);
        if(planetPos + planetScale > 2f)
        {
            planetPos = 2f - planetScale; 
        }
        else if(planetPos - planetScale < -2f)
        {
            planetPos = -2f + planetScale; 
        }
        newPlanet.transform.position = new Vector3(planetPos, -9f, 0f);
        newPlanet.name = "Planet_" + ++planetasCreados;
        planets.Enqueue(newPlanet);    
    }

    void DestroyFirstPlanet()
    {
        GameObject firstPlanet = planets.Dequeue();
        Destroy(firstPlanet);
    }

    public void MovingNextPlanet()
    {
        Vector3 auxVector = Vector3.up * Time.deltaTime * spaceSpeed;
        movementSum += auxVector;
        foreach(GameObject planet in planets)
        {
            if(movementSum.y > 6.0f)
            {
                planet.transform.position += auxVector - (movementSum - new Vector3(0f, 6f, 0f));
            }
            else
            {
                planet.transform.position += auxVector;
            }
        }
        if(movementSum.y >= 6.0f)
        {
            moveNextPlanet = false;
            movementSum = Vector3.zero;
            DestroyFirstPlanet();
            CreateNextPlanet();
        }
    }
}
