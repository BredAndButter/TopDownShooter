using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public Animator anim;
	private int totalDeaths;

	private int mostEnemiesKilled;

	void Start() {
		totalDeaths = PlayerPrefs.GetInt("totalDeaths", 0);
		Time.timeScale = 1;
	}

	public void ReloadScene() {
		StartCoroutine(SceneReload());
	}

	public void LoadNextScene() {
		StartCoroutine(NextScene());
	}

	public void LoadMain() {
		StartCoroutine(MainMenu());
	}

	public void Close() {
		Application.Quit();
	}

	public void FeedbackForm() {
		Application.OpenURL("https://forms.gle/1uKCTnEonKY7TjyR9");
	}

	public void Reset() {
		PlayerPrefs.SetInt("totalKills", 0);
		PlayerPrefs.SetInt("totalDeaths", 0);
		PlayerPrefs.SetInt("highScore", 0);
		PlayerPrefs.SetInt("totalScore", 0);
		PlayerPrefs.SetInt("mostEnemiesKilled", 0);
		PlayerPrefs.SetFloat("highTime", 0);
		PlayerPrefs.SetFloat("totalTime", 0);
		PlayerPrefs.SetInt("isMuted", 1);
	}

	IEnumerator SceneReload() {
		Time.timeScale = 0;
		anim.SetTrigger("FadeOut");
		yield return new WaitForSecondsRealtime(0.75f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator NextScene() {
		Time.timeScale = 0;
		anim.SetTrigger("FadeOut");
		yield return new WaitForSecondsRealtime(0.75f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	IEnumerator MainMenu() {
		mostEnemiesKilled = PlayerPrefs.GetInt("mostEnemiesKilled", 0);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		int enemiesKilled = player.GetComponent<PlayerHealth>().enemiesKilled;
		if(enemiesKilled > mostEnemiesKilled) {
			PlayerPrefs.SetInt("mostEnemiesKilled", enemiesKilled);
		}
		Time.timeScale = 0;
		anim.SetTrigger("FadeOut");
		yield return new WaitForSecondsRealtime(0.75f);
		totalDeaths++;
		PlayerPrefs.SetInt("totalDeaths", totalDeaths);
		PauseMenu.isPaused = false;
		SceneManager.LoadScene(0);
	}
}