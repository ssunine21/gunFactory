using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class JobManager : NetworkBehaviour {
	private Button thisButton;
	private Image jobImg;

	[SerializeField]
	private Image playerJobImg;

	private void Awake() {
		thisButton = GetComponent<Button>();
		jobImg = GetComponent<Image>();
	}
	
	[Command]
	public void CmdChangeJob() {
		if(!playerJobImg) playerJobImg = GameObject.FindGameObjectWithTag("Lobby_Player").transform.Find("JobImg").GetComponent<Image>();


		playerJobImg.sprite = jobImg.sprite;
	}

	//public override void OnStartClient() {
	//	base.OnStartClient();

	//	playerJobImg = GameObject.FindGameObjectWithTag("Lobby_Player").transform.Find("JobImg").GetComponent<Image>();
	//}
	
}
