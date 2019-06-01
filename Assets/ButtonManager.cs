using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
	private Button thisButton;
	private Image jobImg;

	private void Awake() {
		thisButton = GetComponent<Button>();
		jobImg = GetComponent<Image>();
	}

	public void OnClick() {
		
	}
}
