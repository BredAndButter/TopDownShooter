using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour {

	public TextMeshProUGUI AmmoText;
	public TextMeshProUGUI ReloadText;

	public void DisplayAmmo(int ammo, int maxAmmo, bool isReloading) {
		if(isReloading) {
			ReloadText.text = "Reloading...";
			AmmoText.text = ammo + "/" + maxAmmo;
		} else {
			ReloadText.text = "";
			AmmoText.text = ammo + "/" + maxAmmo;
		}
	}
}