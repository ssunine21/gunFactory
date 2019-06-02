using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneChange : MonoBehaviour {
	public void ChangeToPlayScenewithSolo() {
		GameManager.init.isSoloPlay = true;
		SceneManager.LoadScene("PlayScene");
	}


	public void ChangeToPlayScenewithMulti() {

		if ( PhotonNetwork.IsMasterClient )
			PhotonNetwork.LoadLevel("PlayScene");
	}

	public void ChangeToMainScene() {
		SceneManager.LoadScene("MainScene");
	}

	public void ChangeToLobbyScene() {
		SceneManager.LoadScene("LobbyScene");
	}


}
