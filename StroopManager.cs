using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StroopManager : MonoBehaviour
{

    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private StroopTimer stroopTimer;
    [SerializeField] private GameVideoManager gameVideoManager; 
    [SerializeField] private GameObject newRecordText;

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject timerBar;
    [SerializeField] private GameObject timerCountdown;

    [SerializeField] private StroopButtonRandomizer tvColorRandomizer;
    [SerializeField] private StroopButtonRandomizer buttonRandomizer;
    [SerializeField] private Animator[] buttonAnimators;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;

    [SerializeField] StroopGameOverSFX stroopGameOverSFX;

    private bool gameStarted;
    private bool gameEnded;
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
        int totalPlayed = PlayerPrefs.GetInt("StroopTotalPlayed", 0) + 1;
        PlayerPrefs.SetInt("StroopTotalPlayed", totalPlayed);

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
        if (gameEnded)
            return;

        gameEnded = true;
        stroopGameOverSFX.PlayGameOverSFX();
        Time.timeScale = 0f;

        finalScoreText.text = score.ToString();
        if (score > PlayerPrefs.GetInt("StroopHighestScore", 0))
            newRecordText.SetActive(true);
        SaveStatistics();

        Debug.Log("---------------------");
        Debug.Log("Total Played: " + PlayerPrefs.GetInt("StroopTotalPlayed",0));
        Debug.Log("Total Score: " + PlayerPrefs.GetInt("StroopTotalScore",0));
        Debug.Log("Highest Score: " + PlayerPrefs.GetInt("StroopHighestScore",0));

        pausePanel.SetActive(true);
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
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("ShowTutorial",  PlayerPrefs.GetInt("OriginalShowTutorial", 0));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    private void SaveStatistics()
    {
        int totalScore = PlayerPrefs.GetInt("StroopTotalScore", 0);
        int highestScore = PlayerPrefs.GetInt("StroopHighestScore", 0);

        totalScore += score;

        if (score > highestScore)
            PlayerPrefs.SetInt("StroopHighestScore", score);

        PlayerPrefs.SetInt("StroopTotalScore", totalScore);

        PlayerPrefs.Save();   
    }
}
