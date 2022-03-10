using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject {

	//Movement
	public float MoveSpeed;

	//Health
	public int MaxHealth;
	public int PassiveHealth;
	public float PassiveWaitTime;

	//Enemy only
	public int EnemyDamage;
	public float TimeBtwDmg;
	public float StoppingDistance;
	public float RetreatDistance;
	public int EnemyScore;

	//Particles
	public GameObject DeathParticles;

}