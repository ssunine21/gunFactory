﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActive : MonoBehaviour
{
	private Animator doorAnim;
	private IsTrigger isTrigger;

	private void Awake() {
		doorAnim = GetComponent<Animator>();
		isTrigger = this.transform.Find("Trigger").GetComponent<IsTrigger>();
	}

	public void Active() {
		if ( isTrigger.Active ) {
			doorAnim.SetBool("isOpen", true);
		}
	}
}
