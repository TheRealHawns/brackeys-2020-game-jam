using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Singleton
    public static Timer instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] int GameLengthTime = 600;
    [HideInInspector] public int startGameLengthTime;

    Dictionary<int, Crop> PlantsWaterTime = new Dictionary<int, Crop>();
    TextMeshProUGUI timerText;


    private void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        startGameLengthTime = GameLengthTime;
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(CountDown());
    }

    public void ResumeTimer()
    {
        StartCoroutine(CountDown());
    }

    string GetSeconds()
    {
        var sec = (float)startGameLengthTime % 60;

        if (sec < 10) return "0" + sec.ToString();
        else return sec.ToString();
        
    }

    IEnumerator CountDown()
    {
        for (int i = 0; i <= startGameLengthTime; startGameLengthTime--)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (startGameLengthTime == 420)
            {
                Earthquake.instance.StartEarthquake();
            }
            timerText.text = string.Format("Time Remaining: {0} : {1}", Mathf.FloorToInt((float)startGameLengthTime/60), GetSeconds());

            if (PlantsWaterTime.ContainsKey(startGameLengthTime))
            {
                Crop crop = PlantsWaterTime[startGameLengthTime];
                crop.ResetWaterTimer();
                PlantsWaterTime.Remove(startGameLengthTime);
                //Debug.Log(string.Format("Reseting {0} Water Timer of {1} seconds", crop.gameObject.name, startGameLengthTime));

            }
        }
        //GameManager.instance.EndGame();
        // Debug.Log("ending game");
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    public int RequestTime()
    {
        return startGameLengthTime;
    }

    internal void Alarm(int time, Crop crop)
    {
        PlantsWaterTime.Add(time, crop);
    }


}
