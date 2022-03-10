using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] enemies;
	public int[] rates;
	private int randomNum;
	public float radius = 32f;
	public bool canSpawn = true;
	public bool canReduceTime = true;
	public float timeBtwSpawn = 2f;
	public float maxSpawnTime;
	private float startTime;
	private int enemyLength;
	public int enemiesToSpawn = 2;
	public float updateRate = 20f;

	private bool canSpawnRocket = true;

	void Start() {
		startTime = Time.time;
		enemyLength = enemies.Length;
		StartCoroutine(SpawnAtRandomLocation());
	}

	void Update() {
		if(Time.time - startTime >= updateRate) {
			if(timeBtwSpawn > maxSpawnTime && canReduceTime) {
				timeBtwSpawn--;
			} if(enemiesToSpawn < enemyLength) {
				enemiesToSpawn++;
			}

			startTime = Time.time;
		}
	}

	IEnumerator SpawnAtRandomLocation() {
		while(true) {
			if(canSpawn) {
				Vector3 randomPos = Random.insideUnitCircle * radius;
				if(Physics2D.OverlapCircle(randomPos, 2) != null) {
					yield return null;
				} else {
					randomNum = Random.Range(0, rates[enemiesToSpawn-1]);

					for(int i = 0; i < rates.Length; i++) {
						if(randomNum <= rates[i]) {
							if(enemies[i].name == "RocketUpgrade" && canSpawnRocket) {
								Instantiate(enemies[i], randomPos, Quaternion.identity);
								canSpawnRocket = false;
							} else if(enemies[i].name == "RocketUpgrade" && !canSpawnRocket) {
								yield return null;
							} else {
								Instantiate(enemies[i], randomPos, Quaternion.identity);
								yield return new WaitForSeconds(timeBtwSpawn);
								break;
							}
						} else {
							continue;
						}
					}
				}
			} else {
				yield return null;
			}
		}
	}
}