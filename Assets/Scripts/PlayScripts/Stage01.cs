using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage01 : MonoBehaviour
{
	public GameObject door;
	public GameObject lightParent;

	private AudioSource _audio;
	private Light[] lights;

	private float delayTime = 5.0f;
	private float rotSpeed = 500f;

	private bool onlyOnce = false;

	private void Start() {
		lights = lightParent.GetComponentsInChildren<Light>();
		_audio = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter( Collider other ) {
		if ( other.CompareTag("Player") && !onlyOnce) {
			door.GetComponent<Animator>().SetBool("isOpen", false);

			StartCoroutine("RotateLight");
			onlyOnce = true;
		}
	}

	private IEnumerator RotateLight() {
		delayTime += Time.time;
		_audio.PlayOneShot(_audio.clip);
		while ( delayTime > Time.time ) {
			foreach(var light in lights ) {
				light.transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
			}

			yield return null;
		}

		lightParent.gameObject.SetActive(false);
	}

	
}
