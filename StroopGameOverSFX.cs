using UnityEngine;

public class StroopGameOverSFX : MonoBehaviour
{
    [SerializeField] private AudioClip gameOverSFX;

    public void PlayGameOverSFX()
    {
        SFXManager.instance.PlaySFXClip(gameOverSFX, transform, 1f);
    }
}
