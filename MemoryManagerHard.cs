using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MemoryManagerHard : MonoBehaviour
{

    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject pauseBG;
    [SerializeField] private GameObject[] colorButtons;
    [SerializeField] private GameObject[] colorGlows;
    [SerializeField] private GameObject newRecordText;
    [SerializeField] private GameObject clickText;
    [SerializeField] private GameObject buttonText;
    [SerializeField] private Image[] livesImage;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private CorrectWrongSFX corretWrongSFX;
    [SerializeField] private AudioClip flashColorSFX;

    private enum InputType{
        Click,
        Keyboard
    }

    private List<int> colors = new List<int>();
    private List<InputType> inputTypes = new List<InputType>();

    private bool gameStarted;
    private int score = 0;
    private bool enableInput;
    private int lives = 3;
    private int currentInputIndex;

    private void Update()
    {
        if (Time.timeScale == 0f || !enableInput) 
            return;

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            SubmitInput(0, InputType.Keyboard);
        }
        else if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            SubmitInput(1, InputType.Keyboard);
        }
        else if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            SubmitInput(2, InputType.Keyboard);
        }
        else if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SubmitInput(3, InputType.Keyboard);
        }
        else if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SubmitInput(4, InputType.Keyboard);
        }
        else if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            SubmitInput(5, InputType.Keyboard);
        }
    }

    public void Start()
    {
        bool showTutorial = PlayerPrefs.GetInt("ShowTutorial", 1) == 1;
    
        tutorialPanel.SetActive(showTutorial);
        gamePanel.SetActive(!showTutorial);

        scoreText.text = "Score: " + score.ToString();
        clickText.SetActive(false);
        buttonText.SetActive(false);
    }

    public void StartGame()
    {   if (gameStarted)
            return;

        gameStarted = true;

        int totalPlayed = PlayerPrefs.GetInt("MemoryHardTotalPlayed", 0);
        PlayerPrefs.SetInt("MemoryHardTotalPlayed", totalPlayed + 1);
        PlayerPrefs.Save();

        int overallPlayed = PlayerPrefs.GetInt("MemoryOverallPlayedGames", 0);
        PlayerPrefs.SetInt("MemoryOverallPlayedGames", overallPlayed);
        PlayerPrefs.Save();

        RandomColor();
        StartCoroutine(InitialFlashColor());
    }

    private void RandomColor()
    {
        int randomColor = Random.Range(0, colorButtons.Length);

        if (colors.Count >= 2 && colors[colors.Count - 1] == colors[colors.Count - 2])
            {
                int blockedColor = colors[colors.Count - 1];

                while (randomColor == blockedColor)
                {
                    randomColor = Random.Range(0, colorButtons.Length);
                }
            }

        InputType randomInput = (InputType) Random.Range(0, 2);
        colors.Add(randomColor);
        inputTypes.Add(randomInput);
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

        for (int i = 0; i < colors.Count; i++)
        {
            int colorIndex = colors[i];
            InputType input = inputTypes[i];
            colorGlows[colorIndex].SetActive(true);
            SFXManager.instance.PlaySFXClip(flashColorSFX, transform, 1f);
            if (input == InputType.Click)
            {
                clickText.SetActive(true);
            }
            else buttonText.SetActive(true);
            
            yield return new WaitForSeconds(0.5f);

            colorGlows[colorIndex].SetActive(false);
            clickText.SetActive(false);
            buttonText.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }

        enableInput = true;
    }

    public void ColorClicked(int colorIndex)
    {
        if (!enableInput)
            return;

        SubmitInput(colorIndex, InputType.Click);
    }
    
    private IEnumerator ClickGlow(int colorIndex)
    {
        colorGlows[colorIndex].SetActive(true);
        yield return new WaitForSeconds(0.15f);
        colorGlows[colorIndex].SetActive(false);
    }   

    private void SubmitInput(int colorIndex, InputType input)
    {
        if (!enableInput)
            return;

        StartCoroutine(ClickGlow(colorIndex));

        if (input == InputType.Click)
        {
            int totalClick = PlayerPrefs.GetInt("MemoryHardTotalClick", 0);
            PlayerPrefs.SetInt("MemoryHardTotalClick", totalClick + 1);
        }
        else
        {
            int totalButton = PlayerPrefs.GetInt("MemoryHardTotalButton", 0);
            PlayerPrefs.SetInt("MemoryHardTotalButton", totalButton + 1);
        }
        PlayerPrefs.Save();

        int expectedColor = colors[currentInputIndex];
        InputType expectedInput = inputTypes[currentInputIndex];

        if (colorIndex == expectedColor && expectedInput == input)
        {
            currentInputIndex++;
            corretWrongSFX.PlayCorrectSFX();

            if (currentInputIndex == colors.Count)
            {
                enableInput = false;
                
                score++;
                scoreText.text = "Score: " + score.ToString();

                int totalCompleted = PlayerPrefs.GetInt("MemoryHardCompletedRounds", 0);
                PlayerPrefs.SetInt("MemoryHardCompletedRounds", totalCompleted + 1);
                PlayerPrefs.Save();

                StartCoroutine(NextRound());
            }
        }
        else
        {
            int totalMistakes = PlayerPrefs.GetInt("MemoryHardTotalMistakes", 0);
            PlayerPrefs.SetInt("MemoryHardTotalMistakes", totalMistakes + 1);

            if (input == InputType.Click)
            {
                int clickMistakes = PlayerPrefs.GetInt("MemoryHardClickMistakes", 0);
                PlayerPrefs.SetInt("MemoryHardClickMistakes", clickMistakes + 1);
            }
            else
            {
                int buttonMistakes = PlayerPrefs.GetInt("MemoryHardButtonMistakes", 0);
                PlayerPrefs.SetInt("MemoryHardButtonMistakes", buttonMistakes + 1);
            }
            PlayerPrefs.Save();
            
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
            if (lives > 1)
            {
                corretWrongSFX.PlayWrongSFX();
            }
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
        corretWrongSFX.PlayGameOverSFX();
        if (score > PlayerPrefs.GetInt("MemoryHardHighestScore", 0))
        {
            PlayerPrefs.SetInt("MemoryHardHighestScore", score);
            PlayerPrefs.Save();

            newRecordText.SetActive(true);
        }
        SaveModeScoreStats("MemoryHard");
        finalScoreText.text = score.ToString();
        Time.timeScale = 0f;
        pauseBG.SetActive(true);
        endPanel.SetActive(true);  

        Debug.Log("===== MEMORY HARD STATISTICS =====");

        Debug.Log("Total Played: " +
            PlayerPrefs.GetInt("MemoryHardTotalPlayed", 0));

        Debug.Log("Completed Rounds: " +
            PlayerPrefs.GetInt("MemoryHardCompletedRounds", 0));

        Debug.Log("Total Mistakes: " +
            PlayerPrefs.GetInt("MemoryHardTotalMistakes", 0));

        Debug.Log("Highest Score: " +
            PlayerPrefs.GetInt("MemoryHardHighestScore", 0));    
    }

    private void SaveModeScoreStats(string mode)
    {
        int totalScore = PlayerPrefs.GetInt(mode + "TotalScore", 0);
        totalScore += score;
        PlayerPrefs.SetInt(mode + "TotalScore", totalScore);

        int overallHighScore = PlayerPrefs.GetInt("MemoryOverallHighestScore", 0);

        if (score > overallHighScore)
        {
            PlayerPrefs.SetInt("MemoryOverallHighestScore", score);
        }   

        PlayerPrefs.Save();
    }

}

