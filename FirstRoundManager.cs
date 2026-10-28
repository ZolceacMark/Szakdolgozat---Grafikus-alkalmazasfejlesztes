using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class FirstRoundManager : MonoBehaviour
{
    [SerializeField] private ReflexRandomImage randomImageSpawner;

    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private GameObject greenColor;
    [SerializeField] private GameObject yellowColor;
    [SerializeField] private GameObject whiteColor;
    [SerializeField] private GameObject earlyButton;

    [SerializeField] private TMP_Text lengthText;
    [SerializeField] private Image[] livesImage;

    private bool gameStarted;
    private int length;
    private float minSpawnDelay = 2f;
    private float maxSpawnDelay = 5f;
    private float reactionTimeLimit = 10f;
    private bool timeOut;
    private Coroutine reactionCoroutine;
    private Coroutine whiteCoroutine;
    private int lives = 3;

    private void Update()
    {

        if (!gameStarted) return;

        if (Mouse.current.leftButton.wasPressedThisFrame && greenColor.gameObject.activeSelf)
        {
            Dispawn();
            CorrectClick();
        }
        else if (Mouse.current.rightButton.wasPressedThisFrame && yellowColor.gameObject.activeSelf)
        {
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
            DecreaseLength();
            LoseLife();
        }

        if ((Mouse.current.leftButton.wasPressedThisFrame ||
             Mouse.current.rightButton.wasPressedThisFrame) &&
             whiteColor.gameObject.activeSelf)
        {
            Dispawn();
            DecreaseLength();
            LoseLife();
        }

    }
    
    private void Start()
    {

        bool showTutorial = PlayerPrefs.GetInt("ShowTutorial", 1) == 1;

        length = PlayerPrefs.GetInt("CourseLength", 25);
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
        //pontszam noveles
    }

    private IEnumerator Spawner()
    {
        while (length > 0 && lives > 0)
        {   
            float randomFloat = Random.Range(minSpawnDelay, maxSpawnDelay);
            Debug.Log("Hatralevo masodperc: " + randomFloat);
            yield return new WaitForSeconds(randomFloat);

            randomImageSpawner.ShowRandomImage();
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
            DecreaseLength();
            LoseLife();   
        }            
    }

    private IEnumerator WhiteReaction()
    {
        Debug.Log("5s varakozas!");
        yield return new WaitForSeconds(2f);
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
                EndGame();
            }
        }    
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        gamePanel.SetActive(false);
        endPanel.SetActive(true);    
    }

    public void ExitGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
