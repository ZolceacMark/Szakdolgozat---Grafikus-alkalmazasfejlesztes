using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSFX;


    public void PlaySound()
    {
        SFXManager.instance.PlaySFXClip(buttonSFX, transform, 1f);
    }
}