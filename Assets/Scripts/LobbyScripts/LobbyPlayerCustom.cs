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
	private Image img_temp;
	
	private Button readyButton;
	//[SerializeField]
	private Button[] job_Buttons;
	private bool isButton = false;


	// 2. 로비에 입장, 아직 로컬플레이어 지정 안 됨
	public override void OnClientEnterLobby() {
		base.OnClientEnterLobby();
		Debug.Log("OnClientEnterLobby");

		playerList = GameObject.FindGameObjectWithTag("PlayerList");
		readyButton = GameObject.FindGameObjectWithTag("Ready_button").GetComponent<Button>();
		job_Buttons = GameObject.FindGameObjectWithTag("Job_button").GetComponentsInChildren<Button>();
		gameObject.transform.SetParent(playerList.transform, false);
		img_temp = jobImage;

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
	}

	private void SetOtherPlayer() {
		Debug.Log("SetOtherPlayer");
		readyButton.onClick.RemoveAllListeners();
		job_Buttons[0].onClick.RemoveAllListeners();


	}

	private void SetReadyButton() {
		//if ( !isLocalPlayer ) return;

		Debug.Log("SetReadyButton");

		readyButton.onClick.RemoveAllListeners();
		job_Buttons[0].onClick.RemoveAllListeners();

		if ( !readyToBegin ) {
			Debug.Log("isReady");
			readyButton.onClick.AddListener(() => SendReadyToBeginMessage());
		}
		else {
			Debug.Log("notReady");
			readyButton.onClick.AddListener(() => SendNotReadyToBeginMessage());
		}

		//for(int i = 0; i < job_Buttons.Length; ++i ) {
			job_Buttons[0].onClick.AddListener(() => SetJobButton(job_Buttons[0].GetComponent<Image>()));
		//}
	}

	public override void OnClientReady( bool readyState ) {
		base.OnClientReady(readyState);
		
		Debug.Log("OnClientReady");


		readyText.text = readyState == false ? ". . ." : "준비됨";
		jobImage.sprite = img_temp.sprite;

		SetReadyButton();
	}

	public void SetJobButton(Image jobImg) {
		if ( !isLocalPlayer ) return;

		isButton = true;
		img_temp.sprite = jobImg.sprite;

		SendNotReadyToBeginMessage();

		Debug.Log("SetJobButton");
	}
	
}