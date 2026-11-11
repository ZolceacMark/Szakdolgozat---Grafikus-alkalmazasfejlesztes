using TMPro;
using UnityEngine;

public class StatsManagerStroop : MonoBehaviour
{
    [Header("Stroop Statistics")]
    [SerializeField] private TMP_Text highestScoreText;
    [SerializeField] private TMP_Text totalPlayedText;
    [SerializeField] private TMP_Text totalScoreText;
    [SerializeField] private TMP_Text averageScoreText;

    private void Start()
    {
        LoadStatistics();
    }

    private void LoadStatistics()
    {
        int highestScore = PlayerPrefs.GetInt("StroopHighestScore", 0);
        int totalPlayed = PlayerPrefs.GetInt("StroopTotalPlayed", 0);
        int totalScore = PlayerPrefs.GetInt("StroopTotalScore", 0);
        float averageScore = 0;

        if (totalPlayed > 0)
            averageScore = (float) totalScore / totalPlayed;

        highestScoreText.text = highestScore.ToString();
        totalPlayedText.text = totalPlayed.ToString();
        totalScoreText.text = totalScore.ToString();
        averageScoreText.text = averageScore.ToString("F2");
    }
}
