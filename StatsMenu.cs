using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsMenu : MonoBehaviour {

	public TextMeshProUGUI KillsText;
	public TextMeshProUGUI DeathsText;
	public TextMeshProUGUI HighTimeText;
	public TextMeshProUGUI HighScoreText;
	public TextMeshProUGUI TotalScoreText;
	public TextMeshProUGUI MostKillsText;
	public TextMeshProUGUI TotalTimeText;

	void Start() {
		LoadText();
	}

	public void LoadText() {
		KillsText.text = "Total Kills: " + PlayerPrefs.GetInt("totalKills", 0);
		DeathsText.text = "Total Deaths: " + PlayerPrefs.GetInt("totalDeaths", 0);
		HighScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("highScore", 0);
		TotalScoreText.text = "Total Score: " + PlayerPrefs.GetInt("totalScore", 0);
		MostKillsText.text = "Most Enemies Killed: " + PlayerPrefs.GetInt("mostEnemiesKilled", 0);

		float time = PlayerPrefs.GetFloat("highTime", 0);
		float highHour = Mathf.FloorToInt(time/60/60%60);
		float highMin = Mathf.FloorToInt(time/60%60);
		float highSec = Mathf.FloorToInt(time%60);

		HighTimeText.text = "Longest Time Alive: " + highHour.ToString("00") + ":" + highMin.ToString("00") + ":" + highSec.ToString("00");

		float t = PlayerPrefs.GetFloat("totalTime", 0);
		float hours = Mathf.FloorToInt(t/60/60%60);
		float minutes = Mathf.FloorToInt(t/60%60);
		float seconds = Mathf.FloorToInt(t%60);

		TotalTimeText.text = "Total Time Played: " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
	}
}