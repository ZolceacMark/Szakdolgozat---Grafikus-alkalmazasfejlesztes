using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class FirstRoundManager : MonoBehaviour
{
    [SerializeField] private ReflexRandomImage randomImageSpawner;

    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject pauseBackgroundPanel;
    [SerializeField] private GameObject completedScreen;
    [SerializeField] private GameObject failedScreen;

    [SerializeField] private GameObject greenColor;
    [SerializeField] private GameObject yellowColor;
    [SerializeField] private GameObject whiteColor;
    [SerializeField] private GameObject earlyButton;

    [SerializeField] private TMP_Text lengthText;
    [SerializeField] private TMP_Text avgReactionText;
    [SerializeField] private Image[] livesImage;

    private bool gameStarted;
    private bool gameEnded;
    private int length;
    private float minSpawnDelay = 1f;
    private float maxSpawnDelay = 2f;
    private float reactionTimeLimit = 10f;
    private bool timeOut;
    private Coroutine reactionCoroutine;
    private Coroutine whiteCoroutine;
    private int lives = 3;

    private int selectedLength;
    private List<float> greenReactions = new List<float>();
    private List<float> yellowReactions = new List<float>();
    private int correctWhiteReactions;
    private int wrongWhiteReactions;
    private float reactionTimer;  
    private bool failedGame;

    private void Update()
    {
        if (!gameStarted) return;

        if (Mouse.current.leftButton.wasPressedThisFrame && greenColor.gameObject.activeSelf)
        {
            greenReactions.Add((Time.time - reactionTimer) * 1000f);
            Debug.Log($"Green reaction time: {greenReactions[^1]:F0} ms");
            Dispawn();
            CorrectClick();
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame && yellowColor.gameObject.activeSelf)
        {
            yellowReactions.Add((Time.time - reactionTimer) * 1000f);
            Debug.Log($"Yellow reaction time: {yellowReactions[^1]:F0} ms");
            Dispawn();
            CorrectClick();
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame && earlyButton.gameObject.activeSelf)
            LoseLife();

        if ((Mouse.current.rightButton.wasPressedThisFrame && 
             greenColor.gameObject.activeSelf) ||
            (Mouse.current.leftButton.wasPressedThisFrame &&
             yellowColor.gameObject.activeSelf))
        {
            Dispawn();
            LoseLife(); 
            DecreaseLength();
        }

        if ((Mouse.current.leftButton.wasPressedThisFrame ||
             Mouse.current.rightButton.wasPressedThisFrame) &&
             whiteColor.gameObject.activeSelf)
        {
            wrongWhiteReactions++;

            Dispawn();
            LoseLife(); 
            DecreaseLength();
        }

    }
    
    private void Start()
    {

        bool showTutorial = PlayerPrefs.GetInt("ShowTutorial", 1) == 1;

        length = PlayerPrefs.GetInt("CourseLength", 25);
        selectedLength = length;
        lengthText.text = length.ToString();

        tutorialPanel.SetActive(showTutorial);
        gamePanel.SetActive(!showTutorial);

    }

    public void StartGame()
    {
        StartCoroutine(Spawner());
        gameStarted = true;
    }

    public void CorrectClick()
    {
        DecreaseLength();
        Dispawn();
    }

    private IEnumerator Spawner()
    {
        while (length > 0 && lives > 0)
        {   
            float randomFloat = Random.Range(minSpawnDelay, maxSpawnDelay);
            Debug.Log("Hatralevo masodperc: " + randomFloat);
            yield return new WaitForSeconds(randomFloat);

            randomImageSpawner.ShowRandomImage();
            reactionTimer = Time.time;
            earlyButton.gameObject.SetActive(false);  

            if (whiteColor.gameObject.activeSelf)
            {
                whiteCoroutine = StartCoroutine(WhiteReaction());
            }

            timeOut = true;
            reactionCoroutine = StartCoroutine(ReactionTimeout());

            Debug.Log("Waiting for player"); 
            yield return new WaitUntil(() => !timeOut);
        }
    }

    private IEnumerator ReactionTimeout()
    {
        yield return new WaitForSeconds(reactionTimeLimit);

        if (timeOut)
        {
            Debug.Log("Timeout");
            timeOut = false;
            Dispawn();
            LoseLife(); 
            DecreaseLength();   
        }            
    }

    private IEnumerator WhiteReaction()
    {
        Debug.Log("1.5s varakozas");
        yield return new WaitForSeconds(1.5f);
        correctWhiteReactions++;
        CorrectClick();
    }

    public void Dispawn()
    {
        if (reactionCoroutine != null)
        {
            StopCoroutine(reactionCoroutine);
            reactionCoroutine = null;
        }

        if (whiteCoroutine != null)
        {
            StopCoroutine(whiteCoroutine);
            whiteCoroutine = null;
        }

        timeOut = false;

        greenColor.gameObject.SetActive(false);
        yellowColor.gameObject.SetActive(false);
        whiteColor.gameObject.SetActive(false); 

        earlyButton.gameObject.SetActive(true);     
    }

    public void DecreaseLength()
    {
        if (length > 0)
        {
            length--;
            lengthText.text = length.ToString();
        }

        if (length == 0)
            EndGame();   
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            livesImage[lives].enabled = false;
            if (lives == 0)
            {
                failedGame = true;
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

    private void AvgReaction()
    {
        float a, b, c;
        a = greenReactions.Sum();
        b = yellowReactions.Sum();
        c = greenReactions.Count + yellowReactions.Count;

        if (c > 0)
        {
            avgReactionText.text = $"{((a+b) / c):F0} ms";
        }
        else
            avgReactionText.text = "-";

    }

    private void EndGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;
        SaveReactions();
        SaveWhiteReactions();
        SavePlayedGames();

        Time.timeScale = 0f;
        gamePanel.SetActive(false);
        endPanel.SetActive(true);  
        pauseBackgroundPanel.SetActive(true);
        failedScreen.SetActive(failedGame);
        completedScreen.SetActive(!failedGame);  
        AvgReaction();
    }

    public void ExitGame ()
    {   
        PlayerPrefs.SetInt("ShowTutorial",  PlayerPrefs.GetInt("OriginalShowTutorial", 0));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private List<float> LoadReactions(string key)
    {
        string json = PlayerPrefs.GetString(key, "{\"values\": []}");

        return JsonUtility.FromJson<ReactionData>(json).values;
    }

    private void SaveReactions()
    {
        List<float> savedGreenReactions = LoadReactions("ReflexGreenReactions"); 
        List<float> savedYellowReactions = LoadReactions("ReflexYellowReactions");

        savedGreenReactions.AddRange(greenReactions);
        savedYellowReactions.AddRange(yellowReactions);

        string greenJson = JsonUtility.ToJson(new ReactionData {values = savedGreenReactions});
        string yellowJson = JsonUtility.ToJson(new ReactionData {values = savedYellowReactions});

        PlayerPrefs.SetString("ReflexGreenReactions", greenJson);
        PlayerPrefs.SetString("ReflexYellowReactions", yellowJson);

        PlayerPrefs.Save(); 
    }

    private void SaveWhiteReactions()
    {
        int savedCorrect = PlayerPrefs.GetInt("ReflexCorrectWhite", 0);
        int savedWrong = PlayerPrefs.GetInt("ReflexWrongWhite", 0);

        correctWhiteReactions += savedCorrect;
        wrongWhiteReactions += savedWrong;

        PlayerPrefs.SetInt("ReflexCorrectWhite", correctWhiteReactions);
        PlayerPrefs.SetInt("ReflexWrongWhite", wrongWhiteReactions);

        PlayerPrefs.Save();
    }

    private void SavePlayedGames()
    {
        int totalGames = PlayerPrefs.GetInt("ReflexPlayedGames", 0);
        totalGames++;
        PlayerPrefs.SetInt("ReflexPlayedGames", totalGames);
        Debug.Log("Total played games: " + totalGames);

        string lengthKey = "Reflex" + selectedLength;
        int playedLength = PlayerPrefs.GetInt(lengthKey + "Played", 0);
        playedLength++;
        PlayerPrefs.SetInt(lengthKey + "Played", playedLength);
        Debug.Log("Total played games in this mode: " + playedLength);

        if (failedGame)
        {
            int totalFailed = PlayerPrefs.GetInt("ReflexFailedGames", 0);
            totalFailed++;
            PlayerPrefs.SetInt("ReflexFailedGames", totalFailed);

            int failedLength = PlayerPrefs.GetInt(lengthKey + "Failed", 0);
            failedLength++;
            PlayerPrefs.SetInt(lengthKey + "Failed", failedLength);
            Debug.Log("Total failed games: " + failedLength);
        }
        else
        {
            int totalCompleted = PlayerPrefs.GetInt("ReflexCompletedGames", 0);
            totalCompleted++;
            PlayerPrefs.SetInt("ReflexCompletedGames", totalCompleted);

            int completedLength = PlayerPrefs.GetInt(lengthKey + "Completed", 0);
            completedLength++;
            PlayerPrefs.SetInt(lengthKey + "Completed", completedLength);
            Debug.Log("Total completed games: " + completedLength);
        }

        PlayerPrefs.Save();
    }

}
