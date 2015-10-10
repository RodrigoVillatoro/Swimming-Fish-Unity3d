using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	private AudioSource audioSource;
	private float audioLength;

//	// Use this for initialization
//	void Start() {
//		audioSource = GetComponent<AudioSource>();
//		audioLength = audioSource.clip.length;
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//		audioLength -= Time.deltaTime;
//		if (audioLength <= 0.0f) {
//
//			if (Application.levelCount == Application.loadedLevel) {
//				Application.LoadLevel("0_MainMenu");
//			} else {
//				Application.LoadLevel(Application.loadedLevel + 1);
//			}
//
//		}
//
//	}
}
