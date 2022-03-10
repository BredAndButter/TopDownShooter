using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour {

	public Slider slider;
	public Gradient gradient;
	public Image fill;
	public TextMeshProUGUI HPText;

	public void SetMaxHealth(int health) {
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
		HPText.text = health + "/" + health;
	}

	public void SetHealth(int health, int maxHealth) {
		slider.value = health;

		fill.color = gradient.Evaluate(slider.normalizedValue);
		HPText.text = health + "/" + maxHealth;
	}
}