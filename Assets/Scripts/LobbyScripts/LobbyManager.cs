using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks {
	private string gameVersion = "1";

	public Text connectionInfoText;
	public Button joinButton;
	public GameObject canvas;

	public GameObject room;
	public Transform roomListTr;
	public Text roomText;
	public Text maxPlayer;
	

	//매치메이킹 시도
	private void Start() {
		PhotonNetwork.GameVersion = gameVersion;
		PhotonNetwork.ConnectUsingSettings();
		PhotonNetwork.AutomaticallySyncScene = true;
		connectionInfoText.gameObject.SetActive(true);
		joinButton.interactable = false;
		connectionInfoText.text = "서버에 접속하는 중";
	}

	private void Update() {
		if ( Input.GetKeyDown(KeyCode.A) ) {
			if( canvas.GetComponentInChildren<Button>().interactable == false )
				canvas.GetComponentInChildren<Button>().interactable = true;
			else
				canvas.GetComponentInChildren<Button>().interactable = false;
		}
	}


	//서버에 접속한 경우
	public override void OnConnectedToMaster() {
		joinButton.interactable = true;
		canvas.SetActive(true);
		connectionInfoText.text = "서버 접속";

		PhotonNetwork.JoinLobby();

		Invoke("SetActiveObject", 1f);
	}

	private void SetActiveObject() {
		connectionInfoText.gameObject.SetActive(false);
	}

	//서버 끊긴 경우
	public override void OnDisconnected( DisconnectCause cause ) {
		joinButton.interactable = false;
		connectionInfoText.text = "접속 실패! 접속 재시도...";

		PhotonNetwork.ConnectUsingSettings();
	}

	//Join Button을 클릭했을 때 실행 (무작위 룸에 접속 시도)
	public void Connect() {
		joinButton.interactable = false;

		if ( PhotonNetwork.IsConnected ) {
			connectionInfoText.text = "접속";
			PhotonNetwork.JoinRandomRoom();
		}
		else {
			connectionInfoText.text = "접속 실패! 접속 재시도...";
			PhotonNetwork.ConnectUsingSettings();
		}
	}

	//방이 없어서 접속에 실패했을 때
	public override void OnJoinRandomFailed( short returnCode, string message ) {
		Debug.Log("OnJoinRandomFailed");
		connectionInfoText.text = "새로운 방 생성";
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
	}

	//참가 성공
	public override void OnJoinedRoom() {
		Debug.Log("OnjoinedRoom");
		connectionInfoText.text = "방 참가 성공";
		PhotonNetwork.IsMessageQueueRunning = false;
		//SceneManager.LoadScene("LobbyScene");
		PhotonNetwork.LoadLevel("LobbyScene");
	}

	public override void OnJoinedLobby() {
		Debug.Log("Joined Lobby");
	}

	//방 만들기
	public void OnCreateRoom() {
		Debug.Log("OnCreateRoom");
		PhotonNetwork.CreateRoom(roomText.text, new RoomOptions { MaxPlayers = 2 });
	}


	public override void OnRoomListUpdate( List<RoomInfo> roomList ) {

		Debug.Log("OnRoomListUpdate");

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Room") ) {
			Destroy(obj);
		}

		foreach(RoomInfo roomInfo in roomList ) {
			GameObject _room = Instantiate(room, roomListTr);
			RoomData roomData = _room.GetComponent<RoomData>();
			roomData.roomName = roomInfo.Name;
			roomData.maxPlayer = roomInfo.MaxPlayers;
			roomData.currPlayer = roomInfo.PlayerCount;

			roomData.UpdateInfo();
			roomData.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnClickRoom(roomData.roomName));
		}
	}

	private void OnClickRoom(string roomName ) {
		Debug.Log("OnClickRoom");
		PhotonNetwork.JoinRoom(roomName, null);
	}

}
