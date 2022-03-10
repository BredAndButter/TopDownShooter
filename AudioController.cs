using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour {

	public AudioSource audioSource;
	public TextMeshProUGUI MuteText;
	private int muteNum;
	private float vol;

	void Awake() {
		if(SceneManager.GetActiveScene().buildIndex == 0) {
			vol = 0.5f;
		} else {
			vol = 0.8f;
		}

		muteNum = PlayerPrefs.GetInt("isMuted", 1);
	}

	void Update() {
		if(muteNum == 1) {
			audioSource.mute = false;
			MuteText.text = "MUTE";
		} else {
			audioSource.mute = true;
			MuteText.text = "UNMUTE";
		}

		if(PauseMenu.isPaused) {
			audioSource.volume = 0.4f;
		} else {
			audioSource.volume = vol;
		}
	}

	public void MuteButton() {
		muteNum = -muteNum;
		PlayerPrefs.SetInt("isMuted", muteNum);
	}
}