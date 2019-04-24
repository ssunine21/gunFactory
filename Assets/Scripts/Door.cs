using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	private Animator anim;
	public Canvas puzzleButton = null;
	
	private readonly int hash_openDoor = Animator.StringToHash("character_nearby");

	private void Awake() {
		anim = GetComponent<Animator>();
	}

	private void OnCollisionEnter( Collision collision ) {
		if ( collision.gameObject.tag == "Player" ) {
			puzzleButton.gameObject.SetActive(true);
		}
	}
}
