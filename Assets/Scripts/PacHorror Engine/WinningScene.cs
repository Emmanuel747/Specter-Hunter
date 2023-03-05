using UnityEngine;
using TMPro;

public class WinningScene : MonoBehaviour {
  public TMP_Text scoreText;
  public TMP_Text scoreShadowText;
  public TMP_Text progressText;
  public TMP_Text gameTimeText;
  public TMP_Text nextLvlBtnText;

  private int scoreValue;
  private float completionPercentage;
  private float gameTimer;


  private void Start() {
    scoreShadowText = GameObject.Find("Score_Shadow").GetComponent<TextMeshProUGUI>();
    scoreText = GameObject.Find("Score_Display").GetComponent<TextMeshProUGUI>();
    gameTimeText = GameObject.Find("GameTimer").GetComponent<TextMeshProUGUI>();
    progressText = GameObject.Find("ProgressBar").GetComponent<TextMeshProUGUI>();
    nextLvlBtnText = GameObject.Find("NextLvL").GetComponent<TextMeshProUGUI>();
  }

  private void Update() {
    playerScoreValue();
  }

  public void playerScoreValue() {
    scoreValue = PlayerPrefs.GetInt("FinalScore");
    completionPercentage = PlayerPrefs.GetFloat("FinalCompletion");
    gameTimer = PlayerPrefs.GetFloat("FinalTime");

    scoreText.SetText("Score \n" + (scoreValue.ToString("0")));
    scoreShadowText.SetText("Score \n" + (scoreValue.ToString("0")));
    progressText.SetText((completionPercentage).ToString("0.00") + "% Completed");
    gameTimeText.SetText(Mathf.FloorToInt(gameTimer / 60).ToString("00") + ":" + (gameTimer % 60).ToString("00"));
  }
}
