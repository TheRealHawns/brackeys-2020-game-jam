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
    private GameObject playerHighScoreObject;
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
            playerHighScoreObject = GameObject.FindGameObjectWithTag("HighScore");
        }
        else
        {
            playerScore = playerScoreObject.GetComponent<TextMeshProUGUI>();
            playerScore.text = score.ToString();
            playerHighScoreObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("HighScore", 0).ToString();
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
        SoundManager.PlayMusic();
        StartCoroutine(SoundManager.WaitForFirstMusicLoop());
        
    }
    public void EndGame()
    {
        if (timer < 0)
        {

            Destroy(GameObject.Find("Music"));
            PlayerPrefs.GetInt("HighScore", 0);
            if (score >= PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
   
    public static IEnumerator WaitForFirstMusicLoop()
    {
        yield return new WaitUntil(() => GameObject.Find("Music").GetComponent<AudioSource>().isPlaying == false);
    }

}
