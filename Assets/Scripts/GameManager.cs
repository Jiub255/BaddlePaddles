using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int leftScore;
    public int rightScore;
    public Text leftScoreText;
    public Text rightScoreText;

    public int level = 1;
    public Text levelText;

    //public bool gameOver;
    public GameObject gameOverPanel;
    //public Button playButton;
    //public Button quitButton;

    public int numberOfBricks;

    //why is this an array of transforms instead of GameObjects? 
    //why do i instantiate transforms, but have to destroy the gameobject its attached to?
    //doesn't work when i change it to gameobjects
    public Transform[] levels;
    public int currentLevelIndex = 0;

    public GameObject ball;
    public GameObject ballRight;
    private BallScript ballScript;
    private BallScriptRighty ballScriptRighty;

    private Transform currentLevel;

    public void Start()
    {
        //maybe put in awake so its called before start counts bricks?
        levelText.text = "Level " + level;

        currentLevel = Instantiate(levels[currentLevelIndex], Vector3.zero, Quaternion.identity);

        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;

        ballScript = ball.GetComponent<BallScript>();
        ballScriptRighty = ballRight.GetComponent<BallScriptRighty>();

    }


    public void UpdateLeftScore(int points)
    {
        leftScore += points;

        leftScoreText.text = "Purpley: " + leftScore;
    }

    public void UpdateRightScore(int points)
    {
        rightScore += points;

        rightScoreText.text = "Greeney: " + rightScore;
    }
    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)//gets called multiple times if i cheat and get negative numberofbricks
        {                
            Destroy(currentLevel.gameObject, 2f);
            //its not destroying the level instance, why? it is now, but why?

            if (currentLevelIndex >= levels.Length - 1)//not sure about here, why is it getting called early?
            {
                Debug.Log("Jame Over");
                gameOverPanel.SetActive(true);
                //playButton.gameObject.SetActive(true);
                //quitButton.gameObject.SetActive(true);

                //gameOver = true;
                //GameOver();
            }
            else if (currentLevelIndex < levels.Length - 1)
            {
                //gameOver = true;
                Invoke("LoadLevel", 2f);
                //want to go to a menu between levels where you can spend money and upgrade your paddle or whatever, instead of load level right away
            }
        }
    }

    void LoadLevel()
    {
        level++;
        levelText.text = "Level " + level;
        currentLevelIndex++;
        currentLevel = Instantiate(levels[currentLevelIndex], Vector3.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;

        //gameOver = false;
        ballScript.inPlay = false;
        ballScript.leftDeath = false;
        ballScriptRighty.inPlay = false;
        ballScriptRighty.rightDeath = false;
        //levelText.enabled = true;
        //timeToDisappear = 2.0f;

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Start Menu");
    }

}
