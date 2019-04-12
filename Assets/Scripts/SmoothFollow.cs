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

		private void Start() {

		}

		private void LateUpdate() {
			if (!target)
				return;

			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;

			var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;

			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			transform.position = target.position;

			//Not camera rotate.

			//transform.position -= currentRotation * Vector3.forward * distance;
			transform.position -= Vector3.forward * distance;

			transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
			transform.LookAt(target);
		}
	}
}
