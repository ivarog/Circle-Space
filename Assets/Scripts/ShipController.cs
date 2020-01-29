using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float rotationVelocity = 50f;
    [SerializeField] float travelVelocity = 50f;

    GameObject actualPlanet;
    bool shipOnOrbit = false;
    Vector3 actualPlanetBounds = Vector3.zero;
    Vector3 actualPlanetPosition = Vector3.zero;
    float lengthActualPlanetOrbit = 0f;
    float anglePlanetShip = 0f;
    bool firstTouch = false;
    Rigidbody2D myRb;

    private void Start() 
    {
        myRb = GetComponent<Rigidbody2D>();    
    }

    private void Update() 
    {
        if(shipOnOrbit)
            RotateOnOrbit();
        if(Input.GetKeyDown(KeyCode.Mouse0))
            ThrowShip();
    }

    void RotateOnOrbit()
    {
        if(firstTouch)
        {
            //Vector del centro del planeta hacia la nave
            Vector3 planetToShip = (this.transform.position - actualPlanetPosition).normalized;
            //Ángulo entre el planeta y el vector entrante de la nave
            anglePlanetShip = Vector3.Angle(planetToShip, Vector3.right);
            // Si es positivo el angulo es positivo y si es negativo el ángulo tmabién lo es
            float fAngle = Vector3.Cross(Vector3.right, planetToShip).z;    
            anglePlanetShip = fAngle < 0 ? -anglePlanetShip : anglePlanetShip;  
            lengthActualPlanetOrbit = actualPlanetBounds.x / 2 * 1.5f; 
            //Desactivamos la velocidad
            myRb.velocity = Vector3.zero;
            //Ya no es el primer toque de la nave en el planeta y lo desactivamos
            firstTouch = false;
        }

        
        float xPos = actualPlanetPosition.x + lengthActualPlanetOrbit * Mathf.Cos(anglePlanetShip * Mathf.Deg2Rad);
        float yPos = actualPlanetPosition.y + lengthActualPlanetOrbit * Mathf.Sin(anglePlanetShip * Mathf.Deg2Rad);
        transform.position = new Vector3(xPos, yPos, 0f);

        anglePlanetShip += rotationVelocity * Time.deltaTime;
    }

    void ThrowShip()
    {
        if(shipOnOrbit)
        {
            shipOnOrbit = false;
            Vector3 planetToShip = (this.transform.position - actualPlanetPosition).normalized;
            myRb.AddForce(planetToShip * travelVelocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Planet")
        {
            shipOnOrbit = true;
            actualPlanetBounds = other.gameObject.GetComponent<SpriteRenderer>().bounds.size;
            actualPlanetPosition = other.transform.position;
            firstTouch = true;
        }    
    }


}
