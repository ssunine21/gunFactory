using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkLobbyCustom : NetworkLobbyManager
{
	public GameObject canvas;

	public override void OnServerAddPlayer( NetworkConnection conn, short playerControllerId ) {
		base.OnServerAddPlayer(conn, playerControllerId);

		Debug.Log(SceneManager.GetActiveScene().name);

		string name = SceneManager.GetActiveScene().name;

		if ( name != this.lobbyScene ) {
			var player = Instantiate(gamePlayerPrefab, new Vector3(0, 0, 7), Quaternion.identity);
			NetworkServer.AddPlayerForConnection(conn, player.gameObject, playerControllerId);

			return;
		}
	}

	
	#region Lobby Enter and Exit
	public override void OnLobbyClientEnter() {
		base.OnLobbyClientEnter();
		canvas.SetActive(true);
	}

	public override void OnLobbyClientExit() {
		base.OnLobbyClientExit();
		canvas.SetActive(false);
	}
	#endregion
}
