using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PersistData : MonoBehaviour
{
    // field declarations
    public static int playerLives { get; set; } = 3;
    public static int playerScore { get; set; } = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;


    // Start is called before the first frame update
    void Start ()
    {
        // Singleton pattern
        if (FindObjectsOfType<PersistData> ().Length > 1)
        {
            Destroy (gameObject);
        }
        else
        {
            DontDestroyOnLoad (gameObject);
        }
    }

    void Update ()
    {
        scoreText.text = playerScore.ToString();
        livesText.text = playerLives.ToString();
    }

    // call in Points.cs
    public void AddPointsToPlayerScore (int points)
    {
        playerScore += points;
    }

    public void TakeLives ()
    {
        if (playerLives > 1)
        {
            --playerLives;
            ResetLevel();
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        playerLives = 3;
        playerScore = 0;
        // main menu is set at index 0
        SceneManager.LoadScene(0);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}