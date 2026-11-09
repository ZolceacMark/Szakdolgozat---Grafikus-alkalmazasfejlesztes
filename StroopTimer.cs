using UnityEngine;
using DentedPixel;
using System.Threading;
using TMPro;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
public class StroopTimer : MonoBehaviour
{
    [SerializeField] private GameObject timerBar;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float time;
    
    [SerializeField] private StroopManager timeEndedManager;

    private float startingTime = 10f;
    private bool timerStarter;
    private bool timeEnded;

    private void Update()
    {
        if (!timerStarter)
        {
            return;
        }

        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else if (time <= 0)
        {
            time = 0;

            timerStarter = false;
            timeEnded = true;

            timeEndedManager.TimerEnded();
        }
        timerText.text = time.ToString("F2");
    }

    public void TimeAnimation()
    {
        timerStarter = true;
        LeanTween.scaleX(timerBar, 0, time);
    }

    public void ResetTimer(float newTime)
    {
        LeanTween.cancel(timerBar);

        time = newTime;
        timeEnded = false;
        timerStarter = true;

        timerText.text = time.ToString("F2");

        timerBar.transform.localScale = Vector3.one;

        LeanTween.scaleX(timerBar, 0, time);
    }

}
