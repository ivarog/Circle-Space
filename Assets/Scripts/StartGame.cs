using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour
{

    [SerializeField] Animator buttonAnimator;
    [SerializeField] ParticleSystem planetsZoom;
    [SerializeField] GameObject planetsSpace;
    [SerializeField] Animator planetSpaceAnimator;
    [SerializeField] GameObject planet;
    [SerializeField] GameObject ship;
    [SerializeField] Button againButton;

    bool momentToZoomMainPlanets = false;


    private void Start() 
    {
        planetSpaceAnimator.enabled = false;
        planetsSpace.transform.localScale = Vector3.zero;
        againButton.interactable = false;
    }

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
        planetsZoom.Play();
        Camera.main.orthographic = false;
        StartCoroutine(EntryMainPlanets());
    }

    IEnumerator EntryMainPlanets()
    {
        float seconds = planetsZoom.main.duration + 1.8f;
        yield return new WaitForSeconds(seconds);
        ZoomMainPLanets();
    }

    void ZoomMainPLanets()
    {
        Camera.main.orthographic = true;
        planetSpaceAnimator.enabled = true;
        planetSpaceAnimator.Play("Zoom Planet Space");
    }


}
