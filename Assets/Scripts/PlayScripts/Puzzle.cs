using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
	private RectTransform tr;

	public Canvas panel = null;

	public Image gage = null;
	public Image point = null;
	public float MIN = 0f;
	public float MAX = 1400f;


	private void Start() {
		tr = GetComponent<RectTransform>();
		gage.fillAmount = 0f;
	}


	private void FixedUpdate() {

		if ( Input.GetMouseButton(0) ) {
			if ( tr.position.x > MAX ) return;
			//Debug.Log(tr.position.x);
				tr.Translate(Vector3.right * 5f);
		}
		else {
			if ( tr.position.x < MIN ) return;
			//Debug.Log(tr.position.x);
			tr.Translate(Vector3.left);
		}
			
	}

	private void OnTriggerStay2D( Collider2D collision ) {
		StartCoroutine("Filled");
	}

	private IEnumerator Filled() {
		gage.fillAmount += 0.01f;
		if ( gage.fillAmount == 1 )
			StartCoroutine("Stop");
		yield return null;
	}

	private IEnumerator Stop() {
		gage.color = Color.blue;
		yield return new WaitForSeconds(1.5f);

		panel.gameObject.SetActive(false);
	}
}
