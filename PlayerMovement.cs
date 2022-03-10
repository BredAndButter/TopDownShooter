using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public PlayerData playerData;

	private float moveSpeed;

	public Rigidbody2D rb;
	public Camera cam;
	Vector2 movement;
	Vector2 mousePos;

	void Start() {
		moveSpeed = playerData.MoveSpeed;
	}

	void Update() {
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		movement.Normalize();

		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
	}

	void FixedUpdate() {
		rb.velocity = new Vector2(movement.y * moveSpeed, rb.velocity.y);
		rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.x);

		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
	}

	void OnTriggerEnter2D(Collider2D col) {
		UpgradeData upgradeData = col.gameObject.GetComponent<UpgradeDataHolder>().upgradeData;

		moveSpeed += upgradeData.SpeedToAdd;
	}
}