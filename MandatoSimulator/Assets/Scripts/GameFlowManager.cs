using System.Collections;
using System.Collections.Generic;
using Padoru.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public GameObject EndGameInterface;
    public TextMeshProUGUI EndText;

    private IGameManager gameManager;

    private void Start()
    {
        gameManager = Locator.GetService<IGameManager>();
        gameManager.OnGameLost += GameEnded;
        gameManager.OnGameWon += GameEnded;
    }

    private void GameEnded(string message)
    {
        EndText.text = message;
        EndGameInterface.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
