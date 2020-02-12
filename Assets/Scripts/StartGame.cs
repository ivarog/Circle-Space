using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour
{

    [SerializeField] Animator buttonAnimator;
    [SerializeField] ParticleSystem planetsZoomParticle;
    [SerializeField] GameObject planetsSpace;
    [SerializeField] Animator planetSpaceAnimator;
    [SerializeField] GameObject planet;
    [SerializeField] GameObject ship;
    [SerializeField] Button againButton;
    [SerializeField] Text maxScore;

    bool momentToZoomMainPlanets = false;


    private void Start() 
    {
        planetSpaceAnimator.enabled = false;
        planetsSpace.transform.localScale = Vector3.zero;
        againButton.interactable = false;
        maxScore.text = "Max: " + PlayerPrefs.GetInt("highScore" , 0).ToString();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Esta función es llamada cuuadno se da click en el botón play y el botón again, crea un planet space en caso  //
    // de no existir, aplica las animaciones a los botones y manda a llamar que se inicie el efecto de las          //
    // partículas. Cambia el valor de la cámara a perspectiva y manda a llamar la Corountine que traera al inicio   //
    // loa planetas proncipales.                                                                                    //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void FadeButton()
    {
        if(GameObject.Find("Planets Space") == null)
        {
            GameObject actualPlanetSpace =Instantiate(planetsSpace);
            actualPlanetSpace.name = "Planets Space";
            planetSpaceAnimator = actualPlanetSpace.GetComponent<Animator>();
            ship = actualPlanetSpace.transform.Find("Ship").gameObject;
            planetsSpace.transform.localScale = Vector3.zero;
        }
        if(EventSystem.current.currentSelectedGameObject.name == "Play Button")
        {
            buttonAnimator.Play("Fade Button");
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "Again Button")
        {
            buttonAnimator.Play("Fade Again Button");
            againButton.interactable = false;
        }
        planetsZoomParticle.Play();
        Camera.main.orthographic = false;
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
        StartCoroutine(EntryMainPlanets());
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Obtiene la duración de el sistema de partículas, espera y llama a los planetas principales a escena          //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    IEnumerator EntryMainPlanets()
    {
        float seconds = planetsZoomParticle.main.duration + 1.8f;
        yield return new WaitForSeconds(seconds);
        ZoomMainPLanets();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Regresa la cámara a la visión ortográfica, detiene todas las partículas, e inicia la animación donde se      //
    // muestran los planetas principales                                                                            //
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void ZoomMainPLanets()
    {
        Camera.main.orthographic = true;
        planetsZoomParticle.Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);
        planetSpaceAnimator.enabled = true;
        planetSpaceAnimator.Play("Zoom Planet Space");
    }
}
