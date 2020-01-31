using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    [SerializeField] Animator buttonAnimator;
    [SerializeField] ParticleSystem planetsZoom;
    [SerializeField] GameObject planetsSpace;
    [SerializeField] Animator planetSpaceAnimator;
    [SerializeField] GameObject planet;
    [SerializeField] GameObject ship;

    bool momentToZoomMainPlanets = false;

    private void Start() 
    {
        planetSpaceAnimator.enabled = false;
        planetsSpace.transform.localScale = Vector3.zero;
    }

    public void FadeButton()
    {
        buttonAnimator.Play("Fade Button");
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
