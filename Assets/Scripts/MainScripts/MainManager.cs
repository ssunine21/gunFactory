using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainManager : MonoBehaviourPunCallbacks, IPunObservable {

	public GameObject LobbyPlayer;
	public Transform playerList;
	public int jobidx = 0;

	private int readyCount = 1;

	public static MainManager init {
		get {
			if ( m_init == null ) m_init = FindObjectOfType<MainManager>();

			return m_init;
		}
	}

	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info ) {
		if ( stream.IsWriting ) {
			stream.SendNext(readyCount);
		}
		else {
			readyCount = (int)stream.ReceiveNext();
		}
	}

	private void Start() {
		PhotonNetwork.IsMessageQueueRunning = true;
		Debug.Log("dd");

		//PhotonNetwork.Instantiate(LobbyPlayer.name, Vector3.zero, Quaternion.identity).transform.SetParent(playerList);
	}

	public void OnReady() {
		if ( PhotonNetwork.IsMasterClient ) {
			Debug.Log(readyCount);
			//if ( PhotonNetwork.CountOfPlayers < readyCount )

			DontDestroyOnLoad(this.gameObject);
			PhotonNetwork.LoadLevel("PlayScene");
		}
		else {
			readyCount += 1;

			Debug.Log(readyCount);
		}
	}

	private static MainManager m_init;
}
