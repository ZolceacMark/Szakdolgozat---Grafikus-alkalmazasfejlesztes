using UnityEngine;

public class CountdownSFX : MonoBehaviour
{
    [SerializeField] private AudioClip countdownSFX;
    [SerializeField] private AudioClip startSFX;

    public void PlayCountdownSFX()
    {
        SFXManager.instance.PlaySFXClip(countdownSFX, transform, 1f);
    }

    public void PlayStartSFX()
    {
        SFXManager.instance.PlaySFXClip(startSFX, transform, 1f);
    }
}
