using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour {
  public GameObject pauseMenu;
  public static bool isPaused;
 
  private WinningScene winnerScript; // outside script variable

  void Start() {
    pauseMenu.SetActive(false);
    winnerScript = GameObject.Find("ScoreContainerCanvas").GetComponent<WinningScene>();
  }

  void Update() {
    if(Input.GetKeyDown(KeyCode.Escape)) {
      if(isPaused) {
        ResumeGame();
      } else {
        PauseGame();
      }
    }
  }

  // Function called when "Start Game" button is pressed
  public void StartGame() {
    // Load the next scene in the build index
    SceneManager.LoadScene("Level 1");
    Time.timeScale = 1f;
  }

  public void PauseGame() {
    pauseMenu.SetActive(true);
    isPaused = true;
    Time.timeScale = 0f;
  }
  public void ResumeGame() {
    pauseMenu.SetActive(false);
    isPaused = false;
    Time.timeScale = 1f;
  }

  // Function that displays the win screen and informs the player
  // Final Score screen
  public void PlayerWon() {
    SceneManager.LoadScene("WinningScene");
    winnerScript.playerScoreValue();
    Time.timeScale = 0f;
  }

  public void ContinueToNextLevel(int num = 2) {
    SceneManager.LoadScene("Level " + num);
    Time.timeScale = 1f;
  }
  /* Future helper function to jump between levels
  NOTES:
    -Check if scene exists in the build list before attempting to 
      load it using SceneManager.GetSceneByName("String") method. 
    - The SceneManager.GetSceneByName method returns a Scene object. 
        If the scene with the specified name is not present in the build list, 
        the returned Scene object will have its isLoaded property set to false.
      
  public void ContinueToNextLevel(int num) {
    SceneManager.LoadScene("Level " + num);
    Time.timeScale = 1f;
  }
  */

  // Function called when "End Game" button is pressed
  public void PlayerLose() {
    // Load the next scene in the build index
    SceneManager.LoadScene("GameOver");
  }

  public void LoadMainMenu() {
    // Load the next scene in the build index
    SceneManager.LoadScene(0);
  }

  public void CloseApplication() {
    Application.Quit();
  }
}

// StartGame method is called to load a scene
// public void StartGame(string sceneName)
// {
//     try
//     {
//         // Load the specified scene
//         SceneManager.LoadScene(sceneName);
//     }
//     catch (UnityException e)
//     {
//         // Log an error message if the scene fails to load
//         Debug.LogError("Error loading scene: " + e.Message);
//     }
// }