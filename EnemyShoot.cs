using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

	public WeaponData weaponData;

	private bool isAbleToShoot;
	private GameObject bulletObj;
	private float timeBtwShots;
	private float startTimeBtwShots = 2f;
	
	void Start() {
		LoadData();

		timeBtwShots = 1f / startTimeBtwShots;
	}

	void Update() {
		if(isAbleToShoot == false || PauseMenu.isPaused) {
			return;
		}

		if(timeBtwShots <= 0) {
			Fire();
		} else {
			timeBtwShots -= Time.deltaTime;
		}
	}

	void Fire() {
		Vector3 placement = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
		Instantiate(bulletObj, placement, transform.rotation);
		timeBtwShots = 1f / startTimeBtwShots;
	}

	void LoadData() {
		isAbleToShoot = weaponData.CanShoot;
		bulletObj = weaponData.BulletObj;
		startTimeBtwShots = weaponData.FireRate;
	}
}