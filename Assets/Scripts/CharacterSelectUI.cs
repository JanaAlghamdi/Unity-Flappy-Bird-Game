using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectUI : MonoBehaviour
{
    // Save keys
    private const string BirdKey = "SelectedBird";   // Boy / Girl
    private const string LevelKey = "SelectedLevel";  // Easy / Hard
    private const string RegionKey = "SelectedRegion"; // Central / South / West

    // ========== CHARACTER SELECTION ==========
    public void ChooseBoy()
    {
        PlayerPrefs.SetString(BirdKey, "Boy");
        PlayerPrefs.Save();
    }

    public void ChooseGirl()
    {
        PlayerPrefs.SetString(BirdKey, "Girl");
        PlayerPrefs.Save();
    }

    // ========== REGION SELECTION ==========
    public void ChooseCentral()
    {
        PlayerPrefs.SetString(RegionKey, "Central");
        PlayerPrefs.Save();
    }

    public void ChooseSouth()
    {
        PlayerPrefs.SetString(RegionKey, "South");
        PlayerPrefs.Save();
    }

    public void ChooseWest()
    {
        PlayerPrefs.SetString(RegionKey, "West");
        PlayerPrefs.Save();
    }

    // ========== LEVEL SELECTION (your old code) ==========
    public void ChooseEasy()
    {
        PlayerPrefs.SetString(LevelKey, "Easy");
        PlayerPrefs.Save();
    }

    public void ChooseHard()
    {
        PlayerPrefs.SetString(LevelKey, "Hard");
        PlayerPrefs.Save();
    }

    // ========== PLAY BUTTON ==========
    public void PlayLevel()
    {
        // 🟢 STOP MAIN MENU MUSIC HERE using the singleton
        if (MusicManagerMainMenuScript.Instance != null)
        {
            MusicManagerMainMenuScript.Instance.StopMenuMusic();
        }

        // then load the selected level
        string selectedLevel = PlayerPrefs.GetString(LevelKey, "Easy"); // default Easy

        if (selectedLevel == "Hard")
            SceneManager.LoadScene("HardLevel");
        else
            SceneManager.LoadScene("EasyLevel");
    }
}

