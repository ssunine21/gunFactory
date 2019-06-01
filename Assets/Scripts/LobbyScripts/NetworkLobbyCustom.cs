using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkLobbyCustom : NetworkLobbyManager
{
	public GameObject canvas;

	// 4.
	public override void OnServerAddPlayer( NetworkConnection conn, short playerControllerId ) {
		base.OnServerAddPlayer(conn, playerControllerId);

		Debug.Log("OnServerAddPlayer " + SceneManager.GetActiveScene().name);

		string name = SceneManager.GetActiveScene().name;

		if ( name != this.lobbyScene ) {
			var player = Instantiate(gamePlayerPrefab, new Vector3(0, 0, 7), Quaternion.identity);
			NetworkServer.AddPlayerForConnection(conn, player.gameObject, playerControllerId);

			return;
		}
	}

	// 1. 호스트로 방 만들 때
	public override void OnLobbyClientEnter() {
		base.OnLobbyClientEnter();
		Debug.Log("OnLobbyClientEnter");
		canvas.SetActive(true);
	}

	public override void OnLobbyClientExit() {
		base.OnLobbyClientExit();
		Debug.Log("OnLobbyClientExit");
		canvas.SetActive(false);
	}
}
