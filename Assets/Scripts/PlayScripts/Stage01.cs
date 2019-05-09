using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage01 : MonoBehaviour
{
	public GameObject door;
	
	private void OnTriggerEnter( Collider other ) {
		if ( other.CompareTag("Player") ) {
			door.GetComponent<Animator>().SetBool("isOpen", false);
		}
	}
}
