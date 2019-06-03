using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class JobSpawn : MonoBehaviourPun {

	public GameObject jobPrefab;
	public Sprite[] jobImgs;
	public Transform jobSpawnTr;

	private void Start() {

		OnSpawnJob();
	}

	private void Update() {
		if ( Input.GetKeyDown(KeyCode.Space) ) {
			OnSpawnJob();
		}
	}

	private void OnSpawnJob() {
		if ( !PhotonNetwork.IsMasterClient ) return;

		//for ( int i = 0; i < jobImgs.Length; ++i ) {
		Debug.Log("Spawn");
		GameObject jobObj = PhotonNetwork.Instantiate(jobPrefab.name, jobSpawnTr.position, Quaternion.identity);

		jobObj.GetComponent<Image>().sprite = jobImgs[0];
		//jobObj.transform.SetParent(jobSpawnTr);
		//}
	}
}
