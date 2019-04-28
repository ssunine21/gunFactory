using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour
{
	public GameObject itemObj = null;
	public Color changedColor = Color.white;


	private Color currColor;
	private Image itemImage;
	private RectTransform itemRect;

	private void Start() {
		itemImage = itemObj.GetComponent<Image>();
		itemRect = itemObj.GetComponent<RectTransform>();

		currColor = itemImage.color;
	}

	public void OnMouseEnter() {
		if ( LobbyManager.init.isMouseButton_down ) return;
		itemImage.color = changedColor;
		itemRect.localScale = new Vector3(1.04f, 1.04f, 1.04f);
	}

	public void OnMouseExit() {
		itemImage.color = currColor;
		itemRect.localScale = Vector3.one;
	}
	
}
