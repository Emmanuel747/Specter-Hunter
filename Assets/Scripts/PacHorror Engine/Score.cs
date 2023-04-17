using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
  private TextMeshProUGUI progressText, Game_Timer;
  public TextMeshProUGUI scoreShadow, scoreText;
  private int scoreValue;

  private int intialPellets;
  private int remainingPellets;

  private PacMovementV2 pacmanScript; // targets ../AvatarScripts/PacMovementV2
  private float completionPercentage;
  private float gameTimer = 420f; // (420) 7 minutes in seconds
  private float mapCompletionTarget = 100f; // How many pellets one must eat to pass 1 = 1% max is 100

  private StartMenu menuScript;

  void Start() {
    pacmanScript = GameObject.FindWithTag("Pacman").GetComponent<PacMovementV2>();
    intialPellets = GameObject.FindGameObjectsWithTag("Pellet").Length;
    menuScript = GameObject.Find("PauseMenuTrigger").GetComponent<StartMenu>();

    /* [BUG] werid bug with scoreText and scoreShadow where they only render the score
    when toggled on and off during play.
    scoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
    Debug.Log("SCore Object ->" + scoreText);
    scoreShadow = GameObject.Find("Score_Shadow").GetComponent<TextMeshProUGUI>(); 
    */

    // Creating seperate instances of TextMeshPro
    progressText = GameObject.Find("ProgressBar").GetComponent<TextMeshProUGUI>();
    Game_Timer = GameObject.Find("Game_Timer").GetComponent<TextMeshProUGUI>();
    UpdateTimerText();
  }
  
  void Update () {
    remainingPellets = intialPellets - pacmanScript.consumedPellets;
    completionPercentage = ((float) pacmanScript.consumedPellets / intialPellets * 100); 

    TimerCountdown();
    playerCompletionValue();
    updatePlayerStats();
    // playerScoreValue();
    if (completionPercentage >= mapCompletionTarget) {
      menuScript.PlayerWon();
    }
  }

  // helper function to save player data between screens
  public void updatePlayerStats() {
    PlayerPrefs.SetInt("FinalScore", scoreValue);
    PlayerPrefs.SetFloat("FinalCompletion", completionPercentage);
    PlayerPrefs.SetFloat("FinalTime", gameTimer);
  }


  public void playerCompletionValue() {
    progressText.SetText((completionPercentage).ToString("0.00") + "% Completed");
  }

  // scoreScript.PlayerScoreINC(num) will increase player score by passthrough int value
  public void playerScoreINC(int num) {
    scoreValue += num;
    scoreShadow.SetText("Score \n" + (scoreValue.ToString("0")));
    scoreText.SetText("Score \n" + (scoreValue.ToString("0")));
  }

  // scoreScript.PlayerScoreINC(num) will increase player score by passthrough int value
  public void playerScoreDEC(int num) {
    scoreValue -= num;
  }

  // Potential Lose condition
  private void TimerCountdown() {
    if (gameTimer > 0) {
        gameTimer -= Time.deltaTime;
        UpdateTimerText();
    } else {
        // Time.timeScale = 0f;
        menuScript.PlayerLose();
    }
  }

  public void UpdateTimerText() {
    Game_Timer.SetText(Mathf.FloorToInt(gameTimer / 60).ToString("00") + ":" + (gameTimer % 60).ToString("00"));
  }

  public float GetCompletionPercentage() {
      return completionPercentage;
  }

}
