using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject {

	//Enemy only
	public bool IsEnemy;
	public bool CanShoot;
	public float DistanceToShoot;

	//General
	public int BulletDamage;
	public float FireRate;
	public float BulletSpeed;
	public float DelayDestroy;

	//Player only
	public int AmmoMax;
	public float TimeToReload;
	
	//Explosions
	public bool CanExplode;
	public float ExplosionField;
	public float ExplosionForce;
	public float DestroyExplosionTime;
	public LayerMask LayersToHit;
	public GameObject BulletObj;
	public GameObject ExplosionPrefab;

}