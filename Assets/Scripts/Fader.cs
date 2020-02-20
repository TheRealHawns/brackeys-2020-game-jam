using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    GameObject fader;

    public void Awake()
    {
        fader = GameObject.FindGameObjectWithTag("GameController");
    }

    public void FaderAnimation()
    {
        fader.GetComponent<GameManager>().OnFadeComplete();
    }
}
