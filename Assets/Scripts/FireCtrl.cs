using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
	public GameObject bullet;
	public ParticleSystem cartridge = null;
	public Transform firePosTr;

	private float destoryTime = 2f;
	private ParticleSystem muzzleFlash;

    void Start() {
		muzzleFlash = firePosTr.GetComponentInChildren<ParticleSystem>();
    }
	

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
			Fire();
		}
    }

	private void Fire() {
		Instantiate(bullet, firePosTr.position, firePosTr.rotation);

		if ( cartridge ) cartridge.Play();
		if ( muzzleFlash ) muzzleFlash.Play();

		//Destroy(bullet, destoryTime);
	}
}
