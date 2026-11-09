using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StroopManager : MonoBehaviour
{

    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private StroopTimer stroopTimer;
    [SerializeField] private GameVideoManager gameVideoManager; 

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject timerBar;
    [SerializeField] private GameObject timerCountdown;

    [SerializeField] private StroopButtonRandomizer tvColorRandomizer;
    [SerializeField] private StroopButtonRandomizer buttonRandomizer;
    [SerializeField] private Animator[] buttonAnimators;

    [SerializeField] private TMP_Text scoreText;

    private bool gameStarted;
    private int score = 0;


    private void Start()
    {
        bool showTutorial = PlayerPrefs.GetInt("ShowTutorial", 1) == 1;

        buttons.SetActive(false);
        timerBar.SetActive(false);
        timerCountdown.SetActive(false);

        tutorialPanel.SetActive(showTutorial);
        gamePanel.SetActive(!showTutorial);

        scoreText.text = "Score: 0";
    }

    public void StartGame()
    {
        if (gameStarted)
            return;
        
        gameStarted = true;

        buttons.SetActive(true);
        timerBar.SetActive(true);
        timerCountdown.SetActive(true);

        NextRound();
    }

    public void OnButtonClicked(int buttonIndex)
    {
        if (buttonRandomizer.IsCorrectButton(buttonIndex))
        {
            score++;

            scoreText.text = "Score: " + score;

            NextRound();
        }
        else
            EndGame();
    }

    private void NextRound()
    {
        gameVideoManager.ReplayVideo();
        PlayAnimations();

        tvColorRandomizer.RandomizeTvColor();
        buttonRandomizer.RandomizeButtons();

        stroopTimer.ResetTimer(RoundTime());
    }

    private float RoundTime()
    {
        if (score < 10)
            return 10f;

        int difficulty = (score - 10) / 5 + 1;

        float difficultyTimer = 10f - (difficulty * 0.75f);

        return Mathf.Max(3.5f, difficultyTimer);
    }

    private void PlayAnimations()
    {
        foreach (Animator animator in buttonAnimators)
        {
            animator.ResetTrigger("Glitch");
            animator.SetTrigger("Glitch");
        }
    }

    public void TimerEnded()
    {
        EndGame();
    }

    private void EndGame()
    {
        Time.timeScale = 0f;

        gamePanel.SetActive(false);
        endPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("ShowTutorial", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }    

    public void ExitGame()
    {
        PlayerPrefs.SetInt("ShowTutorial",  PlayerPrefs.GetInt("OriginalShowTutorial", 0));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }
}
