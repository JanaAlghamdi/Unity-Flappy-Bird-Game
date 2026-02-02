using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject levelPanel;
    public GameObject characterPanel;

    void Awake() => ShowMain();

    void ShowOnly(GameObject p)
    {
        if (mainPanel) mainPanel.SetActive(false);
        if (levelPanel) levelPanel.SetActive(false);
        if (characterPanel) characterPanel.SetActive(false);
        if (p) p.SetActive(true);
    }

    public void ShowMain() => ShowOnly(mainPanel);
    public void ShowLevels() => ShowOnly(levelPanel);
    public void ShowCharacters() => ShowOnly(characterPanel);
}
