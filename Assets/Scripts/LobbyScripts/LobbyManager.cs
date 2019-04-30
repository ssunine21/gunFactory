using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ITEMGROUP { SecondWP = 0, MainWP, SubItem };

public class LobbyManager : MonoBehaviour
{
	public GameObject[] itemMounting;
	public Transform targetTr = null;
	public float rotSpeed = 2f;
	public GameObject LeftPanel = null;

	[HideInInspector]
	public bool isMouseButton_down = false;

	private void Awake() {
		if ( init == null )
			init = this;
	}

	private void OnMouseDrag() {
		float mouseX = Input.GetAxisRaw("Mouse X") * rotSpeed;
		targetTr.Rotate(0, -mouseX, 0, Space.World);
	}

	private void Update() {
		if ( Input.GetMouseButtonDown(0) )
			isMouseButton_down = true;

		else if ( Input.GetMouseButtonUp(0) )
			isMouseButton_down = false;
	}

	public void ChangeImage(ITEMGROUP itemGroup, Image image) {
		switch ( itemGroup ) {
			case ITEMGROUP.SecondWP:
				
				break;
			case ITEMGROUP.MainWP:
				break;
			case ITEMGROUP.SubItem:
				break;
		}
	}

	public static LobbyManager init = null;
}
