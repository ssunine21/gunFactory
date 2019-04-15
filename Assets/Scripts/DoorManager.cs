using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
	Animator doorAnim;

	private readonly int hash_characterNearby = Animator.StringToHash("character_nearby");

	private void Awake() {
		doorAnim = GetComponent<Animator>();
	}

	public void DoorOpen() {
		doorAnim.SetBool(hash_characterNearby, true);
	}
}
