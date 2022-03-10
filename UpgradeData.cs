using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/UpgradeData")]
public class UpgradeData : ScriptableObject {

	public string UpgradeDesc;

	public int HealthToAdd;
	public int PassiveHealthAdd;
	public float PassiveWaitSubtract;
	public float SpeedToAdd;
	public int AmmoToAdd;
	public float ReloadTimeFaster;
	public float FireRateFaster;
	public int BulletDamageToAdd;

	public GameObject NewBullet;
	public WeaponData NewWeaponData;

}