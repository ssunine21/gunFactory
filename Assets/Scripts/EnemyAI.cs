using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public enum State {
		PATRAL, TRACE, ATTACK, DIE
	}

	public State state = State.PATRAL;

	private MoveAgent moveAgent;

	private Transform playerTr;
	private Transform enemyTr;
	private Animator animator;
	private WaitForSeconds stateCheck_time;
	private float distance_fromPlayer;
	private bool isAttack = false;
	private float nextAttack = 0f;

	private readonly int hash_isMove = Animator.StringToHash("isMove");
	private readonly int hash_isDie = Animator.StringToHash("isDie");
	private readonly int hash_isAttack = Animator.StringToHash("isAttack");

	public float attack_Distance = 5f;
	public float trace_Distance = 5f;
	public float attack_Delay = 3f;
	public float die_Delay = 3f;

	public bool isDie = false;

	private void Awake() {
		playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		enemyTr = GetComponent<Transform>();
		moveAgent = GetComponent<MoveAgent>();
		animator = GetComponent<Animator>();

		stateCheck_time = new WaitForSeconds(0.3f);
	}

	private void OnEnable() {
		StartCoroutine(CheckState());
		StartCoroutine(Action());
	}

	private void Update() {
		if (isAttack) {
			if(Time.time > nextAttack) {
				Attack();
				nextAttack = Time.time + attack_Delay;
			}
		}
	}

	private void Attack() {
		animator.SetTrigger(hash_isAttack);
	}

	IEnumerator CheckState() {
		while ( !isDie ) {
			if (state == State.DIE) yield break;

			distance_fromPlayer = Vector3.Distance(enemyTr.position, playerTr.position);

			if ( distance_fromPlayer <= attack_Distance ) state = State.ATTACK;
			else if ( distance_fromPlayer <= trace_Distance ) state = State.TRACE;
			else state = State.PATRAL;

			yield return stateCheck_time;
		}
	}

	IEnumerator Action() {
		while (!isDie) {
			yield return stateCheck_time;

			switch (state) {
				case State.PATRAL:
					isAttack = false;
					moveAgent.patrolling = true;
					animator.SetBool(hash_isMove, true);
					break;
				case State.TRACE:
					isAttack = false;
					moveAgent.traceTarget = playerTr.position;
					animator.SetBool(hash_isMove, true);
					break;
				case State.ATTACK:
					if (!isAttack) isAttack = true;
					moveAgent.Stop();
					animator.SetBool(hash_isMove, false);
					break;
				case State.DIE:
					isDie = true;
					isAttack = false;
					moveAgent.Stop();
					animator.SetBool(hash_isDie, true);
					GetComponent<CapsuleCollider>().enabled = false;

					Destroy(gameObject, die_Delay);
					break;
			}
		}
	}
}
