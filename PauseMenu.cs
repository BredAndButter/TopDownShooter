using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public static bool isPaused = false;

	public GameObject pauseMenuUI;

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Space)) {
			if(isPaused) {
				Resume();
			} else {
				Pause();
			}
		}
	}

	public void Resume() {
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}

	void Pause() {
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}
}