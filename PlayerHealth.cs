using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour {

	public PlayerData playerData;

	private int maxHealth;
	private int passiveHealth;
	private float passiveWaitTime;

	public int currentHealth;

	public HealthBar healthBar;
	public SceneLoader sceneLoader;

	private Color tmp;
	private Color gunTmp;
	public SpriteRenderer gun;

	private bool touchingEnemy = false;
	private bool isTakingDamage = false;
	private int numberOfEnemies = 0;

	private GameObject deathParticles;

	private float highTime;
	private float startTime;
	private float totalTime;

	private float score;
	public int addedScore;
	private int highScore;
	private int totalScore;
	private float t;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI timeText;

	public int enemiesKilled = 0;
	private int mostEnemiesKilled;

	public Animator anim;
	public TextMeshProUGUI upgradeText;

	void Start() {
		LoadData();
		highTime = PlayerPrefs.GetFloat("highTime", 0);
		highScore = PlayerPrefs.GetInt("highScore", 0);
		totalScore = PlayerPrefs.GetInt("totalScore", 0);
		mostEnemiesKilled = PlayerPrefs.GetInt("mostEnemiesKilled", 0);
		totalTime = PlayerPrefs.GetFloat("totalTime", 0);
		startTime = Time.time;

		Color tmp = GetComponent<SpriteRenderer>().color;
		Color gunTmp = gun.color;

		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		StartCoroutine(addHealth());
	}

	void Update() {
		t = Time.time - startTime;
		score = t + addedScore;

		float hours = Mathf.FloorToInt(t/60/60%60);
		float minutes = Mathf.FloorToInt(t/60%60);
		float seconds = Mathf.FloorToInt(t%60);

		scoreText.text = "Score: " + Mathf.FloorToInt(score);
		timeText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");

		if(currentHealth <= 0) {
			tmp.a = 0;
			gunTmp.a = 0;
			GetComponent<SpriteRenderer>().color = tmp;
			gun.color = gunTmp;

			PlayerPrefs.SetInt("totalScore", totalScore + Mathf.FloorToInt(score));
			PlayerPrefs.SetFloat("totalTime", totalTime + (Time.time - startTime));

			if(Time.time - startTime > highTime) {
				PlayerPrefs.SetFloat("highTime", Time.time - startTime);
			} if(score > highScore) {
				PlayerPrefs.SetInt("highScore", Mathf.FloorToInt(score));
			} if(enemiesKilled > mostEnemiesKilled) {
				PlayerPrefs.SetInt("mostEnemiesKilled", enemiesKilled);
			}

			sceneLoader.LoadMain();
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Upgrade") {
			UpgradeData upgradeData = col.gameObject.GetComponent<UpgradeDataHolder>().upgradeData;

			upgradeText.text = upgradeData.UpgradeDesc;
			anim.SetTrigger("UpgradeTrigger");

			maxHealth += upgradeData.HealthToAdd;
			currentHealth += upgradeData.HealthToAdd;
			healthBar.SetMaxHealth(maxHealth);
			healthBar.SetHealth(currentHealth, maxHealth);

			passiveHealth += upgradeData.PassiveHealthAdd;
			passiveWaitTime -= upgradeData.PassiveWaitSubtract;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {

		if(col.gameObject.tag == "EnemyBullet") {
			Bullet enemyBullet = col.gameObject.GetComponent<Bullet>();
			TakeDamage(enemyBullet.bulletDamage);
		}

		if(col.gameObject.tag == "Enemy") {
			isTakingDamage = true;
			touchingEnemy = true;
			numberOfEnemies++;
			EnemyMovement enemyMovement = col.gameObject.GetComponent<EnemyMovement>();
			StartCoroutine(takeHealth(enemyMovement.enemyDamage, enemyMovement.timeBtwDmg, numberOfEnemies));
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "Enemy") {
			isTakingDamage = false;
			touchingEnemy = false;
			numberOfEnemies--;
		}
	}

	public void EnemyKilled() {
		enemiesKilled += 1;
	}

	public void TakeDamage(int damage) {
		if(currentHealth >= 0) {
			if(currentHealth - damage < 0) {
				currentHealth = 0;
				healthBar.SetHealth(0, maxHealth);
			} else {
				currentHealth -= damage;

				healthBar.SetHealth(currentHealth, maxHealth);
			}
		} else {
			currentHealth = 0;
			healthBar.SetHealth(0, maxHealth);
		}
	}

	void AddHealth(int health) {
		if(currentHealth <= maxHealth) {
			if(currentHealth + health > maxHealth){
				currentHealth = maxHealth;

				healthBar.SetHealth(maxHealth, maxHealth);
			} else {
				currentHealth += health;

				healthBar.SetHealth(currentHealth, maxHealth);
			}
		} else {
			currentHealth = maxHealth;

			healthBar.SetHealth(maxHealth, maxHealth);
		}
	}

	IEnumerator addHealth() {
		while(true) {
			if(!isTakingDamage) {
				yield return new WaitForSeconds(passiveWaitTime);
				AddHealth(passiveHealth);
			} else {
				yield return null;
			}
		}
	}

	IEnumerator takeHealth(int damage, float waitTime, int numOfEnemy) {
		while(touchingEnemy) {
			TakeDamage(damage * numOfEnemy);
			yield return new WaitForSeconds(waitTime);
		}
	}

	void LoadData() {
		maxHealth = playerData.MaxHealth;
		passiveHealth = playerData.PassiveHealth;
		passiveWaitTime = playerData.PassiveWaitTime;
		deathParticles = playerData.DeathParticles;
	}
}