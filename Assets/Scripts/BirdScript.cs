using Unity.VisualScripting;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;

    AudioMangerScript audioManger;


    [Header("Shield")]
    public bool shieldActive = false;
    public float shieldTimeRemaining = 0f;

    // Optional: drag a glow/circle sprite here in Inspector to show shield
    public GameObject shieldVisual;



    private void Awake()
    {
        audioManger = GameObject.FindWithTag("Audio").GetComponent <AudioMangerScript>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == true) {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;

        }
        if (transform.position.y > 17 || transform.position.y < -17)
        {
            logic.gameOver();
            birdIsAlive=false;

        }

        // --- SHIELD TIMER ---
        if (shieldActive)
        {
            shieldTimeRemaining -= Time.deltaTime;
            if (shieldTimeRemaining <= 0f)
            {
                shieldActive = false;
                shieldTimeRemaining = 0f;

                if (shieldVisual != null)
                    shieldVisual.SetActive(false);
            }
        }


    }
    public void ActivateShield(float duration)
    {
        shieldActive = true;
        shieldTimeRemaining = duration;

        if (shieldVisual != null)
            shieldVisual.SetActive(true);
        UISfxManagerScript.Instance?.PlayByKey("sparkle");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!birdIsAlive) return;

        // If shield is active, ignore the crash completely (NO SOUND, NO DEATH)
        if (shieldActive)
        {
            return;   // ← just ignore
        }

        // Normal behavior when no shield
        birdIsAlive = false;

        if (myRigidbody != null)
        {
            myRigidbody.linearVelocity = Vector2.zero;
        }

        // Play collision only when shield is NOT active
        audioManger.PlaySFX(audioManger.collision);
        logic.gameOver();
    }


}
