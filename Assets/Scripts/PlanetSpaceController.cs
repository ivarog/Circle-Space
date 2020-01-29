using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpaceController : MonoBehaviour
{
    [SerializeField] GameObject ship;
    Rigidbody2D shipRB;

    private void Start() 
    {
        shipRB = ship.GetComponent<Rigidbody2D>();    
    }

    public void ZoomMainPLanetsEnded()
    {
        shipRB.simulated = true;
    }
}
