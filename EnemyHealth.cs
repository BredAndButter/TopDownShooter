using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public PlayerData playerData;

	private int maxHealth = 100;

	public int currentHealth;

	public EnemyHealthBar enemyHealthBar;

	private int totalKills;

	private GameObject player;

	private int enemyScore;

	void Start() {
		enemyScore = playerData.EnemyScore;
		maxHealth = playerData.MaxHealth;
		currentHealth = maxHealth;
		enemyHealthBar.SetMaxHealth(maxHealth);
		totalKills = PlayerPrefs.GetInt("totalKills", 0);
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		if(currentHealth <= 0) {
			totalKills++;
			PlayerPrefs.SetInt("totalKills", totalKills);
			player.GetComponent<PlayerHealth>().addedScore += enemyScore;
			player.GetComponent<PlayerHealth>().enemiesKilled += 1;
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {

		if(col.gameObject.tag == "Bullet") {
			Bullet bullet = col.gameObject.GetComponent<Bullet>();
			TakeDamage(bullet.bulletDamage);
		}

	}

	public void TakeDamage(int damage) {
		if(currentHealth >= 0) {
			if(currentHealth - damage < 0) {
				currentHealth = 0;
				enemyHealthBar.SetHealth(0);
			} else {
				currentHealth -= damage;

				enemyHealthBar.SetHealth(currentHealth);
			}
		} else {
			currentHealth = 0;
			enemyHealthBar.SetHealth(0);
		}
	}
}