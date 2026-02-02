using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayGameManager : MonoBehaviour
{
    private string selectedCharacter; 
    private string selectedLevel;

    public void SelectEasy()
    {

        selectedLevel = "Easy";


    }

    public void SelectHard()
    {

        selectedLevel = "Hard";


    }
    public void SelectBoy()
    {

        selectedCharacter = "Boy";


    }
    public void SelectGirl()
    {

        selectedCharacter = "Girl";

    }

    public void Play()
    {
        string sceneName = "";

        if (selectedCharacter == "Boy" && selectedLevel == "Easy")
            sceneName = "BoyEasy";
        else if (selectedCharacter == "Boy" && selectedLevel == "Hard")
            sceneName = "BoyHard";
        else if (selectedCharacter == "Girl" && selectedLevel == "Easy")
            sceneName = "GirlEasy";
        else if (selectedCharacter == "Girl" && selectedLevel == "Hard")
            sceneName = "GirlHard";
        else
            sceneName = "GirlEasy"; 

        SceneManager.LoadScene(sceneName);
    }
}

