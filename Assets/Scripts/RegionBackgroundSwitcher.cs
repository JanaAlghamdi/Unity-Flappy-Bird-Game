using UnityEngine;

public class RegionBackgroundSwitcher : MonoBehaviour
{
    private const string RegionKey = "SelectedRegion";

    [Header("Background Quad")]
    public MeshRenderer backgroundRenderer;

    [Header("Region Background Materials")]
    public Material centralMaterial;
    public Material southMaterial;
    public Material westMaterial;

    [Header("Pipe Spawner & Prefabs")]
    public PipeSpawner pipeSpawner;
    public GameObject centralPipePrefab;
    public GameObject southPipePrefab;
    public GameObject westPipePrefab;

    void Awake()   // <<---- change Start() to Awake()
    {
        string region = PlayerPrefs.GetString(RegionKey, "Central");

        // === Background ===
        if (region == "Central")
            backgroundRenderer.material = centralMaterial;
        else if (region == "South")
            backgroundRenderer.material = southMaterial;
        else if (region == "West")
            backgroundRenderer.material = westMaterial;

        // === Pipes ===
        if (region == "Central")
            pipeSpawner.pipe = centralPipePrefab;
        else if (region == "South")
            pipeSpawner.pipe = southPipePrefab;
        else if (region == "West")
            pipeSpawner.pipe = westPipePrefab;
    }
}


