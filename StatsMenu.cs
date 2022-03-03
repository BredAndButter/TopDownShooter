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

	void Start() {
		KillsText.text = "Total Kills: " + PlayerPrefs.GetInt("totalKills", 0);
		DeathsText.text = "Total Deaths: " + PlayerPrefs.GetInt("totalDeaths", 0);
		HighTimeText.text = "Longest Time Alive: " + Mathf.Round(PlayerPrefs.GetFloat("highTime", 0) * 100f) / 100f + " Seconds";
		HighScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("highScore", 0);
		TotalScoreText.text = "Total Score: " + PlayerPrefs.GetInt("totalScore", 0);
		MostKillsText.text = "Most Enemies Killed: " + PlayerPrefs.GetInt("mostEnemiesKilled", 0);
	}
}