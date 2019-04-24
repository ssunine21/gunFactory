using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
	private Transform tr;

	public Image gage = null;


	private void Start() {
		tr = GetComponent<Transform>();
		gage.fillAmount = 0f;
	}
	private void FixedUpdate() {

		if ( Input.GetMouseButton(0) ) {
			tr.Translate(Vector3.right * 2f);
		}
		else {

			tr.Translate(Vector3.left);
		}
			
	}

	private void OnTriggerStay2D( Collider2D collision ) {
		StartCoroutine("Filled");
	}

	private IEnumerator Filled() {
		gage.fillAmount += 0.1f;
		yield return null;
	}
}
