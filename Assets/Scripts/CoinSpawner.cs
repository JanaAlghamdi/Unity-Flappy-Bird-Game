using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnRate = 2.5f;
    public float verticalPadding = 1f;
    public LayerMask pipeMask;
    public float checkRadius = 1.0f;
    public float horizontalCheckOffset = 0.8f;

    public float extraX = 1.5f;   // how far outside the right edge

    private float timer = 0f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        timer = Random.Range(0f, spawnRate);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            TrySpawnCoin();
            timer = 0f;
        }
    }

    void TrySpawnCoin()
    {
        // right edge of the camera in world units
        float rightX = cam.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x + extraX;

        // top and bottom of screen
        Vector3 bottom = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 top = cam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));

        const int maxTries = 5;
        for (int i = 0; i < maxTries; i++)
        {
            float y = Random.Range(bottom.y + verticalPadding, top.y - verticalPadding);

            Vector3 centerPos = new Vector3(rightX, y, 0f);
            Vector3 leftPos = new Vector3(rightX - horizontalCheckOffset, y, 0f);
            Vector3 rightPos = new Vector3(rightX + horizontalCheckOffset, y, 0f);

            bool centerBlocked = Physics2D.OverlapCircle(centerPos, checkRadius, pipeMask);
            bool leftBlocked = Physics2D.OverlapCircle(leftPos, checkRadius, pipeMask);
            bool rightBlocked = Physics2D.OverlapCircle(rightPos, checkRadius, pipeMask);

            if (centerBlocked || leftBlocked || rightBlocked)
                continue;

            Instantiate(coinPrefab, centerPos, Quaternion.identity);
            return;
        }
        // all tries blocked → skip this spawn
    }
}
