using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private LogicScript logic;
    private AudioSource audioSource;

    private void Start()
    {
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        audioSource = GetComponent<AudioSource>(); // get the AudioSource attached to the coin prefab
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // your bird is using layer 3 in PipeMiddleScript
        if (collision.gameObject.layer == 3)
        {
            // play sound if available
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }

            // add coin to the logic counter
            logic.AddCoin(1);

            // disable the visual so coin disappears immediately
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // destroy the coin after sound finishes playing (short delay)
            Destroy(gameObject, 0.50f);
        }
    }
}
