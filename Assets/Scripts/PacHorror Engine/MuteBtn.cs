using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MuteBtn : MonoBehaviour
{
  public TMP_Text muteBtnText;
  public AudioSource backgroundAudio;
  private bool isAudioSourceAttached = false;

  void Start()
  {
    InvokeRepeating("CheckAudioSourceAttached", 0.0f, 1.0f);
  }

  void CheckAudioSourceAttached()
  {
    if (backgroundAudio != null)
    {
      isAudioSourceAttached = true;
      CancelInvoke("CheckAudioSourceAttached");
    }
  }

  public void MuteAudio()
  {
    if (isAudioSourceAttached)
    {
      if (backgroundAudio.isPlaying)
      {
        backgroundAudio.Pause();
        muteBtnText.SetText("Unmute");
      }
      else
      {
        backgroundAudio.UnPause();
        muteBtnText.SetText("Mute");
      }
    }
  }
}