using TMPro;
using UnityEngine;

public class StroopButtonRandomizer : MonoBehaviour
{
    [SerializeField] private TMP_Text[] buttonsText;
    [SerializeField] private TMP_Text tvColor;
    private Color pinkColor = new Color32(255, 0, 131, 255); // #FF0083
    
    public enum StroopColor
    {
        Purple, 
        Pink,
        Red,
        Green,
        Blue,
        Yellow,
        Orange,
        White,
        Black
    }

    private StroopColor[] names = new StroopColor[9];
    private StroopColor[] colors = new StroopColor[9];

    //AI Idea
    private StroopColor targetColor;
    private StroopColor TargetColor => targetColor;

    private void Start()
    {
        RandomizeButtons();
    }

    public void RandomizeTvColor()
    {
        StroopColor name = (StroopColor) Random.Range(0, 9);
        StroopColor color = (StroopColor) Random.Range(1, 9);

        targetColor = color;

        tvColor.text = name.ToString();
        switch (color)
        {
            case StroopColor.Purple:
                tvColor.color = Color.purple;
                break;
            case StroopColor.Pink:
                tvColor.color = pinkColor;
                break;
            case StroopColor.Red:
                tvColor.color = Color.red;
                break;
            case StroopColor.Green:
                tvColor.color = Color.green;
                break;
            case StroopColor.Blue:
                tvColor.color = Color.blue;
                break;
            case StroopColor.Yellow:
                tvColor.color = Color.yellow;
                break;
            case StroopColor.Orange:
                tvColor.color = Color.orange;
                break;
            case StroopColor.White:
                tvColor.color = Color.white;
                break;
            case StroopColor.Black:
                tvColor.color = Color.black;
                break;
        }
    }

    public void RandomizeButtons()
    {   
        //Opus idea
        int count = Mathf.Min (buttonsText.Length, names.Length);

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = (StroopColor) i;
        }

        for (int i = names.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            StroopColor temp = names[i];
            names[i] = names[randomIndex];
            names[randomIndex] = temp;
        }

        for (int i = 0; i < names.Length; i++)
        {
            colors[i] = names[i];
        }

        for (int i = names.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i);

            StroopColor temp = colors[i];
            colors[i] = colors[randomIndex];
            colors[randomIndex] = temp;
        }

        for (int i = 0; i < count; i++)
        {
            buttonsText[i].text = names[i].ToString();
            TextColor(buttonsText[i], colors[i]);
        }
    }

    private void TextColor(TMP_Text buttonText, StroopColor color)
    {
        switch (color)
            {
                case StroopColor.Purple:
                    buttonText.color = Color.purple;
                    break;
                case StroopColor.Pink:
                    buttonText.color = pinkColor;
                    break;
                case StroopColor.Red:
                    buttonText.color = Color.red;
                    break;
                case StroopColor.Green:
                    buttonText.color = Color.green;
                    break;
                case StroopColor.Blue:
                    buttonText.color = Color.blue;
                    break;
                case StroopColor.Yellow:
                    buttonText.color = Color.yellow;
                    break;
                case StroopColor.Orange:
                    buttonText.color = Color.orange;
                    break;
                case StroopColor.White:
                    buttonText.color = Color.white;
                    break;
                case StroopColor.Black:
                    buttonText.color = Color.black;
                    break;
            } 
    }

    public bool IsCorrectButton(int i)
    {
        return names[i] == targetColor;
    }
}
