using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyPlayerCustom : NetworkLobbyPlayer
{
	public GameObject playerList;
	public Text readyText;

	private Image jobImage;
	private Image temp_jobImage;
	private Button readyButton;

	[SerializeField]
	private Button[] job_Buttons;


	public override void OnClientEnterLobby() {
		base.OnClientEnterLobby();
		playerList = GameObject.FindGameObjectWithTag("PlayerList");
		readyButton = GameObject.FindGameObjectWithTag("Ready_button").GetComponent<Button>();
		job_Buttons = GameObject.FindGameObjectWithTag("Job_button").GetComponentsInChildren<Button>();
		jobImage = transform.Find("JobImg").GetComponent<Image>();
		OnClientReady(false);
		gameObject.transform.SetParent(playerList.transform, false);
		SetPlayer();
	}


	public override void OnStartLocalPlayer() {
		base.OnStartLocalPlayer();
		Debug.Log("OnstartLocalPlayer");

		if ( !isLocalPlayer ) {
			SetOtherPlayer();
		}

		SetReadyButton();
		SetJobButton();
	}

	private void SetPlayer() {
		//Text playerName = gameObject.transform.GetChild(1).GetComponent<Text>();
		//playerName.text = netId.ToString();

		Debug.Log("isLocalPlayer");

		SetReadyButton();
		SetJobButton();
	}

	private void SetOtherPlayer() {
		Debug.Log("NotLocalPlayer");
		readyButton.onClick.RemoveAllListeners();

		foreach ( var jobButton in job_Buttons )
			jobButton.onClick.RemoveAllListeners();
	}

	private void SetReadyButton() {
		if ( !isLocalPlayer ) return;

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

		foreach( var jobButton in job_Buttons ) {
			jobButton.onClick.RemoveAllListeners();
			jobButton.onClick.AddListener(() => ChangeJob(jobButton.GetComponent<Image>()));
		}
	}

	private void ChangeJob(Image job) {
		if ( !isLocalPlayer ) return;

		Debug.Log("changeImg");
		jobImage.sprite = job.sprite;

		//job.GetComponent<JobManager>().CmdFalse();
	}
}