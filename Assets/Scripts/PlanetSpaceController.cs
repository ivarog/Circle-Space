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
    public int createdPlanets = 2;
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

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Llamada por evento en la animación Zoom Planet Space, inicia la simulación de la nave, crea el tercer planeta//
    // en escena y mete el score en escena                                                                          //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void ZoomMainPLanetsEnded()
    {
        shipRB.simulated = true;
        ship.GetComponent<TrailRenderer>().emitting = true;
        CreateNextPlanet();
        canvasAnimator.Play("Score In");
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Crea tercer planeta con tamaño y posición aleatoria y le coloca el nombre con el número de planeta que es    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void CreateNextPlanet()
    {
        GameObject newPlanet = Instantiate(planet, gameObject.transform);
        PlanetController planetController = newPlanet.GetComponent<PlanetController>();
        planetController.InitPlanet(++createdPlanets);
        planets.Enqueue(newPlanet);    
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Destruye el planeta que ha sido desplazado hacia arriba                                                      //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void DestroyFirstPlanet()
    {
        GameObject firstPlanet = planets.Dequeue();
        Destroy(firstPlanet);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Mueve el tercer planeta a escena destruye el viejo y crea uno nuevo                                          //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void MovingNextPlanet()
    {   
        Vector3 auxVector = Vector3.up * Time.deltaTime * spaceSpeed;
        movementSum += auxVector;
        if(movementSum.y >= 6.0f)
        {
            Camera.main.transform.position -= auxVector - (movementSum - new Vector3(0f, 6f, 0f));
            moveNextPlanet = false;
            movementSum = Vector3.zero;
            DestroyFirstPlanet();
            CreateNextPlanet();
        }
        else
        {
            Camera.main.transform.position -= auxVector;
        }
    }
}
