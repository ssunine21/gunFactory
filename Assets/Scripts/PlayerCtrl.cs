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

public class PlayerCtrl : MonoBehaviour
{
	public float moveSpeed = 5.0f;
	public float rotSpeed = 5.0f;

	public Camera pointCamera = null;
	public PlayerAnim playerAnim;
	public Animation anim;

	private float h = 0.0f;
	private float v = 0.0f;

	private Vector3 moveDir;
	private Transform playerTr;

	private Vector3 mousePos;


	private void Awake() {
		playerTr = GetComponent<Transform>();
		anim = GetComponent<Animation>();
	}

	private void Start() {
		anim.clip = playerAnim.idle;
		anim.Play();
    }
	
    private void Update() {
		PlayerMove();
		PlayerAnimation();
		MouseMove();
    }

	private void PlayerMove() {
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		moveDir = (Vector3.forward * v) + (Vector3.right * h);
		playerTr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.World);
	}


	private void PlayerAnimation() {
		if (v >= 0.1f) anim.CrossFade(playerAnim.runF.name, 0.3f);
		else if (v <= -0.1f) anim.CrossFade(playerAnim.runB.name, 0.3f);
		else if (h >= 0.1f) anim.CrossFade(playerAnim.runR.name, 0.3f);
		else if (h <= -0.1f) anim.CrossFade(playerAnim.runL.name, 0.3f);
		else anim.CrossFade(playerAnim.idle.name, 0.3f);
	}


	private void MouseMove() {
		mousePos = pointCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointCamera.transform.position.y));

		float dx = mousePos.x - playerTr.position.x;
		float dz = mousePos.z - playerTr.position.z;
		float rotDgree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

		playerTr.rotation = Quaternion.Lerp(playerTr.rotation, Quaternion.Euler(0f, rotDgree, 0f), rotSpeed * Time.deltaTime);

		//Debug.Log(mousePos.x + ", " + mousePos.y + ", " + mousePos.z);
	}


	private void OnDrawGizmos() {
		if (!pointCamera) return;

		mousePos = pointCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointCamera.transform.position.y));

		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(mousePos, 1.0f);

	}
}
