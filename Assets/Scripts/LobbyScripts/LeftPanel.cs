using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeftPanel : MonoBehaviour
{
	private enum SPRITE { gun= 0, shotgun, flamegun, healpack }

	public int idx = 0;
	public GunInfo[] gunInfo;
	public Text nameText = null;
	public GameObject[] itemImage;
	public Sprite[] sprites;

	private void Awake() {
		if ( init == null ) init = this;
	}

	private void Update() {
	}

	public void ChangeItemGroup(GunInfo[] item ) {
		gunInfo = new GunInfo[item.Length];
		gunInfo = item;

		switch ( gunInfo[0].itemGroup ) {
			case ITEMGROUP.SecondWP:
				itemImage[0].GetComponent<Image>().sprite = sprites[(int)SPRITE.gun];
				itemImage[0].GetComponent<Image>().SetNativeSize();
				itemImage[1].SetActive(false);
				break;

			case ITEMGROUP.MainWP:
				if ( !itemImage[1].activeSelf ) itemImage[1].SetActive(true);
				itemImage[0].GetComponent<Image>().sprite = sprites[(int)SPRITE.shotgun];
				itemImage[0].GetComponent<Image>().SetNativeSize();
				itemImage[1].GetComponent<Image>().sprite = sprites[(int)SPRITE.flamegun];
				itemImage[1].GetComponent<Image>().SetNativeSize();
				break;

			case ITEMGROUP.SubItem:
				itemImage[0].GetComponent<Image>().sprite = sprites[(int)SPRITE.healpack];
				itemImage[0].GetComponent<Image>().SetNativeSize();
				itemImage[1].SetActive(false);
				break;
		}
	}

	public void ChangeItemValue() {

	}

	public static LeftPanel init = null;
}
