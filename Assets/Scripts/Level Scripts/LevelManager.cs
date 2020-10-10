using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadMainMenu()
    {
        PersistData.playerLives = PersistData.playerScore = 0;
        SceneManager.LoadScene(0);
    }
}
