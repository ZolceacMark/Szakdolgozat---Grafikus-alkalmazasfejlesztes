using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class StatsManager : MonoBehaviour
{
    [Header("Reflex General Stats")]
    [SerializeField] TMP_Text totalPlayedText;
    [SerializeField] TMP_Text totalCompletedText;
    [SerializeField] TMP_Text totalFailedText;

    [Header("Reflex Length 25 Stats")]
    [SerializeField] TMP_Text played25Text;

    [Header("Reflex Length 50 Stats")]
    [SerializeField] TMP_Text played50Text;

    [Header("Reflex Length 75 Stats")]
    [SerializeField] TMP_Text played75Text;

    [Header("Reflex Length 100 Stats")]
    [SerializeField] TMP_Text played100Text;

    [Header("Reflex Reaction Stats")]
    [SerializeField] TMP_Text avgReactionText;
    [SerializeField] TMP_Text avgGreenReactionText;
    [SerializeField] TMP_Text avgYellowReactionText;

    [Header("Reflex White Stats")]
    [SerializeField] TMP_Text whiteCorrectText;
    [SerializeField] TMP_Text whiteWrongText;

    private void Start()
    {
        LoadGeneralStats();
        LoadLengthStats();
        LoadReactionStats();
        LoadWhiteStats();      
    }


    private void LoadGeneralStats()
    {
        totalPlayedText.text = PlayerPrefs.GetInt("ReflexPlayedGames", 0).ToString();
        totalCompletedText.text = PlayerPrefs.GetInt("ReflexCompletedGames", 0).ToString();
        totalFailedText.text = PlayerPrefs.GetInt("ReflexFailedGames", 0).ToString();
    }

     private void LoadLengthStats()
    {
        SetLengthStats(25, played25Text);
        SetLengthStats(50, played50Text);
        SetLengthStats(75, played75Text);
        SetLengthStats(100, played100Text);
    }

    private void SetLengthStats(int length, TMP_Text playedText)
    {
        string lengthKey = "Reflex" + length;

        playedText.text = PlayerPrefs.GetInt(lengthKey + "Played", 0).ToString();
    }

    private void LoadReactionStats()
    {
        List<float> greenReactions = LoadReactionList("ReflexGreenReactions");
        List<float> yellowReactions = LoadReactionList("ReflexYellowReactions");
        List<float> allReactions = new List<float>();

        allReactions.AddRange(greenReactions);
        allReactions.AddRange(yellowReactions);

        avgReactionText.text = FormatReaction(allReactions);
        avgGreenReactionText.text = FormatReaction(greenReactions);
        avgYellowReactionText.text = FormatReaction(yellowReactions);
    }

    private List<float> LoadReactionList(string key)
    {
        string json = PlayerPrefs.GetString(key, "");

        if (string.IsNullOrEmpty(json))
            return new List<float>();

        ReactionData data = JsonUtility.FromJson<ReactionData>(json);

        if (data == null || data.values == null)
            return new List<float>();

        return data.values;
    }

    private float CalculateAverage(List<float> reactions)
    {
        if (reactions.Count == 0)
            return 0f;

        float total = 0f;

        foreach (float reaction in reactions)
        {
            total += reaction;
        }

        return total / reactions.Count;
    }

    private string FormatReaction(List<float> reactions)
    {
        if (reactions.Count == 0)
            return "-";

        return CalculateAverage(reactions).ToString("F0") + " ms";
    }

    private void LoadWhiteStats()
    {
        int correct = PlayerPrefs.GetInt("ReflexCorrectWhite", 0);
        int wrong = PlayerPrefs.GetInt("ReflexWrongWhite", 0);

        whiteCorrectText.text = correct.ToString();
        whiteWrongText.text = wrong.ToString();
    }
}

