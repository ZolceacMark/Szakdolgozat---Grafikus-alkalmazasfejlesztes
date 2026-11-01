using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MemoryManagerEasy : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject pauseBG;
    [SerializeField] private GameObject[] colorButtons;
    [SerializeField] private GameObject[] colorGlows;
    [SerializeField] private GameObject newRecordText;
    [SerializeField] private Image[] livesImage;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;

    private List<int> colors = new List<int>();

    private bool gameStarted;
    private int score = 0;
    private bool enableInput;
    private int lives = 3;
    private int currentInputIndex;

    public void Start()
    {
        bool showTutorial = PlayerPrefs.GetInt("ShowTutorial", 1) == 1;
    
        tutorialPanel.SetActive(showTutorial);
        gamePanel.SetActive(!showTutorial);

        scoreText.text = "Score: " + score.ToString();
    }

    public void StartGame()
    {   if (gameStarted)
            return;

        gameStarted = true;
        RandomColor();
        StartCoroutine(InitialFlashColor());
    }

    private void RandomColor()
    {
        int randomColor = Random.Range(0, colorButtons.Length);
        colors.Add(randomColor);
    }

    private IEnumerator InitialFlashColor()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(FlashColor());
    }

    private IEnumerator FlashColor()
    {
        enableInput = false;
        currentInputIndex = 0;

        foreach (int colorIndex in colors)
        {
            colorGlows[colorIndex].SetActive(true);
            yield return new WaitForSeconds(0.5f);

            colorGlows[colorIndex].SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }

        enableInput = true;
    }

    public void ColorClicked(int colorIndex)
    {
        if (!enableInput)
            return;

        if (colorIndex == colors[currentInputIndex])
        {
            currentInputIndex++;

            if (currentInputIndex == colors.Count)
            {
                enableInput = false;
                score++;
                scoreText.text = "Score: " + score.ToString();
                StartCoroutine(NextRound());
            }
        }
        else
        {
            enableInput = false;
            LoseLife();
            if (lives > 0)
            {
                StartCoroutine(ReplayRound());
            }
        }
    }

    private IEnumerator NextRound()
    {
        yield return new WaitForSeconds(1f);
        RandomColor();
        StartCoroutine(FlashColor());
    }

    private IEnumerator ReplayRound()
    {
        
        yield return new WaitForSeconds(1f);
        StartCoroutine(FlashColor());

    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            livesImage[lives].enabled = false;
            if (lives == 0)
            {
                EndGame();
            }
        }    
    }    

    public void RestartGame()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("ShowTutorial", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }    

    private void EndGame()
    {
        if (score > PlayerPrefs.GetInt("MemoryEasyHighestScore", 0))
        {
            PlayerPrefs.SetInt("MemoryEasyHighestScore", score);
            PlayerPrefs.Save();

            newRecordText.SetActive(true);
        }

        finalScoreText.text = score.ToString();
        Time.timeScale = 0f;
        pauseBG.SetActive(true);
        endPanel.SetActive(true);  
    }

}
