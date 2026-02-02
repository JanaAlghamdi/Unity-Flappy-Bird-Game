using UnityEngine;

public class BirdSkinSwitcher : MonoBehaviour
{
    [Header("Sprite Renderer")]
    public SpriteRenderer sr;

    [Header("Central Region")]
    public Sprite centralBoy;
    public Sprite centralGirl;

    [Header("South Region")]
    public Sprite southBoy;
    public Sprite southGirl;

    [Header("West Region")]
    public Sprite westBoy;
    public Sprite westGirl;

    // same keys used in CharacterSelectUI
    private const string BirdKey = "SelectedBird";    // "Boy" or "Girl"
    private const string RegionKey = "SelectedRegion";  // "Central" / "South" / "West"

    void Awake()
    {
        ApplyFromSaved();
    }

    public void ApplyFromSaved()
    {
        string gender = PlayerPrefs.GetString(BirdKey, "Boy");
        string region = PlayerPrefs.GetString(RegionKey, "Central");

        Sprite chosen = centralBoy; // default

        if (region == "Central")
        {
            chosen = (gender == "Girl") ? centralGirl : centralBoy;
        }
        else if (region == "South")
        {
            chosen = (gender == "Girl") ? southGirl : southBoy;
        }
        else if (region == "West")
        {
            chosen = (gender == "Girl") ? westGirl : westBoy;
        }

        sr.sprite = chosen;
    }
}
