using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public PlayerData playerData;

	public int enemyDamage;
	public float timeBtwDmg;

	private float speed;
	private float stoppingDistance;
	private float retreatDistance;

	private Transform player;
	public Rigidbody2D rb;

	void Start() {
		LoadData();

		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update() {
		if(Vector2.Distance(transform.position, player.position) > stoppingDistance) {
			transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		} else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance) {
			transform.position = this.transform.position;
		} else if(Vector2.Distance(transform.position, player.position) < retreatDistance) {
			transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
		}
	}

	void FixedUpdate() {
		Vector2 lookDir = player.position - transform.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
	}

	void LoadData() {
		enemyDamage = playerData.EnemyDamage;
		timeBtwDmg = playerData.TimeBtwDmg;

		speed = playerData.MoveSpeed;
		stoppingDistance = playerData.StoppingDistance;
		retreatDistance = playerData.RetreatDistance;
	}
}