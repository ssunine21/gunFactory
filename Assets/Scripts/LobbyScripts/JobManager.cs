using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class JobManager : NetworkBehaviour {


	public GameObject jobButton;
	public Sprite[] jobImg;

	[SerializeField]
	private Image playerJobImg;

	private void Awake() {
		if ( init ) Destroy(init);
		init = this;
	}


	//[Command]
	//public void CmdFalse() {
	//	thisButton.interactable = false;
	//}

	[Command]
	public void CmdSpawnJobButton() {
		for ( int i = 0; i < jobImg.Length; ++i ) {
			GameObject spawnButton = Instantiate(jobButton, this.transform);
			spawnButton.GetComponent<Image>().sprite = jobImg[i];

			NetworkServer.Spawn(spawnButton);
		}
	}

	//public override void OnStartClient() {
	//	base.OnStartClient();

	//	playerJobImg = GameObject.FindGameObjectWithTag("Lobby_Player").transform.Find("JobImg").GetComponent<Image>();
	//}


	static public JobManager init;
}
