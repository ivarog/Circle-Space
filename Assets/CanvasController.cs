using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public void InteractableButton()
    {
       GameObject.Find("Again Button").GetComponent<Button>().interactable = true;
    }
}
