using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public float m_DampTime = 0.2f;                
	public float m_ScreenEdgeBuffer = 4f;          
	public float m_MinSize = 6.5f;
	public float m_MaxDistance = 5f;
	public Transform m_MainTarget;
	[HideInInspector] public Transform[] m_EnemyTargets; 


	private Camera m_Camera;
	private float m_ZoomSpeed;
	private Vector3 m_MoveVelocity;
	private Vector3 m_DesiredPosition;


	private void Awake() {
		m_Camera = GetComponentInChildren<Camera>();
	}


	private void FixedUpdate() {
		Move();
		Zoom();
	}


	private void Move() {
		FindAveragePosition();

		this.transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
	}


	private void FindAveragePosition() {
		Vector3 averagePos = new Vector3();
		Vector3 averageDistance = new Vector3();
		int numTargets = 0;
		
		foreach (var Enemy in m_EnemyTargets ) {
			if ( !Enemy.gameObject.activeSelf ) continue;

			averageDistance = m_MainTarget.position - Enemy.position;

			if ( averageDistance.sqrMagnitude > Mathf.Sqrt(m_MaxDistance) ) {
				averagePos += Enemy.position;
				numTargets++;
			}
		}
		

		if ( numTargets > 0 )
			averagePos /= numTargets;
		
		averagePos.y = this.transform.position.y;
		
		m_DesiredPosition = averagePos;
	}


	private void Zoom() {
		float requiredSize = FindRequiredSize();
		m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
	}


	private float FindRequiredSize() {
		Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);
		
		float size = 0f;

		foreach (var Enemy in m_EnemyTargets ) {
			if ( !Enemy.gameObject.activeSelf ) continue;
			

			Vector3 targetLocalPos = transform.InverseTransformPoint(Enemy.position);
			
			Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
			
			size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
			
			size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
		}
		
		size += m_ScreenEdgeBuffer;
		
		size = Mathf.Max(size, m_MinSize);

		return size;
	}


	public void SetStartPositionAndSize() {
		FindAveragePosition();
		
		transform.position = m_DesiredPosition;
		
		m_Camera.orthographicSize = FindRequiredSize();
	}
}