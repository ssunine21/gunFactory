using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeJob : MonoBehaviour
{
	private Button[] jobList;
	private int idx = 0;

	private void Start() {
		jobList = GetComponentsInChildren<Button>();
	}

	public void OnChangeJob(int idx_temp) {
		if ( idx == idx_temp && !jobList[idx].interactable) return;

		jobList[idx].interactable = true;
		jobList[idx_temp].interactable = false;

		idx = idx_temp;

		MainManager.init.jobidx = idx;
	}
}
