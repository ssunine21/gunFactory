//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//[System.Serializable]
//public struct GunInfo {
//	public ITEMGROUP itemGroup;
//	public string text;
//	public float damage;
//	public float shot_Spacing;
//	public float maxbullet;
//	public float reloadtime;
//}

//public class ItemClick : MonoBehaviour
//{
//	public GunInfo[] gunInfo;
//	public GameObject itemObj = null;
//	public Color changedColor = Color.white;
//	public GameObject leftPanel = null;

//	private Color currColor;
//	private Image itemImage;
//	private RectTransform itemRect;

//	private void Start() {
//		if ( itemObj == null ) itemObj = this.gameObject;

//		itemImage = itemObj.GetComponent<Image>();
//		itemRect = itemObj.GetComponent<RectTransform>();


//		currColor = itemImage.color;
//	}

//	private void OnMouseDown() {
//		if ( !leftPanel.activeSelf )
//			leftPanel.SetActive(true);

//		LeftPanel.init.ChangeItemGroup(gunInfo);
//	}

//	private void OnMouseEnter() {
//		if ( LobbyManager.init.isMouseButton_down ) return;
//		itemImage.color = changedColor;
//		itemRect.localScale = new Vector3(1.04f, 1.04f, 1.04f);
//	}

//	private void OnMouseExit() {
//		itemImage.color = currColor;
//		itemRect.localScale = Vector3.one;
//	}
	
//}
