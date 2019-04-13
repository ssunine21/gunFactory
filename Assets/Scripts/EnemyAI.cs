using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public enum State {
		PATRAL, TRACE, ATTACK, DIE
	}

	public State state = State.PATRAL;

	private Transform playerTr;
	private Transform enemyTr;
	private WaitForSeconds stateCheck_time;
	private float distance_fromPlayer;

	public float attack_Distance = 5f;
	public float trace_Distance = 5f;

	public bool isDie = false;

	private void Awake() {
		playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		enemyTr = GetComponent<Transform>();

		stateCheck_time = new WaitForSeconds(0.3f);
	}

	private void OnEnable() {
		StartCoroutine(CheckState());
	}

	IEnumerator CheckState() {
		while ( !isDie ) {
			if ( isDie ) StopCoroutine(CheckState());

			distance_fromPlayer = Vector3.Distance(enemyTr.position, playerTr.position);

			if ( distance_fromPlayer <= attack_Distance ) state = State.ATTACK;
			else if ( distance_fromPlayer <= trace_Distance ) state = State.TRACE;
			else state = State.PATRAL;

			yield return stateCheck_time;
		}
	}
}
