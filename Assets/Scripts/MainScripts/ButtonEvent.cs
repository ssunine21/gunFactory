using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{


	public Sprite mouseOver_Image = null;

	private void OnMouseOver() {
		Debug.Log("mouseOver");
		gameObject.GetComponent<SpriteRenderer>().sprite = mouseOver_Image;
	}
}
