using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MemoryManagerEasy : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject[] colorButtons;
    [SerializeField] private GameObject[] colorGlows;
    [SerializeField] private Image[] livesImage;

    private List<int> colors = new List<int>();

    private int score;
    private bool enableInput;
    private int lives = 3;
    private int currentInputIndex;

    public void Start()
    {
        bool showTutorial = PlayerPrefs.GetInt("ShowTutorial", 1) == 1;
    
        tutorialPanel.SetActive(showTutorial);
        gamePanel.SetActive(!showTutorial);
    }

    public void StartGame()
    {   
        RandomColor();
        StartCoroutine(FlashColor());
    }

    private void RandomColor()
    {
        int randomColor = Random.Range(0, colorButtons.Length);
        colors.Add(randomColor);
    }

    private IEnumerator FlashColor()
    {
        enableInput = false;
        currentInputIndex = 0;

        foreach (int colorIndex in colors)
        {
            colorButtons[colorIndex].SetActive(true);
            yield return new WaitForSeconds(0.5f);

            colorButtons[colorIndex].SetActive(false);
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

    private void EndGame()
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);  
    }

}
