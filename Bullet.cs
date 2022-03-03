using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public WeaponData weaponData;

	public int bulletDamage;
	
	private bool isEnemy;
	private float delayDestroy;
	private float bulletSpeed;
	private bool canExplode;
	private float explosionField;
	private float explosionForce;
	private float destroyExplosionTime;

	private LayerMask layersToHit;
	private GameObject explosionPrefab;

	private GameObject player;
	private Vector2 target;

	private Rigidbody2D rb;

	void Start() {
		LoadData();

		if(isEnemy) {
			gameObject.layer = 9;

			rb = GetComponent<Rigidbody2D>();
			player = GameObject.FindGameObjectWithTag("Player");
			target = (player.transform.position - transform.position).normalized * bulletSpeed;
			rb.AddForce(target * bulletSpeed, ForceMode2D.Impulse);
		} else {
			return;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(canExplode) {
			Explosion();
		} else {
    		Destroy(gameObject);
    	}
	}

	IEnumerator SelfDestruct() {
		yield return new WaitForSeconds(delayDestroy);
    	if(canExplode) {
			Explosion();
		} else {
    		Destroy(gameObject);
    	}
	}

	void Explosion() {
		Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionField, layersToHit);
		
		foreach(Collider2D obj in objects) {
			Vector2 dir = obj.transform.position - transform.position;
			obj.GetComponent<Rigidbody2D>().AddForce(dir * explosionForce);

			if(obj.GetComponent<EnemyHealth>()) {
				obj.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
			} if(obj.GetComponent<PlayerHealth>()) {
				obj.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
			}
		}

		GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, transform.rotation);
		Destroy(explosionEffect, destroyExplosionTime);
		Destroy(gameObject);
	}

	void LoadData() {
		bulletDamage = weaponData.BulletDamage;

		isEnemy = weaponData.IsEnemy;
		bulletSpeed = weaponData.BulletSpeed;
		delayDestroy = weaponData.DelayDestroy;
		canExplode = weaponData.CanExplode;
		explosionField = weaponData.ExplosionField;
		explosionForce = weaponData.ExplosionForce;
		layersToHit = weaponData.LayersToHit;
		explosionPrefab = weaponData.ExplosionPrefab;
		destroyExplosionTime = weaponData.DestroyExplosionTime;

		StartCoroutine(SelfDestruct());
	}
}