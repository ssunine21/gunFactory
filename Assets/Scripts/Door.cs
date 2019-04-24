using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

	private Animator anim;
	private MeshCollider meshCollider;

	public Canvas puzzleButton = null;
	
	private readonly int hash_openDoor = Animator.StringToHash("character_nearby");

	private void Awake() {
		anim = GetComponent<Animator>();
		meshCollider = GetComponent<MeshCollider>();
	}

	private void OnCollisionEnter( Collision collision ) {
		if ( collision.gameObject.tag == "Player" && !anim.GetBool(hash_openDoor)) {
			puzzleButton.gameObject.SetActive(true);
		}
	}

	private void OnCollisionExit( Collision collision ) {
		if ( collision.gameObject.tag == "Player" ) {
			puzzleButton.gameObject.SetActive(false);
		}
	}

	public void SetAnim() {
		anim.SetBool(hash_openDoor, true);
		meshCollider.convex = false;
	}
}
