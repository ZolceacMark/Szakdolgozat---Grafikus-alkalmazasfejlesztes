using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountdownStroop : MonoBehaviour
{

    [SerializeField] private UnityEvent gameStarter;
    [SerializeField] private CountdownSFX countdownSFX;

    [SerializeField] private int countdownTime = 3;
    [SerializeField] private TMP_Text countdownText;

    private Color color;

    void Start()
    {
        if (PlayerPrefs.GetInt("ShowTutorial", 0) == 0)
        {
            StartCountdown();
        }    
    }

    public void StartCountdown()
    {
        StopAllCoroutines();

        countdownTime = 3;
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();

            switch (countdownTime)
            {
                case 3:
                    ColorUtility.TryParseHtmlString("#FF0000", out color);
                    break;

                case 2:
                    ColorUtility.TryParseHtmlString("#FF8C1B", out color);
                    break;

                case 1:
                    ColorUtility.TryParseHtmlString("#FDFF17", out color);
                    break;
            }
            
            countdownText.color = color;
            countdownSFX.PlayCountdownSFX();

            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.text = "Start!";
        ColorUtility.TryParseHtmlString("#22D800", out color);
        countdownText.color = color;
        countdownSFX.PlayStartSFX();

        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);

        gameStarter.Invoke();
    }
}
