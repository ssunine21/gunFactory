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
	
	[SerializeField]
	private WeaponType _weaponType;
	public WeaponType weaponType {
		get { return _weaponType; }
		set { _weaponType = value; }
	}

	public float[] fireSpeed;

	private float currTime;

	public GameObject[] bullets;
	private ParticleSystem flameBullet;
	public bool isFlameBullet = false;

	public GameObject gunManager;
	private List<GameObject> bulletList = new List<GameObject>();

	public ParticleSystem cartridge = null;
	public Transform firePosTr;

	public int maxBullet = 50;
	private int currBullet;
	public Text currbullet_Text;

	public float reloadTime = 2f;
	public float changingBulletTime = 0.5f;
	private bool isStop = false;

	//private float destoryTime = 2f;
	private ParticleSystem muzzleFlash;

	public float decrease_moveSpeed = 2f;
	private float origin_moveSpeed;
	

	private void Awake() {
		if ( init == null )
			init = this;

		CreatePooling();
	}

	void Start() {
		currTime = Time.time;
		muzzleFlash = firePosTr.GetComponentInChildren<ParticleSystem>();
		currBullet = maxBullet;

		origin_moveSpeed = PlayerCtrl.init.moveSpeed;
		UpdateBulletText();
	}
	

    void Update() {


		if ( !isStop && Input.GetMouseButton(0) ) {
			Fire();

			if ( currBullet == 0 ) {
				StartCoroutine(Reloading());
			}
		}
		else if ( Input.GetMouseButtonUp(0) ) {
			PlayerCtrl.init.moveSpeed = origin_moveSpeed;
			if ( isFlameBullet ) {
				flameBullet.Stop();
			}
		}
    }


	private void Fire() {
		if ( currTime + fireSpeed[PlayerCtrl.init.gunChangeIdx] > Time.time ) return;

		PlayerCtrl.init.moveSpeed = origin_moveSpeed - decrease_moveSpeed;
		currTime = Time.time;

		if ( isFlameBullet ) {
			flameBullet.Play();
		}
		else {
			var _bullet = GetBullet();
			if ( _bullet != null ) {
				_bullet.transform.position = firePosTr.position;
				_bullet.transform.rotation = firePosTr.rotation;
				_bullet.SetActive(true);

			}

			currBullet--;

			if ( cartridge ) cartridge.Play();
			if ( muzzleFlash ) muzzleFlash.Play();
			UpdateBulletText();
		}
	}


	public IEnumerator Reloading() {
		isStop = true;

		yield return new WaitForSeconds(reloadTime);

		foreach(var list in bulletList ) {
			if ( list.activeSelf ) list.SetActive(false);
		}

		isStop = false;
		currBullet = maxBullet;
		UpdateBulletText();
	}

	public IEnumerator ChangeBullet() {
		isStop = true;

		yield return new WaitForSeconds(changingBulletTime);
		DestroyPooling();
		CreatePooling();

		isStop = false;
	}


	private void UpdateBulletText() {
		currbullet_Text.text = currBullet.ToString("000");
	}


	private void CreatePooling() {
		if(gunManager == null) {
			Debug.Log("ERROR! : bulletPool is empty");
			return;
		}

		if ( PlayerCtrl.init.gunChangeIdx == (int)WeaponType.FIREGUN )
			isFlameBullet = true;
		else
			isFlameBullet = false;

		if ( isFlameBullet ) {
			var obj = Instantiate<GameObject>(bullets[PlayerCtrl.init.gunChangeIdx], firePosTr);
			flameBullet = obj.GetComponent<ParticleSystem>();
			flameBullet.Stop();
			bulletList.Add(obj);
		}
		else {
			for ( int i = 0; i < maxBullet; ++i ) {
				var obj = Instantiate<GameObject>(bullets[PlayerCtrl.init.gunChangeIdx], gunManager.transform);
				obj.SetActive(false);

				bulletList.Add(obj);
			}
		}
	}

	private void DestroyPooling() {
		foreach ( Transform objs in gunManager.transform )
			GameObject.Destroy(objs.gameObject);

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
