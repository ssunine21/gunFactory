using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviourPunCallbacks {

	public GameObject jobPrefab;
	public Sprite[] jobImgs;
	public Transform jobSpawnTr;

    private void Start(){
		PhotonNetwork.IsMessageQueueRunning = true;

		OnSpawnJob();
    }
	
	private void OnSpawnJob() {
		for ( int i = 0; i < jobImgs.Length; ++i ) {
			Debug.Log("Spawn");
			var jobObj = PhotonNetwork.Instantiate(jobPrefab.name, jobSpawnTr.position, Quaternion.identity);

			jobObj.GetComponent<Image>().sprite = jobImgs[i];
			jobObj.transform.SetParent(jobSpawnTr);
		}
	}

	private void Update() {
		if ( Input.GetKeyDown(KeyCode.Escape) ) {
			SceneManager.LoadScene("MainScene");
		}
	}
}
