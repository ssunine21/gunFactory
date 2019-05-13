using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PlayerSfx {
	public AudioClip[] fire;
	public AudioClip[] reload;
}


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

	//[SerializeField]
	public GameObject[] bullets;


	private GameObject gunManager;
	private List<GameObject> bulletList = new List<GameObject>();
	private Gun currGun;
	private ParticleSystem flameBullet;
	private AudioSource _audio;

	public ParticleSystem cartridge = null;
	public Transform firePosTr;
	public Text currbullet_Text;
	public PlayerSfx playerSfx;

	public float reloadTime = 2f;
	public float changingBulletTime = 0.5f;
	public bool isFlameBullet = false;
	public bool isShotFlame = false;

	private bool isStop = false;
	public bool IsStop {
		set { isStop = value; }
	}

	//private float destoryTime = 2f;
	private ParticleSystem muzzleFlash;

	public float decrease_moveSpeed = 2f;
	private float origin_moveSpeed;
	

	private void Awake() {
		if (init)
			Destroy(gameObject);

		init = this;

	}

	void Start() {
		CreatePooling();
		currGun.CurrBullet = currGun._maxBullet;

		currTime = Time.time;
		muzzleFlash = firePosTr.GetComponentInChildren<ParticleSystem>();
		flameBullet = bullets[(int)WeaponType.FIREGUN].GetComponent<ParticleSystem>();
		origin_moveSpeed = PlayerCtrl.init.moveSpeed;

		_audio = GetComponent<AudioSource>();

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
				_audio.Pause();
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

			FireSfx();
		}

		currGun.CurrBullet--;
		UpdateBulletText();
	}

	private void FireSfx() {
		var _sfx = playerSfx.fire[(int)weaponType];

		if ( isFlameBullet ) {
			_audio.clip = _sfx;
			_audio.Play();
		}
		else {
			_audio.PlayOneShot(_sfx, 1f);
		}
	}

	public void ReloadSfx() {
		var _sfx = playerSfx.reload[(int)weaponType];
		_audio.PlayOneShot(_sfx, 1f);
	}


	public IEnumerator Reloading() {
		isStop = true;
		if ( flameBullet ) {
			flameBullet.Stop();
			isShotFlame = false;
		}

		ReloadSfx();

		yield return new WaitForSeconds(reloadTime);

		if ( !isFlameBullet ) {
			foreach ( var list in bulletList ) {
				if ( list.activeSelf ) list.SetActive(false);
			}
		}

		isStop = false;
		isShotFlame = true;

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
			//Debug.Log("ERROR! : bulletPool is empty");
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
