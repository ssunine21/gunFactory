using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTrigger : MonoBehaviour
{
	public Canvas help_F = null;
	public Canvas puzzle = null;
	
	private bool _active;
	public bool Active {
		get { return _active; }
	}

	private void Start() {
		if ( help_F )
			help_F.gameObject.SetActive(false);
	}

	private void OnTriggerStay( Collider other ) {
		if ( other.gameObject.CompareTag("Player") && Quest.init.QuestIdx >= 1) {
			help_F.gameObject.SetActive(true);

			if ( Input.GetKeyDown(KeyCode.F) ) {
				puzzle.gameObject.SetActive(true);
				other.GetComponent<FireCtrl>().IsStop = true;
			}
		}
	}

	private void OnTriggerExit( Collider other ) {
		help_F.gameObject.SetActive(false);
	}

	public void TriggerPlay() {
		_active = true;
		gameObject.GetComponentInParent<IsActive>().Active();
		//gameObject.SetActive(false);
	}
}
