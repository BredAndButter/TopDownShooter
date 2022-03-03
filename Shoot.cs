using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public WeaponData weaponData;

	private GameObject bulletObj;
	private float bulletSpeed;
	private float rateOfFire;
	private int maxAmmo;
	private float reloadTime;
	
	public Transform firePoint;
	public AmmoUI ammoUI;
	private int currentAmmmo;
	private bool isReloading = false;
	private float nextTimeToFire = 0f;
	
	void Start() {
		LoadData();

		currentAmmmo = maxAmmo;
		ammoUI.DisplayAmmo(currentAmmmo, maxAmmo, isReloading);
	}

	void Update() {
		if(PauseMenu.isPaused) {
			return;
		}

		if(isReloading) {
			return;
		}

		if(currentAmmmo <= 0) {
			StartCoroutine(Reload());
			return;
		}

		if(Input.GetKeyDown(KeyCode.R) && currentAmmmo < maxAmmo) {
			StartCoroutine(Reload());
			return;
		}

		if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire) {
			nextTimeToFire = Time.time + 1f / rateOfFire;
			Fire();
		}
	}

	IEnumerator Reload() {
		isReloading = true;
		ammoUI.DisplayAmmo(currentAmmmo, maxAmmo, isReloading);
		yield return new WaitForSeconds(reloadTime);

		currentAmmmo = maxAmmo;
		isReloading = false;
		ammoUI.DisplayAmmo(currentAmmmo, maxAmmo, isReloading);
	}

	void Fire() {
		GameObject bullet = Instantiate(bulletObj, firePoint.position, firePoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);

		currentAmmmo--;
		ammoUI.DisplayAmmo(currentAmmmo, maxAmmo, isReloading);
	}

	void LoadData() {
		bulletObj = weaponData.BulletObj;
		bulletSpeed = weaponData.BulletSpeed;
		rateOfFire = weaponData.FireRate;
		maxAmmo = weaponData.AmmoMax;
		reloadTime = weaponData.TimeToReload;
	}
}