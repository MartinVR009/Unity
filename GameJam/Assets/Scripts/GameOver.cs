using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI score_text;
    public Score score;
    public Ship_Controller controller;
    public Health health;

    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void Update()
    {
        Debug.Log("Health:" + health.slider.value);
        if (health.slider.value <= 0)
        {
            gameObject.SetActive(true);
            score_text.text = score.score_value.ToString();
        }
    }

    public void GameOverScreen()
    {
        gameObject.SetActive(true);
    }
    public void RestartButton(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ExitButton(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}