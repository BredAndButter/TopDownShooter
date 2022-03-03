using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	public AudioSource audioSource;
	private int muteNum;

	void Awake() {
		muteNum = PlayerPrefs.GetInt("isMuted", 1);
	}

	void Update() {
		if(muteNum == 1) {
			audioSource.mute = false;
		} else {
			audioSource.mute = true;
		}
	}

	public void MuteButton() {
		muteNum = -muteNum;
		PlayerPrefs.SetInt("isMuted", muteNum);
	}
}