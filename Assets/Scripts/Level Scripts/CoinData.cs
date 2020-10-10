using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinData : MonoBehaviour
{
    int startingInd;

    void Awake()
    {
        // if there's already one, then destroy this gameobject
        if (FindObjectsOfType<CoinData>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        startingInd = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        // if we're not in the current scene, then destroy the saved coindata info
        if (startingInd != SceneManager.GetActiveScene().buildIndex)
        {
            Destroy(gameObject);
        }
    }
}
