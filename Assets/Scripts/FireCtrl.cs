using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCtrl : MonoBehaviour
{
	public enum WeaponType {
		GUN = 0,
		SHOTGUN,
		FIREGUN
	}
	private WeaponType weaponType;

	public float[] fireSpeed;
	private float currTime;

	public GameObject bullet;
	private List<GameObject> bulletList = new List<GameObject>();

	public ParticleSystem cartridge = null;
	public Transform firePosTr;

	public int maxBullet = 50;
	private int currBullet;
	public Text currbullet_Text;

	public float reloadTime = 2f;
	private bool isReloading = false;

	//private float destoryTime = 2f;
	private ParticleSystem muzzleFlash;

	private void Awake() {
		CreatePooling();
	}

	void Start() {
		currTime = Time.time;
		muzzleFlash = firePosTr.GetComponentInChildren<ParticleSystem>();
		currBullet = maxBullet;
		UpdateBulletText();
	}
	

    void Update() {
		

        if (!isReloading && Input.GetMouseButton(0)) {
			Fire();

			if(currBullet == 0 ) {
				StartCoroutine(Reloading());
			}
		}
    }

	private void Fire() {
		if ( currTime + fireSpeed[PlayerCtrl.init.gunChangeIdx] > Time.time ) return;

		currTime = Time.time;

		//Instantiate(bullet, firePosTr.position, firePosTr.rotation);
		var _bullet = GetBullet();
		if ( _bullet != null) {
			_bullet.transform.position = firePosTr.position;
			_bullet.transform.rotation = firePosTr.rotation;
			_bullet.SetActive(true);
		}

		currBullet--;

		if ( cartridge ) cartridge.Play();
		if ( muzzleFlash ) muzzleFlash.Play();
		UpdateBulletText();
	}

	IEnumerator Reloading() {
		isReloading = true;

		yield return new WaitForSeconds(reloadTime);

		foreach(var list in bulletList ) {
			if ( list.activeSelf ) list.SetActive(false);
		}

		isReloading = false;
		currBullet = maxBullet;
		UpdateBulletText();
	}

	private void UpdateBulletText() {
		currbullet_Text.text = currBullet.ToString("000");
	}

	private void CreatePooling() {
		GameObject bulletPool = new GameObject("BulletPool");

		for(int i = 0; i < maxBullet; ++i ) {
			var obj = Instantiate<GameObject>(bullet, bulletPool.transform);
			obj.SetActive(false);

			bulletList.Add(obj);
		}
	}

	private GameObject GetBullet() {
		foreach(var list in bulletList ) {
			if ( list.activeSelf == false) return list;
		}

		return null;
	}
}
