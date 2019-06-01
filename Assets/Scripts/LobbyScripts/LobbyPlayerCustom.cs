using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyPlayerCustom : NetworkLobbyPlayer
{
	public GameObject playerList;
	public Text readyText;
	public Image jobImage;
	
	private Button readyButton;


	// 2. 로비에 입장, 아직 로컬플레이어 지정 안 됨
	public override void OnClientEnterLobby() {
		base.OnClientEnterLobby();
		Debug.Log("OnClientEnterLobby");

		playerList = GameObject.FindGameObjectWithTag("PlayerList");
		readyButton = GameObject.FindGameObjectWithTag("Ready_button").GetComponent<Button>();
		gameObject.transform.SetParent(playerList.transform, false);

		OnClientReady(false);
	}

	// 3. 로컬플레이어 지정
	public override void OnStartLocalPlayer() {
		base.OnStartLocalPlayer();
		Debug.Log("OnstartLocalPlayer");

		if ( !isLocalPlayer ) {
			SetOtherPlayer();
		}
		else SetPlayer();
	}

	private void SetPlayer() {

		Debug.Log("SetPlayer");

		SetReadyButton();
		SetJobButton();
	}

	private void SetOtherPlayer() {
		Debug.Log("SetOtherPlayer");
		readyButton.onClick.RemoveAllListeners();
		
	}

	private void SetReadyButton() {
		if ( !isLocalPlayer ) return;

		Debug.Log("SetReadyButton");

		readyButton.onClick.RemoveAllListeners();

		if ( !readyToBegin ) {
			Debug.Log("isReady");
			readyButton.onClick.AddListener(() => SendReadyToBeginMessage());
		}
		else {
			Debug.Log("notReady");
			readyButton.onClick.AddListener(() => SendNotReadyToBeginMessage());
		}
	}

	public override void OnClientReady( bool readyState ) {
		base.OnClientReady(readyState);
		
		Debug.Log("OnClientReady");

		readyText.text = readyState == false ? ". . ." : "준비됨";

		SetReadyButton();
	}

	private void SetJobButton() {
		if ( !isLocalPlayer ) return;

		Debug.Log("SetJobButton");
	}
	
}