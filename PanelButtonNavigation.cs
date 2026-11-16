using UnityEngine;
using UnityEngine.InputSystem;

public class PanelButtonNavigation : MonoBehaviour
{
   [SerializeField] private GameObject previousPanel;
   [SerializeField] private ButtonSFX buttonSFX;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            buttonSFX.PlaySound();
            previousPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
