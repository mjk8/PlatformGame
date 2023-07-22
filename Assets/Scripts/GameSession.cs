using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [FormerlySerializedAs("score")] [SerializeField] private int totalCoins = 0;
    [SerializeField] private float loadDelay = 2f;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    void Awake()
    {
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = totalCoins.ToString();
    }

    public void CoinGettoDaze()
    {
        totalCoins += 1;
        if (totalCoins >= 10)
        {
            totalCoins -= 10;
            playerLives++;
            livesText.text = "+1 life!!";
            Invoke("lifePlusOne",1f);

        }
        scoreText.text = totalCoins.ToString();
    }

    void lifePlusOne()
    {
        livesText.text = playerLives.ToString();
    }

    public IEnumerator ProcessPlayerDeath()
    {
        yield return new WaitForSeconds(loadDelay);
        if (playerLives > 1)
        {
            playerLives--;
            livesText.text = playerLives.ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}
