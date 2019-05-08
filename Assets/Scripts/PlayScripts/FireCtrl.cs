using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FireCtrl : MonoBehaviour
{
	
	[SerializeField]
	private WeaponType _weaponType;
	public WeaponType weaponType {
		get { return _weaponType; }
		set { _weaponType = value; }
	}

	public float[] fireSpeed;

	private float currTime;

	public GameObject[] bullets;
	private Gun currGun;
	private ParticleSystem flameBullet;
	public bool isFlameBullet = false;
	public bool isShotFlame = false;

	private GameObject gunManager;
	private List<GameObject> bulletList = new List<GameObject>();

	public ParticleSystem cartridge = null;
	public Transform firePosTr;
	
	public Text currbullet_Text;

	public float reloadTime = 2f;
	public float changingBulletTime = 0.5f;
	private bool isStop = false;

	//private float destoryTime = 2f;
	private ParticleSystem muzzleFlash;

	public float decrease_moveSpeed = 2f;
	private float origin_moveSpeed;
	

	private void Awake() {
		if (init)
			Destroy(gameObject);

		init = this;

		CreatePooling();
	}

	void Start() {
		currGun = bullets[PlayerCtrl.init.gunChangeIdx].GetComponent<Gun>();

		currTime = Time.time;
		muzzleFlash = firePosTr.GetComponentInChildren<ParticleSystem>();
		flameBullet = bullets[(int)WeaponType.FIREGUN].GetComponent<ParticleSystem>();

		origin_moveSpeed = PlayerCtrl.init.moveSpeed;
		UpdateBulletText();

	}
	

    void Update() {


		if ( !isStop && Input.GetMouseButton(0) ) {
			Fire();

			if ( currGun.CurrBullet <= 0 ) {
				StartCoroutine(Reloading());
			}
		}
		else if ( Input.GetMouseButtonUp(0) ) {
			PlayerCtrl.init.moveSpeed = origin_moveSpeed;
			if ( isFlameBullet ) {
				isShotFlame = false;
				flameBullet.Stop();
			}
		}
    }


	private void Fire() {
		if ( currTime + currGun._reloadSpeed > Time.time ) return;

		PlayerCtrl.init.moveSpeed = origin_moveSpeed - decrease_moveSpeed;
		currTime = Time.time;

		if ( isFlameBullet ) {
			isShotFlame = true;
			flameBullet.Play();
		}
		else {
			var _bullet = GetBullet();
			if ( _bullet != null ) {
				_bullet.transform.position = firePosTr.position;
				_bullet.transform.rotation = firePosTr.rotation;
				_bullet.SetActive(true);

			}
			if ( cartridge ) cartridge.Play();
			if ( muzzleFlash ) muzzleFlash.Play();

		}

		currGun.CurrBullet--;
		UpdateBulletText();
	}


	public IEnumerator Reloading() {
		isStop = true;
		if (flameBullet)
			flameBullet.Stop();

		yield return new WaitForSeconds(reloadTime);

		if ( !isFlameBullet ) {
			foreach ( var list in bulletList ) {
				if ( list.activeSelf ) list.SetActive(false);
			}
		}

		isStop = false;

		currGun.CurrBullet = currGun._maxBullet;
		UpdateBulletText();
	}

	public IEnumerator ChangeBullet() {
		isStop = true;
		yield return new WaitForSeconds(changingBulletTime);
		DestroyPooling();
		CreatePooling();
		UpdateBulletText();

		isStop = false;
	}


	private void UpdateBulletText() {
		currbullet_Text.text = currGun.CurrBullet.ToString("000");
	}


	private void CreatePooling() {
		if(gunManager == null) {
			gunManager = new GameObject("GunManager");
			Debug.Log("ERROR! : bulletPool is empty");
		}

		currGun = bullets[PlayerCtrl.init.gunChangeIdx].GetComponent<Gun>();

		if ( currGun.weaponType == WeaponType.FIREGUN ) {
			isFlameBullet = true;
			var obj = Instantiate<GameObject>(bullets[(int)currGun.weaponType], firePosTr);
			flameBullet = obj.GetComponent<ParticleSystem>();
			flameBullet.Stop();
			bulletList.Add(obj);
		}
		else {
			isFlameBullet = false;
			for ( int i = 0; i < currGun._maxBullet; ++i ) {
				var obj = Instantiate<GameObject>(bullets[(int)currGun.weaponType], gunManager.transform);
				obj.SetActive(false);

				bulletList.Add(obj);
			}
		}
	}

	private void DestroyPooling() {
		if ( isFlameBullet ) {
			GameObject.Destroy(flameBullet.gameObject);
		}
		else {
			foreach ( Transform objs in gunManager.transform )
				GameObject.Destroy(objs.gameObject);
		}
		bulletList.Clear();
	}


	private GameObject GetBullet() {
		foreach(var list in bulletList ) {
			if ( list.activeSelf == false) return list;
		}

		return null;
	}
	
	public static FireCtrl init = null;

}
