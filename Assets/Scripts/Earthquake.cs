using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Earthquake : MonoBehaviour
{

    public static Earthquake instance;

    private void Awake()
    {
        instance = this;
    }
    public GameObject SoilParent;
    public Soil[] soilArray;

    public void StartEarthquake()
    {
        soilArray = SoilParent.GetComponentsInChildren<Soil>();
        StartCoroutine(Quake());
    }

    IEnumerator Quake()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.state = State.Wait;
        Timer.instance.StopTimer();
        var cam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        var shake = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shake.m_FrequencyGain = 55f;

        yield return new WaitForSecondsRealtime(3f);

        shake.m_FrequencyGain = 0f;
        foreach (Soil soil in soilArray)
        {
            var coinFlip = Random.Range(0, 3);
            if (coinFlip <= 1)
            {
                soil.CreateHole();
            }
        }
        Timer.instance.ResumeTimer();
        player.state = State.Move;
    }

}