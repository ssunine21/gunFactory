using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
	public float patrolSpeed = 2f;
	public float traceSpeed = 5f;

	private NavMeshAgent agent;

	private bool _patrolling;
	public bool patrolling {
		get { return _patrolling; }
		set {
			_patrolling = value;
			if (_patrolling) {
				agent.speed = patrolSpeed;
			}
		}
	}

	private Vector3 _traceTarget;
	public Vector3 traceTarget {
		get { return _traceTarget; }
		set {
			_traceTarget = value;
			agent.speed = traceSpeed;
			TraceTarget(_traceTarget);
		}
	}

	public float speed {
		get { return agent.velocity.magnitude; }
	}

	private void Awake() {
		agent = GetComponent<NavMeshAgent>();

		//목적지가 가까워질수록 속도를 줄이는 옵션 비활성화
		agent.autoBraking = false;
	}

	private void Start() {
		agent.speed = patrolSpeed;

		this.patrolling = true;
	}

	private void TraceTarget(Vector3 pos) {
		if (agent.isPathStale) return;

		agent.destination = pos;
		agent.isStopped = false;
	}

	public void Stop() {
		agent.isStopped = true;
		agent.velocity = Vector3.zero;
		_patrolling = false;
	}
}
