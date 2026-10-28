using Unity.Collections;
using UnityEngine;

public class ReflexRandomImage : MonoBehaviour
{

    [SerializeField] private RectTransform greenColor;
    [SerializeField] private RectTransform yellowColor;
    [SerializeField] private RectTransform whiteColor;
    

    public void ShowRandomImage()
    {
        greenColor.gameObject.SetActive(false);
        yellowColor.gameObject.SetActive(false);
        whiteColor.gameObject.SetActive(false);


        int randomImage = Random.Range(0, 3);
        RectTransform selectedImage;

        if (randomImage == 0)
            selectedImage = greenColor;
        else if (randomImage == 1)
            selectedImage = yellowColor;
        else 
            selectedImage = whiteColor;

        float x, y, scale;
        RandomRange(out x, out y);
        RandomScale(out scale);

        selectedImage.anchoredPosition = new Vector2(x, y);
        selectedImage.localScale = new Vector3(scale, scale, scale);

        selectedImage.gameObject.SetActive(true);

    }

    private void RandomRange(out float x, out float y)
    {
        x = Random.Range(-645f, 645f);
        y = Random.Range(-240f, 120f);
    }

    private void RandomScale(out float x)
    {
        x = Random.Range(0.6f, 6f);
    }


}
