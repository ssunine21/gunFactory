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

	public Camera pointCamera = null;
	public PlayerAnim playerAnim;
	public Animator gunChangeAnim;
	public Animation anim;
	private int gunChangeIdx = 0;

	private float h_Value = 0.0f;
	private float v_Value = 0.0f;

	private Vector3 movement;
	private Transform playerTr;
	private Rigidbody playerRigid;


	private Dictionary<KeyCode, Action> keyDic;

	private Ray ray;
	private RaycastHit hit;
	private Vector3 mousePos;


	private void Awake() {
		playerTr = GetComponent<Transform>();
		anim = GetComponent<Animation>();
		playerRigid = GetComponent<Rigidbody>();
	}

	private void Start() {
		keyDic = new Dictionary<KeyCode, Action> {
			{KeyCode.E, Key_E },
			{KeyCode.Q, Key_Q }
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
		//if ( Physics.Raycast(ray, out hit, 200f, 1 << 8) ) mousePos = hit.point;

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
		if ( !pointCamera ) return;

		mousePos = pointCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointCamera.transform.position.y));

		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(mousePos, 1.0f);

	}
	
	private void Key_E() {
		gunChangeAnim.SetInteger("changeNum", ++gunChangeIdx);
		gunChangeAnim.SetTrigger("changeTrigger");
	}
	private void Key_Q() {
		gunChangeAnim.SetInteger("changeNum", --gunChangeIdx);
		gunChangeAnim.SetTrigger("changeTrigger");
	}

}
