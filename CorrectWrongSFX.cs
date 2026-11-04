using UnityEngine;

public class CorrectWrongSFX : MonoBehaviour
{
    [SerializeField] private AudioClip correctSFX;
    [SerializeField] private AudioClip wrongSFX;
    [SerializeField] private AudioClip gameOverSFX;

    public void PlayCorrectSFX()
    {
        SFXManager.instance.PlaySFXClip(correctSFX, transform, 1f);
    }

    public void PlayWrongSFX()
    {
        SFXManager.instance.PlaySFXClip(wrongSFX, transform, 1f);
    }

    public void PlayGameOverSFX()
    {
        SFXManager.instance.PlaySFXClip(gameOverSFX, transform, 1f);
    }
}
