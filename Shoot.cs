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

	public AudioSource audio;
	public AudioClip clip;

	public int DmgUpgrade = 0;
	
	void Start() {
		LoadData(weaponData);

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

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Upgrade") {
			UpgradeData upgradeData = col.gameObject.GetComponent<UpgradeDataHolder>().upgradeData;

			DmgUpgrade += upgradeData.BulletDamageToAdd;

			if(upgradeData.AmmoToAdd > 0) {
				maxAmmo += upgradeData.AmmoToAdd;
				currentAmmmo = maxAmmo;
				ammoUI.DisplayAmmo(currentAmmmo, maxAmmo, isReloading);
			}

			bulletSpeed += upgradeData.SpeedToAdd;
			rateOfFire += upgradeData.FireRateFaster;
			reloadTime -= upgradeData.ReloadTimeFaster;

			if(upgradeData.NewBullet && upgradeData.NewWeaponData) {
				weaponData = upgradeData.NewWeaponData;
				LoadData(weaponData);
				currentAmmmo = maxAmmo;
				ammoUI.DisplayAmmo(currentAmmmo, maxAmmo, isReloading);
				bulletObj = upgradeData.NewBullet;
			}

			Destroy(col.gameObject);
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

		audio.PlayOneShot(clip, 1f);

		currentAmmmo--;
		ammoUI.DisplayAmmo(currentAmmmo, maxAmmo, isReloading);
	}

	void LoadData(WeaponData weaponData) {
		bulletObj = weaponData.BulletObj;
		bulletSpeed = weaponData.BulletSpeed;
		rateOfFire = weaponData.FireRate;
		maxAmmo = weaponData.AmmoMax;
		reloadTime = weaponData.TimeToReload;
	}
}