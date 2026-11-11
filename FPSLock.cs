using UnityEngine;

public class FPSLock : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 240;
        QualitySettings.vSyncCount = 0;
    }
}
