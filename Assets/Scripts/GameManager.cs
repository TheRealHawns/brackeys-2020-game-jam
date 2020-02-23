using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score;
    GameObject time;
    int timer;
    GameObject playerScoreObject;
    TextMeshProUGUI playerScore;
    GameObject player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
 
        fader.gameObject.SetActive(true);

        SoundManager.Initialize();
    }
    
    public void Update()
    {
        if (time == null)
        {
            time = GameObject.FindGameObjectWithTag("Timer");
        }
        else
        {
            timer = time.GetComponent<Timer>().startGameLengthTime;
            EndGame();
        }
        if ( player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            score = player.GetComponent<PlayerInventory>().Score;
        }
        if (playerScoreObject == null)
        {
            playerScoreObject = GameObject.FindGameObjectWithTag("Score");
        }
        else
        {
            playerScore = playerScoreObject.GetComponentInChildren<TextMeshProUGUI>();
            playerScore.text = score.ToString();
        }
       
    }

    public Animator animator;
    private int levelToLoad;

    public Image fader;

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void EndGame()
    {
        if (timer < 0)
        {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
   

}
