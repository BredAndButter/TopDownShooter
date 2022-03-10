using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour {

	public TextMeshProUGUI textDisplay;
	public string[] sentences;
	private int index;
	public float textSpeed = 0.02f;

	public GameObject NextButton;
	public GameObject BackButton;

	public Image screenshotImage;
	public Sprite[] images;

	public Animator textAnim;
	public Animator imageAnim;

	void Update() {
		if(textDisplay.text == sentences[index] && index < sentences.Length-1) {
			NextButton.SetActive(true);
		} else {
			NextButton.SetActive(false);
		}

		if(textDisplay.text == sentences[index] && index > 0) {
			BackButton.SetActive(true);
		} else {
			BackButton.SetActive(false);
		}
	}

	public void Type() {
		StartCoroutine(TypeText());
	}

	IEnumerator TypeText() {
		textAnim.SetTrigger("NewText");
		imageAnim.SetTrigger("NewImage");
		screenshotImage.sprite = images[index];
		foreach(char letter in sentences[index].ToCharArray()) {
			textDisplay.text += letter;
			yield return new WaitForSeconds(textSpeed);
		}
	}

	public void NextSentence() {
		if(index < sentences.Length-1) {
			index++;
			textDisplay.text = "";
			StartCoroutine(TypeText());
		} else {
			textDisplay.text = "";
		}
	}

	public void PrevSentence() {
		if(index > 0) {
			index--;
			textDisplay.text = "";
			StartCoroutine(TypeText());
		} else {
			textDisplay.text = "";
		}
	}

	public void ClearText() {
		textDisplay.text = "";
		screenshotImage.sprite = null;
		NextButton.SetActive(false);
		BackButton.SetActive(false);
	}
}