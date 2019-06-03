using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ButtonEvent : MonoBehaviourPun {

	private Transform playerList;

	private void Start() {
		playerList = GameObject.FindGameObjectWithTag("PlayerList").GetComponent<Transform>();

		if ( this.transform.parent == this ) {
			Debug.Log("isnotParent");
			this.transform.SetParent(playerList);
		}
	}

	public void OnChangeImg(Image img) {
		//if ( photonView.IsMine ) {
			Debug.Log(photonView.ViewID);
			Debug.Log(this.transform.GetChild(1).GetComponent<Image>().sprite.ToString());
			this.transform.GetChild(1).GetComponent<Image>().sprite = img.sprite;
			Debug.Log(this.transform.GetChild(1).GetComponent<Image>().sprite.ToString());
		//}
	}
}
