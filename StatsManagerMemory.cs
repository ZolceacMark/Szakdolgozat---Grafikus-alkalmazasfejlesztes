using TMPro;
using UnityEngine;

public class StatsManagerMemory : MonoBehaviour
{
    [Header("Memory overall stats")]
    [SerializeField] private TMP_Text overallHighestScore;
    [SerializeField] private TMP_Text overallTotalPlayed;
    [SerializeField] private TMP_Text overallAvgScore;

    [Header("Memory Easy Stats")]
    [SerializeField] private TMP_Text easyHighScore;
    [SerializeField] private TMP_Text easyPlayed;
    [SerializeField] private TMP_Text easyCompletedRounds;
    [SerializeField] private TMP_Text easyMistakes;

    [Header("Memory Hard Stats")]
    [SerializeField] private TMP_Text hardHighScore;
    [SerializeField] private TMP_Text hardPlayed;
    [SerializeField] private TMP_Text hardCompletedRounds;
    [SerializeField] private TMP_Text hardMistakes;

    [Header("Average Scores")]
    [SerializeField] private TMP_Text easyAvgScore;
    [SerializeField] private TMP_Text hardAvgScore;


    private void Start()
    {
        LoadEasyMemory();
        LoadHardMemory();
        LoadModeAverageScores();
        LoadOverallStats();
    }

    private void LoadEasyMemory()
    {
        LoadMemoryStats("MemoryEasy", easyHighScore, easyPlayed, easyCompletedRounds, easyMistakes); 
    }

    private void LoadHardMemory()
    {
        LoadMemoryStats("MemoryHard", hardHighScore, hardPlayed, hardCompletedRounds, hardMistakes);   
    }

    private void LoadModeAverageScores()
    {
        easyAvgScore.text = CalculateAverageModeScore("MemoryEasy");
        hardAvgScore.text = CalculateAverageModeScore("MemoryHard");   
    }

    private void LoadOverallStats()
    {   
        overallHighestScore.text = PlayerPrefs.GetInt("MemoryOverallHighestScore", 0).ToString();

        int easyPlayed = PlayerPrefs.GetInt("MemoryEasyTotalPlayed", 0);
        int hardPlayed = PlayerPrefs.GetInt("MemoryHardTotalPlayed", 0);
        int overallPlayed = easyPlayed + hardPlayed;

        overallTotalPlayed.text = (easyPlayed + hardPlayed).ToString();

        int totalScore = PlayerPrefs.GetInt("MemoryEasyTotalScore", 0) + PlayerPrefs.GetInt("MemoryHardTotalScore", 0);

        if (overallPlayed == 0)
        {
            overallAvgScore.text = "0";
        }
        else
        {
            float average = (float)totalScore / overallPlayed;
            overallAvgScore.text = average.ToString("F2");
        }
    }

    private void LoadMemoryStats(
        string difficulty, 
        TMP_Text highestScore, 
        TMP_Text totalPlayed, 
        TMP_Text completedRound, 
        TMP_Text totalMistakes)
    {
        highestScore.text = PlayerPrefs.GetInt(difficulty + "HighestScore", 0).ToString();
        totalPlayed.text = PlayerPrefs.GetInt(difficulty + "TotalPlayed", 0).ToString();
        completedRound.text = PlayerPrefs.GetInt(difficulty + "CompletedRounds", 0).ToString();
        totalMistakes.text = PlayerPrefs.GetInt(difficulty + "TotalMistakes", 0).ToString();
    }

    private string CalculateAverageModeScore(string mode)
    {
        int totalScore = PlayerPrefs.GetInt(mode + "TotalScore", 0);
        int totalPlayed = PlayerPrefs.GetInt(mode + "TotalPlayed", 0);

        if (totalPlayed == 0)
            return "0";
        
        float averageScore = (float)totalScore / totalPlayed;

        return averageScore.ToString("F2");
    }

    
}
