using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStanderdAssets.Utility {

	public class SmoothFollow : MonoBehaviour {

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

		private Transform cameraTr;

		//벽 감지를 위한 레이
		Ray ray;
		RaycastHit hit;


		private void Awake() {
			cameraTr = GetComponent<Transform>();
		}


		private void LateUpdate() {
			ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(target.position));
			Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);
			//if ( Physics.Raycast(ray, out hit, 200f, 1 << 9) ) hit.transform.
			//mousePos = hit.point;


			if (!target)
				return;

			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;

			var currentRotationAngle = cameraTr.eulerAngles.y;
			var currentHeight = cameraTr.position.y;

			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			cameraTr.position = target.position;

			//Not camera rotate.

			//cameraTr.position -= currentRotation * Vector3.forward * distance;
			cameraTr.position -= Vector3.forward * distance;

			cameraTr.position = new Vector3(cameraTr.position.x, currentHeight, cameraTr.position.z);
			cameraTr.LookAt(target);
			
		}
	}
}
