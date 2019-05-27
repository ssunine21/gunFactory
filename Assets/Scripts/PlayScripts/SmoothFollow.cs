using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SmoothFollow : NetworkBehaviour {

	[SerializeField]
	private Transform target = null;
	[SerializeField]
	private float distance = 10.0f;
	[SerializeField]
	private float height = 5.0f;

	[SerializeField]
	private float rotationDamping = 3.0f;
	[SerializeField]
	private float heightDamping = 3.0f;

	public float mouseDir = 0f;
	private Vector3 mousePos;

	private Transform cameraTr;

	//벽 감지를 위한 레이
	Ray ray;
	RaycastHit hit;


	private void Awake() {
		init = this;
		cameraTr = GetComponent<Transform>();
	}


	public void FindTarget(Transform target) {
		this.target = target;
	}

	private void LateUpdate() {

		if ( !target ) {
			return;
		}

		ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(target.position));
		Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
		//if ( Physics.Raycast(ray, out hit, 200f, 1 << 9) ) hit.transform.
		//mousePos = hit.point;


		var wantedRotationAngle = target.eulerAngles.y;
		var wantedHeight = target.position.y + height;

		var currentRotationAngle = cameraTr.eulerAngles.y;
		var currentHeight = cameraTr.position.y;

		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		//mousePos에 따른 카메라 추가 움직임
		mousePos = ReturnMouse(Input.mousePosition);
		mousePos.z = mousePos.y;


		cameraTr.position = target.position + (mousePos * mouseDir);
		cameraTr.position -= Vector3.forward * distance;

		cameraTr.position = new Vector3(cameraTr.position.x, currentHeight, cameraTr.position.z);
		//cameraTr.LookAt(target);

	}

	Vector3 ReturnMouse( Vector3 mousePos ) {
		Vector3 temp = (Camera.main.ScreenToViewportPoint(mousePos) - new Vector3(0.5f, 0.5f));

		if ( temp.x < -0.5f ) temp.x = -0.5f;
		if ( temp.x > 0.5f ) temp.x = 0.5f;
		if ( temp.y < -0.5f ) temp.y = -0.5f;
		if ( temp.y > 0.5f ) temp.y = 0.5f;

		return temp;
	}

	static public SmoothFollow init;
}
