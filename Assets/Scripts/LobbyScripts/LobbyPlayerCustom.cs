using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyPlayerCustom : NetworkLobbyPlayer
{
	public GameObject playerList;
	public Text readyText;

	private Button readyButton;


	public override void OnClientEnterLobby() {
		base.OnClientEnterLobby();
		playerList = GameObject.FindGameObjectWithTag("PlayerList");
		readyButton = GameObject.FindGameObjectWithTag("Ready_button").GetComponent<Button>();
		OnClientReady(false);
		gameObject.transform.SetParent(playerList.transform, false);
		SetPlayer();
	}

	public override void OnStartLocalPlayer() {
		base.OnStartLocalPlayer();
		if ( !isLocalPlayer ) SetOtherPlayer();
	}

	private void SetPlayer() {
		//Text playerName = gameObject.transform.GetChild(1).GetComponent<Text>();
		//playerName.text = netId.ToString();

		SetReadyButton();
	}

	private void SetOtherPlayer() {
		readyButton.onClick.RemoveAllListeners();
	}

	public void SetReadyButton() {
		if ( !isLocalPslayer ) return;

		readyButton.onClick.RemoveAllListeners();
		if ( !readyToBegin )
			readyButton.onClick.AddListener(() => SendReadyToBeginMessage());
		else
			readyButton.onClick.AddListener(() => SendNotReadyToBeginMessage());
	}

	public override void OnClientReady( bool readyState ) {
		base.OnClientReady(readyState);

		readyText.text = readyState == false ? ". . ." : "준비됨";
		SetReadyButton();
		
	}
}
