using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[System.Serializable]
public class PlayerAnim {
	public AnimationClip idle;
	public AnimationClip runF;
	public AnimationClip runB;
	public AnimationClip runL;
	public AnimationClip runR;
}

public enum JOB {
	CAP = 0,
	ENGINNER,
}

public class PlayerCtrl : MonoBehaviourPun {

	delegate void Action();

	public Texture2D cursorIdle;
	public Texture2D cursorOnMonster;
	public Texture2D cursorOnWall;
	public CursorMode cursorMode = CursorMode.Auto;

	public float dashSpeed = 30000f;
	public float moveSpeed = 5.0f;
	public float rotSpeed = 5.0f;
	public Vector3 mouseCorrection = Vector3.zero;
	
	public PlayerAnim playerAnim;
	public Animation anim;

	[HideInInspector]
	public int gunChangeIdx = 0;

	private float h_Value = 0.0f;
	private float v_Value = 0.0f;

	private FireCtrl fireCtrl;
	private Vector3 movement;
	private Transform playerTr;
	private Rigidbody playerRigid;
	private float dashCoolTime = 3f;
	private float currTime = 0;
	public JOB job;

	private Dictionary<KeyCode, Action> keyDic;

	private Ray ray;
	private RaycastHit hit;

	[HideInInspector]
	public Vector3 mousePos = Vector3.zero;


	private void Awake() {
		fireCtrl = GetComponent<FireCtrl>();
		playerTr = GetComponent<Transform>();
		anim = GetComponent<Animation>();
		playerRigid = GetComponent<Rigidbody>();
	}

	private void Start() {
		if ( photonView.IsMine )
			SmoothFollow.init.FindTarget(this.transform);

		keyDic = new Dictionary<KeyCode, Action> {
			{KeyCode.Alpha1, Key_1 },
			{KeyCode.Alpha2, Key_2 },
			{KeyCode.Alpha3, Key_3 },
			{KeyCode.R, Key_R },
			{KeyCode.Space, Key_Space }
		};

		switch ( MainManager.init.jobidx ) {
			case 0:
				job = JOB.CAP;
				Debug.Log(job);
				break;
			case 1:
				job = JOB.ENGINNER;
				Debug.Log(job);
				break;
		}
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
		if ( !photonView.IsMine ) return;


		PlayerMove();
		MouseMove();
	}


	private void PlayerMove() {
		h_Value = Input.GetAxisRaw("Horizontal");
		v_Value = Input.GetAxisRaw("Vertical");

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
		ray = Camera.main.ScreenPointToRay((Input.mousePosition - mouseCorrection));
		Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
		if ( Physics.Raycast(ray, out hit, 300f, 1 << 8) ) {
			//mousePos = hit.point;
			Cursor.SetCursor(cursorIdle, mousePos, cursorMode);
		}
		//else if ( Physics.Raycast(ray, out hit, 300f, 1 << 9)) {
		//	mousePos = hit.point;
		//	Cursor.SetCursor(cursorOnWall, mousePos, cursorMode);
		//}
		mousePos = hit.point - playerTr.position;
		mousePos.y = playerTr.position.y;

		Quaternion rot = Quaternion.LookRotation(mousePos);
		playerTr.rotation = Quaternion.Slerp(playerTr.rotation, rot, rotSpeed * Time.deltaTime);
		
	}

	
	private void Key_1() {
		if ( gunChangeIdx == 0 ) return;

		gunChangeIdx = 0;
		ChangeGun(gunChangeIdx);
		GameManager.init.ChangeGunBoxSprite(gunChangeIdx);
		fireCtrl.ReloadSfx();
		Debug.Log("1");
	}
	private void Key_2() {
		if ( gunChangeIdx == 1 ) return;

		gunChangeIdx = 1;
		ChangeGun(gunChangeIdx);
		GameManager.init.ChangeGunBoxSprite(gunChangeIdx);
		fireCtrl.ReloadSfx();
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
		StartCoroutine(fireCtrl.Reloading());
		fireCtrl.ReloadSfx();
	}
	private void Key_Space() {
		if ( currTime + dashCoolTime < Time.time ) {
			playerRigid.AddForce(movement * dashSpeed);
			currTime = Time.time;
		}

	}

	private void ChangeGun(int changeIdx) {

		fireCtrl.weaponType = (WeaponType)changeIdx;
		StartCoroutine(fireCtrl.ChangeBullet());
	}


	//public static PlayerCtrl init;
}
