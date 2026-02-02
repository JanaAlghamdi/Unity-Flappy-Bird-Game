using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;

    public int coinCount;
    public UnityEngine.UI.Text coinText;

    [Header("------- Final Texts (Game Over) -------")]
    public Text finalScoreTextGameOver;
    public Text finalCoinTextGameOver;

    [Header("------- Final Texts (Win Screen) -------")]
    public Text finalScoreTextWin;
    public Text finalCoinTextWin;


    AudioMangerScript audioManger;

    private bool isEndState = false;

    // --- Final Cup on Result Screen ---
    [Header("------- Final Cup (Result Screens) -------")]
    public Image finalCupImageGameOver;   // cup Image on the Game Over panel
    public Image finalCupImageWin;        // cup Image on the Win panel
    public Sprite silverCupSprite;        // silver sprite
    public Sprite goldCupSprite;          // gold sprite


    [Header("------- Win Condition -------")]
    public int pipesToWin = 3;
    public GameObject winImage;

    // ------------- CUPS UI (In-Game Top Corner) ------------- //
    [Header("------- Cups (In-Game) -------")]
    public GameObject silverCupUI;      // drag Silver cup image here (top corner)
    public GameObject goldCupUI;        // drag Gold cup image here (top corner)

    public int silverCupThreshold = 5;  // show silver after 5 pipes
    public int goldCupThreshold = 10;   // show gold after 10 pipes

    [Header("------- Shield Settings -------")]
    public int coinsForShield = 10;     // how many coins needed to activate shield
    public float shieldDuration = 5f;   // how long the shield lasts in seconds

    private BirdScript bird;            // reference to the bird
    private int coinsSinceLastShield = 0;


    private void Awake()
    {
        audioManger = GameObject.FindWithTag("Audio").GetComponent<AudioMangerScript>();
    }

    private void Start()
    {
        // Make sure in-game cups are hidden at the start
        if (silverCupUI != null) silverCupUI.SetActive(false);
        if (goldCupUI != null) goldCupUI.SetActive(false);

        // Hide final cups at the start
        if (finalCupImageGameOver != null)
            finalCupImageGameOver.enabled = false;
        if (finalCupImageWin != null)
            finalCupImageWin.enabled = false;

        // Get the bird reference for shield
        bird = FindFirstObjectByType<BirdScript>();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();

        // UPDATE CUPS WHEN SCORE CHANGES
        UpdateCups();

        // Optional: check win condition here if you want win on certain pipes
        if (playerScore >= pipesToWin)
        {
            Win();
        }
    }

    /// <summary>
    /// Updates the in-game top-corner cup (none / silver / gold)
    /// based on playerScore.
    /// </summary>
    private void UpdateCups()
    {
        // Gold cup if 10 or more pipes
        if (playerScore >= goldCupThreshold)
        {
            if (silverCupUI != null) silverCupUI.SetActive(false);
            if (goldCupUI != null) goldCupUI.SetActive(true);
        }
        // Silver cup if 5–9 pipes
        else if (playerScore >= silverCupThreshold)
        {
            if (silverCupUI != null) silverCupUI.SetActive(true);
            if (goldCupUI != null) goldCupUI.SetActive(false);
        }
        // Less than 5 pipes → hide both
        else
        {
            if (silverCupUI != null) silverCupUI.SetActive(false);
            if (goldCupUI != null) silverCupUI.SetActive(false);
            if (goldCupUI != null) goldCupUI.SetActive(false);
        }
    }

    /// <summary>
    /// Sets the final cup on the result screen (game over / win).
    /// </summary>
    private void UpdateFinalCup(Image targetImage)
    {
        if (targetImage == null)
            return;

        // No cup by default
        targetImage.enabled = false;

        // Gold cup if 10 or more pipes
        if (playerScore >= goldCupThreshold && goldCupSprite != null)
        {
            targetImage.sprite = goldCupSprite;
            targetImage.enabled = true;
        }
        // Silver cup if 5–9 pipes
        else if (playerScore >= silverCupThreshold && silverCupSprite != null)
        {
            targetImage.sprite = silverCupSprite;
            targetImage.enabled = true;
        }
        // Less than threshold → leave disabled
    }

    private void UpdateFinalResultTexts(Text scoreText, Text coinText)
    {
        if (scoreText != null)
            scoreText.text = playerScore.ToString();

        if (coinText != null)
            coinText.text = coinCount.ToString();
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (isEndState) return;

        // STOP SOUNDS
        audioManger.StopMusic();
        audioManger.PlaySFX(audioManger.gameOver);

        // UPDATE FINAL UI TEXTS (GAME OVER)
        UpdateFinalResultTexts(finalScoreTextGameOver, finalCoinTextGameOver);

        // UPDATE FINAL CUP ON GAME OVER SCREEN
        UpdateFinalCup(finalCupImageGameOver);



        // SHOW GAME OVER
        gameOverScreen.SetActive(true);

        FreezeGame();
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Win()
    {
        if (isEndState) return;

        audioManger.StopMusic();
        audioManger.PlaySFX(audioManger.winning);

        // UPDATE FINAL UI TEXTS (WIN)
        UpdateFinalResultTexts(finalScoreTextWin, finalCoinTextWin);

        // FINAL CUP ON WIN SCREEN
        UpdateFinalCup(finalCupImageWin);



        winImage.SetActive(true);
        FreezeGame();
    }

    private void FreezeGame()
    {
        if (isEndState) return;
        isEndState = true;

        Time.timeScale = 0f;

        var bird = FindFirstObjectByType<BirdScript>();
        if (bird != null && bird.myRigidbody != null)
        {
            bird.myRigidbody.linearVelocity = Vector2.zero;
            bird.myRigidbody.simulated = false;
        }
    }

    public void AddCoin(int amount)
    {
        // 1) Total coins (for UI / score)
        coinCount += amount;

        // 2) Progress only for shield
        coinsSinceLastShield += amount;

        // Update the UI with TOTAL coins (never reset here)
        if (coinText != null)
            coinText.text = coinCount.ToString();

        // Check for shield activation
        if (coinsSinceLastShield >= coinsForShield)
        {
            if (bird == null)
                bird = FindFirstObjectByType<BirdScript>();

            if (bird != null)
            {
                bird.ActivateShield(shieldDuration);

                // Use up only the shield-progress coins,
                // but KEEP the total coins shown on screen.
                coinsSinceLastShield -= coinsForShield;

                
            }
        }
    }

}

