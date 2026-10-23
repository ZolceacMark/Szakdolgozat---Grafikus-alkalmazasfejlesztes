using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class FirstRoundManager : MonoBehaviour
{
    public Button button;
    public Button earlyButton;
    public GameObject gameFinishPanel;
    public GameObject reactionPanel;
    public GameObject gamePanel;
    public TextMeshProUGUI reactionText;
    public TextMeshProUGUI averageReactionText;
    private bool activeButton;
    private Color[] colors;
    private float randomTimer;
    private float timer;
    private float startReactionTime;
    private int roundCount = 0;
    private float averageReactionTime;
    List<float> reactions = new List<float>();
    
    private void Start()
    {   
        button.gameObject.SetActive(false);
        earlyButton.gameObject.SetActive(true);
        randomTimer = Random.Range(2,11);
        Debug.Log("Randomized timer: " + randomTimer);
        colors = new Color[2];

        //ColorUtility.TryParseHtmlString: AI megoldas
        ColorUtility.TryParseHtmlString("#FF0000", out colors[0]); 
        ColorUtility.TryParseHtmlString("#22D800", out colors[1]);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!activeButton && timer >= randomTimer)
        {
            SetButtonActive();
            activeButton = true;
            earlyButton.gameObject.SetActive(false);
        }

    }

    void SetButtonActive()
    {
        button.gameObject.SetActive(true);
        
        Color randomColor = colors[1];
        button.GetComponent<Image>().color = randomColor;
        startReactionTime = Time.time;
    }

    public void ReactionTime()
    {
        float reactionTime = (Time.time - startReactionTime) * 1000f;
        reactionText.text = (int)reactionTime + "ms";
        reactions.Add((int)reactionTime);
        Debug.Log("Reaction time: " + reactionTime + "ms");
        roundCount += 1;
        Debug.Log("Current Round: " + roundCount);
    }

    public void RestartRound()
    {
        randomTimer = Random.Range(2,11);
        Debug.Log("Randomized timer: " + randomTimer);
        timer = 0f;
        startReactionTime = 0f;
        earlyButton.gameObject.SetActive(true);
        button.gameObject.SetActive(false);
        activeButton = false;
        if (roundCount == 5)
        {
            EndScreen();
        }   
    }

    public void EndScreen()
    {
        reactionPanel.gameObject.SetActive(false);
        gameFinishPanel.gameObject.SetActive(true);
        gamePanel.gameObject.SetActive(false);
        averageReactionTime = reactions.Average();
        Debug.Log("Average Reaction Time: " + averageReactionTime);
        averageReactionText.text = (int)averageReactionTime + "ms";
    }
}
