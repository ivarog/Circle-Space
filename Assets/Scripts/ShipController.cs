using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float rotationVelocity = 50f;
    [SerializeField] float travelVelocity = 50f;
    [SerializeField] GameObject planetsSpace;

    GameObject actualPlanet;
    bool shipOnOrbit = false;
    Vector3 actualPlanetBounds = Vector3.zero;
    float lengthActualPlanetOrbit = 0f;
    float anglePlanetShip = 0f;
    bool firstTouch = false;
    Rigidbody2D myRb;
    int rotateDirection = 0;
    PlanetSpaceController planetSpaceController;
    ScoreManager scoreManager;
    TrailRenderer trailRenderer;
    bool disableTrail = false;

    private void Start() 
    {
        myRb = GetComponent<Rigidbody2D>();    
        planetSpaceController = FindObjectOfType<PlanetSpaceController>();
        scoreManager = FindObjectOfType<ScoreManager>();
        Debug.Log(scoreManager);
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update() 
    {
        if(shipOnOrbit)
        {
            RotateOnOrbit();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
            ThrowShip();
        ShipOutLimits();

        if(disableTrail)
            SlowTrailDisable();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Mantiene a la nave en el planeta rotando, mueve la animación del siguiente planeta e incrementa el score     //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void RotateOnOrbit()
    {
        if(firstTouch)
        {
            if(actualPlanet.name != "Planet_1")
            {
                planetSpaceController.moveNextPlanet = true;
                scoreManager.IncreaseScore();   
            }
            //Vector del centro del planeta hacia la nave
            Vector3 planetToShip = (this.transform.position - actualPlanet.transform.position).normalized;
            //Ángulo entre el planeta y el vector entrante de la nave
            anglePlanetShip = Vector3.Angle(planetToShip, Vector3.right);
            // Si es positivo el angulo es positivo y si es negativo el ángulo tmabién lo es
            float fAngle = Vector3.Cross(Vector3.right, planetToShip).z;    
            anglePlanetShip = fAngle < 0 ? -anglePlanetShip : anglePlanetShip;  
            lengthActualPlanetOrbit = actualPlanetBounds.x / 2 * 1.5f; 
            //Desactivamos la velocidad
            myRb.velocity = Vector3.zero;
            rotateDirection = Random.Range(0, 2);
            //Aumento score
            //Ya no es el primer toque de la nave en el planeta y lo desactivamos
            firstTouch = false;
        }
        
        float xPos = actualPlanet.transform.position.x + lengthActualPlanetOrbit * Mathf.Cos(anglePlanetShip * Mathf.Deg2Rad);
        float yPos = actualPlanet.transform.position.y + lengthActualPlanetOrbit * Mathf.Sin(anglePlanetShip * Mathf.Deg2Rad);
        transform.position = new Vector3(xPos, yPos, 0f);

        if(rotateDirection == 0)
        {
            anglePlanetShip += rotationVelocity * Time.deltaTime;
        }
        else
        {
            anglePlanetShip -= rotationVelocity * Time.deltaTime;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Lanza en la nave en la dirección del vector resultante entre el planeta y la nave                            //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void ThrowShip()
    {
        if(shipOnOrbit)
        {
            shipOnOrbit = false;

            trailRenderer.time = 0.5f;
            disableTrail = false;

            transform.SetParent(planetsSpace.transform);
            Vector3 planetToShip = (this.transform.position - actualPlanet.transform.position).normalized;
            myRb.AddForce(planetToShip * travelVelocity);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Si la nave colisiona con la orbita de un planeta la nave se queda ahí y habilita el primer toque en él       //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Planet")
        {
            shipOnOrbit = true;
            actualPlanet = other.gameObject;

            disableTrail = true;

            transform.SetParent(actualPlanet.transform);
            actualPlanetBounds = other.gameObject.GetComponent<SpriteRenderer>().bounds.size;
            firstTouch = true;
        }    
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Desactiva el trail de la nave lentamente cuando esta toca un planeta                                         //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 
    void SlowTrailDisable() 
    {
        if(trailRenderer.time >= 0)
        {
            trailRenderer.time -= 0.1f * Time.deltaTime;
        }
        else
        {
            disableTrail = false;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Si la nave sale de los límites de la pantalla entonces termina la partida y desactiva el collider del tercer //
    // planeta                                                                                                      //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void ShipOutLimits()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        if(transform.position.y > (cameraPosition.y + 7f) || transform.position.y < (cameraPosition.y - 7f) || transform.position.x > (cameraPosition.x + 3f) || transform.position.x < (cameraPosition.x - 3f))
        {
            scoreManager.EndGame();
            GameObject.Find("Planet_" + (FindObjectOfType<PlanetSpaceController>().createdPlanets).ToString()).GetComponent<Collider2D>().enabled = false; 
        }
    }


}
