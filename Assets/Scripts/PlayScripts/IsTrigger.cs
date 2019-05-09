using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTrigger : MonoBehaviour
{
	public Canvas viewCanvas = null;
	
	private bool _active;
	public bool Active {
		get { return _active; }
	}

	private void Start() {
		if ( viewCanvas )
			viewCanvas.gameObject.SetActive(false);
	}

	private void OnTriggerStay( Collider other ) {
		if ( other.gameObject.CompareTag("Player") ) {
			viewCanvas.gameObject.SetActive(true);

			if ( Input.GetKeyDown(KeyCode.F) ) {
				_active = true;
				gameObject.GetComponentInParent<IsActive>().Active();
				gameObject.SetActive(false);
			}
		}
	}

	private void OnTriggerExit( Collider other ) {
		viewCanvas.gameObject.SetActive(false);
	}
}
