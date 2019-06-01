//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//public class LeftPanel : MonoBehaviour
//{
//	private enum SPRITE { gun= 0, shotgun, flamegun, healpack }

//	private int _idx;
//	public int idx {
//		get { return _idx; }
//		set { _idx = value; }
//	}


//	public GunInfo[] gunInfo;
//	public Text nameText = null;
//	public GameObject[] itemImages;
//	public Sprite[] sprites;

//	public Image _damage;
//	public Image _speed;
//	public Image _volum;
//	public Image _reload;


//	private void Awake() {
//		if ( init == null ) init = this;
//	}

//	public void ChangeItemGroup(GunInfo[] item ) {
//		gunInfo = new GunInfo[item.Length];
//		gunInfo = item;
//		idx = 0;

//		ChangeItemValue(idx);

//		switch ( gunInfo[0].itemGroup ) {
//			case ITEMGROUP.SecondWP:
//				SetItemGroup(ITEMGROUP.SecondWP);

//				itemImages[0].GetComponent<Image>().sprite = sprites[(int)SPRITE.gun];
//				itemImages[0].GetComponent<Image>().SetNativeSize();
//				itemImages[1].SetActive(false);
//				break;

//			case ITEMGROUP.MainWP:
//				SetItemGroup(ITEMGROUP.MainWP);

//				if ( !itemImages[1].activeSelf ) itemImages[1].SetActive(true);
//				itemImages[0].GetComponent<Image>().sprite = sprites[(int)SPRITE.shotgun];
//				itemImages[0].GetComponent<Image>().SetNativeSize();
//				itemImages[1].GetComponent<Image>().sprite = sprites[(int)SPRITE.flamegun];
//				itemImages[1].GetComponent<Image>().SetNativeSize();
//				break;

//			case ITEMGROUP.SubItem:
//				SetItemGroup(ITEMGROUP.SubItem);

//				if ( !itemImages[1].activeSelf ) itemImages[1].SetActive(true);
//				itemImages[0].GetComponent<Image>().sprite = sprites[(int)SPRITE.healpack];
//				itemImages[0].GetComponent<Image>().SetNativeSize();
//				itemImages[1].GetComponent<Image>().sprite = sprites[(int)SPRITE.healpack];
//				itemImages[1].GetComponent<Image>().SetNativeSize();
//				//itemImage[1].SetActive(false);
//				break;
//		}
//	}

//	private void SetItemGroup(ITEMGROUP itemGroup) {
//		foreach(var itemImage in itemImages ) {
//			itemImage.GetComponent<LeftItemClick>().itemGroup = itemGroup;
//		}
//	}

//	public void ChangeItemValue( int temp_idx ) {
//		idx = temp_idx;
//		nameText.text = gunInfo[idx].text;

//		StartCoroutine(ChangeGunInfo());
//	}

//	private IEnumerator ChangeGunInfo() {
//		float gageSpeed = 0.03f;

//		InitfillAmount();

//		for(float i = gageSpeed; i < 1f; i+=gageSpeed ) {
//			if ( _damage.fillAmount <= (gunInfo[idx].damage * 0.01f) )
//				_damage.fillAmount += gageSpeed;

//			if ( _speed.fillAmount <= (1f - gunInfo[idx].shot_Spacing) )
//				_speed.fillAmount += gageSpeed;

//			if ( _volum.fillAmount <= (gunInfo[idx].maxbullet * 0.05f) )
//				_volum.fillAmount += gageSpeed;

//			if ( _reload.fillAmount <= (gunInfo[idx].reloadtime * 0.1f) )
//				_reload.fillAmount += gageSpeed;

//			yield return null;
//		}
//	}

//	private void InitfillAmount() {
//		_damage.fillAmount = _speed.fillAmount = _volum.fillAmount = _reload.fillAmount = 0;
//	}

//	public static LeftPanel init = null;
//}
