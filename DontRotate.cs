using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRotate : MonoBehaviour {

	public GameObject obj;
	public float offset;

	void Update() {
		transform.eulerAngles = new Vector2(0, 0);
    	transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + offset);
	}

}