using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public Animator anim;
	private int totalDeaths;

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
		Time.timeScale = 0;
		anim.SetTrigger("FadeOut");
		yield return new WaitForSecondsRealtime(0.75f);
		totalDeaths++;
		PlayerPrefs.SetInt("totalDeaths", totalDeaths);
		SceneManager.LoadScene(0);
	}
}