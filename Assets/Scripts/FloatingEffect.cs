// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FloatingEffect : MonoBehaviour {

//   public float speed = 1f;
//   public float magnitude = 0.5f;
//   public int scoreValue = 100;
//   private ScoreManager scoreManager;

//   private Vector3 startPos;

//   void Start() {
//     startPos = transform.position;
//     scoreManager = FindObjectOfType<ScoreManager>();
//   }
//   private void OnTriggerEnter(Collider other) {
//   if (other.gameObject.CompareTag("Pacman")) {
//     scoreManager.AddScore(scoreValue);
//     Destroy(gameObject);
//   }
// }

//   void Update() {
//     transform.position = startPos + new Vector3(0f, Mathf.Sin(Time.time * speed) * magnitude, 0f);
//     transform.Rotate(0f, 0f, Time.deltaTime * 20f);
//   }
// }
