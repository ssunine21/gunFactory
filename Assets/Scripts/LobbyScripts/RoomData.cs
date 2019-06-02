using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
	public string roomName = "";
	public int currPlayer = 0;
	public int maxPlayer = 0;

	private void Awake() {
		UpdateInfo();
	}

	public void UpdateInfo() {
		transform.GetChild(0).GetComponent<Text>().text = roomName;
		transform.GetChild(1).GetComponent<Text>().text = string.Format("{0} / {1}", currPlayer, maxPlayer);
	}
}
