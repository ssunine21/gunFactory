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
	private int max_gunIdx = 2;

	public GameObject[] WeaponList;

	delegate void Action();

	public float moveSpeed = 5.0f;
	public float rotSpeed = 5.0f;
	
	public PlayerAnim playerAnim;
	public Animator gunChangeAnim;
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
		init = this;
		playerTr = GetComponent<Transform>();
		anim = GetComponent<Animation>();
		playerRigid = GetComponent<Rigidbody>();
	}

	private void Start() {
		keyDic = new Dictionary<KeyCode, Action> {
			{KeyCode.E, Key_E },
			{KeyCode.Q, Key_Q },
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
		Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
		if ( Physics.Raycast(ray, out hit, 200f) ) mousePos = hit.point;
		mousePos = hit.point;
		mousePos.y = 0;

		Quaternion rot = Quaternion.LookRotation(mousePos - playerTr.position);
		playerTr.rotation = Quaternion.Slerp(playerTr.rotation, rot, rotSpeed * Time.deltaTime);

		//mousePos = pointCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointCamera.transform.position.y));

		//float dx = mousePos.x - playerTr.position.x;
		//float dz = mousePos.z - playerTr.position.z;
		//float rotDgree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

		//playerTr.rotation = Quaternion.Lerp(playerTr.rotation, Quaternion.Euler(0f, rotDgree, 0f), rotSpeed * Time.deltaTime);

		//Debug.Log(mousePos.x + ", " + mousePos.y + ", " + mousePos.z);
	}


	private void OnDrawGizmos() {
		//mousePos = pointCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointCamera.transform.position.y));

		//Gizmos.color = Color.yellow;
		//Gizmos.DrawSphere(mousePos, 1.0f);

	}
	
	private void Key_E() {
		if ( gunChangeIdx == max_gunIdx ) return;

		ChangeGun(++gunChangeIdx);
		Debug.Log("E" + gunChangeIdx);
	}
	private void Key_Q() {
		if ( gunChangeIdx == 0 ) return;

		ChangeGun(--gunChangeIdx);
		Debug.Log("Q" + gunChangeIdx);
	}
	private void Key_R() {
		StartCoroutine(FireCtrl.init.Reloading());
	}

	private void ChangeGun(int changeIdx) {
		gunChangeAnim.SetInteger("changeNum", changeIdx);
		gunChangeAnim.SetTrigger("changeTrigger");

		FireCtrl.init.weaponType = (FireCtrl.WeaponType)changeIdx;
		StartCoroutine(FireCtrl.init.ChangeBullet());
	}


	public static PlayerCtrl init;
}
