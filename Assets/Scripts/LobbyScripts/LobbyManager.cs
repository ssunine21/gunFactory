using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks {
	private string gameVersion = "1";

	public Text connectionInfoText;
	public Button joinButton;

	//매치메이킹 시도
	private void Start() {
		PhotonNetwork.GameVersion = gameVersion;
		PhotonNetwork.ConnectUsingSettings();

		joinButton.interactable = false;
		connectionInfoText.text = "서버 접속 중...";
	}

	//서버에 접속한 경우
	public override void OnConnectedToMaster() {
		joinButton.interactable = true;
		connectionInfoText.text = "서버 연결 됨";
	}

	//서버 끊긴 경우
	public override void OnDisconnected( DisconnectCause cause ) {
		joinButton.interactable = false;
		connectionInfoText.text = "연결되지 않음... 재시도 중...";

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
			connectionInfoText.text = "연결되지 않음... 재시도 중...";
			PhotonNetwork.ConnectUsingSettings();
		}
	}

	//방이 없어서 접속에 실패했을 때
	public override void OnJoinRandomFailed( short returnCode, string message ) {
		connectionInfoText.text = "새로운 방 생성";
		PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
	}

	//참가 성공
	public override void OnJoinedRoom() {
		connectionInfoText.text = "방 참가 성공";
		PhotonNetwork.LoadLevel("PlayScene");
	}
}
