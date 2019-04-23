using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameRange : MonoBehaviour {
	public float viewAngle = 80f;
	public float viewDist = 10f;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	private Transform _transform;

	private void Awake() {
		_transform = GetComponent<Transform>();
	}

	private void Update() {
		DrawView();
		FindTargets();
	}

	private Vector3 DirFromAngle(float angleInDegrees ) {
		angleInDegrees += transform.eulerAngles.y;
		return new Vector3(Mathf.Sign(angleInDegrees * Mathf.Deg2Rad), 0
			, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	private void DrawView() {
		Vector3 leftBoundary = DirFromAngle(-viewAngle / 2);
		Vector3 rightBoundary = DirFromAngle(viewAngle / 2);

		Debug.DrawLine(_transform.position, _transform.position + leftBoundary * viewDist, Color.blue);
		Debug.DrawLine(_transform.position, _transform.position + rightBoundary * viewDist, Color.blue);
	}

	private void FindTargets() {
		Collider[] targets = Physics.OverlapSphere(_transform.position, viewDist, targetMask);

		for(int i = 0; i < targets.Length; ++i ) {
			Transform target = targets[i].transform;

			Vector3 dirToTarget = (target.position - _transform.position).normalized;

			if(Vector3.Dot(_transform.forward, dirToTarget) > Mathf.Cos((viewAngle / 2) * Mathf.Deg2Rad) ) {
				float distToTarget = Vector3.Distance(_transform.position, target.position);

				if ( !Physics.Raycast(_transform.position, dirToTarget, distToTarget, obstacleMask) ) {
					Debug.DrawLine(_transform.position, target.position, Color.red);
				}
			}
		}
	}
}
