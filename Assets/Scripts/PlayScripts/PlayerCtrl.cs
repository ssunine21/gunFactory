using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnim {
	public AnimationClip idle;
	public AnimationClip runF;
	public AnimationClip runB;
	public AnimationClip runL;
	public AnimationClip runR;
}

public class PlayerCtrl : MonoBehaviour {

	delegate void Action();

	public float moveSpeed = 5.0f;
	public float rotSpeed = 5.0f;
	
	public PlayerAnim playerAnim;
	public Animation anim;

	[HideInInspector]
	public int gunChangeIdx = 0;

	private float h_Value = 0.0f;
	private float v_Value = 0.0f;

	private Vector3 movement;
	private Transform playerTr;
	private Rigidbody playerRigid;


	private Dictionary<KeyCode, Action> keyDic;

	private Ray ray;
	private RaycastHit hit;

	[HideInInspector]
	public Vector3 mousePos = Vector3.zero;


	private void Awake() {
		if (init == null)
			init = this;

		playerTr = GetComponent<Transform>();
		anim = GetComponent<Animation>();
		playerRigid = GetComponent<Rigidbody>();
	}

	private void Start() {
		keyDic = new Dictionary<KeyCode, Action> {
			{KeyCode.Alpha1, Key_1 },
			{KeyCode.Alpha2, Key_2 },
			{KeyCode.Alpha3, Key_3 },
			{KeyCode.R, Key_R }
		};
	}

	private void Update() {
		PlayerAnimation();

		if ( Input.anyKeyDown ) {
			foreach(var dic in keyDic ) {
				if ( Input.GetKeyDown(dic.Key) )
					dic.Value();
			}
		}

	}

	private void FixedUpdate() {
		PlayerMove();
		MouseMove();
	}


	private void PlayerMove() {
		h_Value = Input.GetAxis("Horizontal");
		v_Value = Input.GetAxis("Vertical");

		movement = (Vector3.forward * v_Value) + (Vector3.right * h_Value);
		playerTr.Translate(movement.normalized * moveSpeed * Time.deltaTime, Space.World);

	}


	private void PlayerAnimation() {
		if ( v_Value >= 0.1f ) anim.CrossFade(playerAnim.runF.name, 0.3f);
		else if ( v_Value <= -0.1f ) anim.CrossFade(playerAnim.runB.name, 0.3f);
		else if ( h_Value >= 0.1f ) anim.CrossFade(playerAnim.runR.name, 0.3f);
		else if ( h_Value <= -0.1f ) anim.CrossFade(playerAnim.runL.name, 0.3f);
		else anim.CrossFade(playerAnim.idle.name, 0.3f);
	}


	private void MouseMove() {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 200f, Color.green);
		if ( Physics.Raycast(ray, out hit, 300f) ) mousePos = hit.point;

		mousePos.y = 0;

		Quaternion rot = Quaternion.LookRotation(mousePos - playerTr.position);
		playerTr.rotation = Quaternion.Slerp(playerTr.rotation, rot, rotSpeed * Time.deltaTime);
		
	}

	
	private void Key_1() {
		if ( gunChangeIdx == 0 ) return;

		gunChangeIdx = 0;
		ChangeGun(gunChangeIdx);
		GameManager.init.ChangeGunBoxSprite(gunChangeIdx);
		Debug.Log("1");
	}
	private void Key_2() {
		if ( gunChangeIdx == 1 ) return;

		gunChangeIdx = 1;
		ChangeGun(gunChangeIdx);
		GameManager.init.ChangeGunBoxSprite(gunChangeIdx);
		Debug.Log("2");
	}
	private void Key_3() {
		if ( gunChangeIdx == 2 ) return;

		gunChangeIdx = 2;
		ChangeGun(gunChangeIdx);
		GameManager.init.ChangeGunBoxSprite(gunChangeIdx);
		Debug.Log("3");
	}
	private void Key_R() {
		StartCoroutine(FireCtrl.init.Reloading());
	}

	private void ChangeGun(int changeIdx) {

		FireCtrl.init.weaponType = (FireCtrl.WeaponType)changeIdx;
		StartCoroutine(FireCtrl.init.ChangeBullet());
	}


	public static PlayerCtrl init;
}
