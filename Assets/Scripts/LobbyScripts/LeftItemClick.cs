//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class LeftItemClick : MonoBehaviour {

//	public int idx = 0;
//	public ITEMGROUP itemGroup;


//	private float rotSpeed = 50f;
//	private RectTransform itemRect;
//	private Quaternion currRotation;

//	private void Start() {
//		itemRect = GetComponent<RectTransform>();
//		currRotation = itemRect.rotation;
//	}

//	private void Update() {
//		if ( LeftPanel.init.idx == idx ) {
//			itemRect.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
//		}
//		else {
//			itemRect.rotation = currRotation;
//		}
//	}

//	private void OnMouseDown() {
//		LeftPanel.init.ChangeItemValue(idx);
//		LobbyManager.init.ChangeImage(itemGroup, this.GetComponent<Image>().sprite);

//		itemRect.localScale = Vector3.one;
//	}

//	private void OnMouseEnter() {
//		if ( LobbyManager.init.isMouseButton_down && LeftPanel.init.idx == idx) return;
//		itemRect.localScale = new Vector3(1.04f, 1.04f, 1.04f);
//	}

//	private void OnMouseExit() {
//		if ( LeftPanel.init.idx == idx ) return;
	
//		itemRect.localScale = Vector3.one;
//	}

//}
