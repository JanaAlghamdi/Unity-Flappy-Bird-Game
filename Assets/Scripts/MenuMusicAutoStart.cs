using UnityEngine;

public class MenuMusicAutoStart : MonoBehaviour
{
    void Start()
    {
        if (MusicManagerMainMenuScript.Instance != null)
            MusicManagerMainMenuScript.Instance.StartMenuMusicIfNeeded(true);
    }
}

