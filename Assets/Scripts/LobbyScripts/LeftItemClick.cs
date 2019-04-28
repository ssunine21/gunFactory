using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftItemClick : MonoBehaviour {
	public ITEMGROUP itemGroup;
	public GameObject itemObj = null;
	public Color changedColor = Color.white;
	public GameObject leftPanel = null;

	private Color currColor;
	private Image itemImage;
	private RectTransform itemRect;

	private void Start() {
		if ( itemObj == null ) itemObj = this.gameObject;

		itemImage = itemObj.GetComponent<Image>();
		itemRect = itemObj.GetComponent<RectTransform>();


		currColor = itemImage.color;
	}

	private void OnMouseDown() {

	}

	private void OnMouseEnter() {
		if ( LobbyManager.init.isMouseButton_down ) return;
		itemImage.color = changedColor;
		itemRect.localScale = new Vector3(1.04f, 1.04f, 1.04f);
	}

	private void OnMouseExit() {
		itemImage.color = currColor;
		itemRect.localScale = Vector3.one;
	}

}
