using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    [SerializeField] float timeRemaining = 20f;
    [SerializeField] Text timerText;
    [SerializeField] GameObject player;  // get the Player object for teleport back to the start point. Otherwise, we don't need this! 
    [SerializeField] GameObject startPoint;
    [SerializeField] Text gameOverText;
    [SerializeField] GameObject startButton;

    public GameObject gameOverUI;
    public gameState myState = gameState.gamePause;
    private bool endGenerated = false;

    public enum gameState 
    {
        gameStart,
        gameEnd,
        gamePause
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void Awake()
    {
       instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining <= 0 && !endGenerated)
        { // Game over when time goes to 0
            GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>().GenerateEnd();
            endGenerated = true;
        }
        else
        {
            timerText.text = "Time left: " + Mathf.Floor(timeRemaining).ToString();
            timeRemaining -= Time.deltaTime;
        }
    }

    private void hotKey()
    {
        if(Input.GetKey(KeyCode.R)){ // teleport the Player to the start point
            player.transform.position = startPoint.transform.position;
            player.transform.rotation = Quaternion.Euler(0,0,0);
            player.GetComponent<PlayerController>().speed = 0;
        }
    }

    public static void GameOver(bool b)
    {
        Time.timeScale = 0f;
        instance.gameOverUI.SetActive(true); // Show Gameover view
        if(b==true){
            instance.gameOverText.text = "STAGE 1 PASS";
        }else{
            instance.gameOverText.text = "TIME UP";
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Debug.Log("this btn works!");
    }

    public void StartGame()
    {
        myState = gameState.gameStart;
        Time.timeScale = 1f;
        startButton.SetActive(false);
    }
}
