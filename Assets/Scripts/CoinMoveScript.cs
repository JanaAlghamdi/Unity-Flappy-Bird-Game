using UnityEngine;

public class CoinMoveScript : MonoBehaviour
{
    public float moveSpeed = 5f;   // same as your PipeMoveScript
    public float deadZone = -45f;  // when it goes off screen to the left

    void Update()
    {
        // move left every frame
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // delete the coin when it goes far left
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
