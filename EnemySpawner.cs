using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] enemies;
	public float radius = 32f;
	public bool canSpawn = true;
	public float timeBtwSpawn = 2f;
	private float startTime;
	private int enemyLength;
	public int enemiesToSpawn = 2;
	public float updateRate = 20f;

	void Start() {
		startTime = Time.time;
		enemyLength = enemies.Length;
		StartCoroutine(SpawnAtRandomLocation());
	}

	void Update() {
		if(Time.time - startTime >= updateRate) {
			if(timeBtwSpawn > 1) {
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
				Instantiate(enemies[Random.Range(0, enemiesToSpawn)], randomPos, Quaternion.identity);
				yield return new WaitForSeconds(timeBtwSpawn);
			} else {
				yield return null;
			}
		}
	}
}