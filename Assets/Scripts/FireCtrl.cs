using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCtrl : MonoBehaviour
{
	public GameObject bullet;
	public ParticleSystem cartridge = null;
	public Transform firePosTr;

	public int maxBullet = 10;
	private int currBullet;
	public Text currbullet_Text;

	public float reloadTime = 2f;
	private bool isReloading = false;

	//private float destoryTime = 2f;
	private ParticleSystem muzzleFlash;

    void Start() {
		muzzleFlash = firePosTr.GetComponentInChildren<ParticleSystem>();
		currBullet = maxBullet;
		UpdateBulletText();
	}
	

    void Update() {
        if (!isReloading && Input.GetMouseButtonDown(0)) {
			Fire();

			if(currBullet == 0 ) {
				StartCoroutine(Reloading());
			}
		}
    }

	private void Fire() {
		Instantiate(bullet, firePosTr.position, firePosTr.rotation);

		currBullet--;

		if ( cartridge ) cartridge.Play();
		if ( muzzleFlash ) muzzleFlash.Play();
		UpdateBulletText();
	}

	IEnumerator Reloading() {
		isReloading = true;

		yield return new WaitForSeconds(reloadTime);
		isReloading = false;
		currBullet = maxBullet;
		UpdateBulletText();
	}

	private void UpdateBulletText() {
		currbullet_Text.text = currBullet.ToString("000");
	}
}
