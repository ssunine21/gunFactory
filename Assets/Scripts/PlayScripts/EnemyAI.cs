using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public enum State {
		PATRAL, TRACE, ATTACK, DIE
	}

	private int dieCount = 0;

	public State state = State.PATRAL;
	public AudioClip dieSfx;

	private AudioSource _audio;

	private GameObject dieEffect;
	private Transform attackRange;
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
	private readonly int hash_attackIdx = Animator.StringToHash("attackIdx");

	public float attack_Distance = 5f;
	public float trace_Distance = 5f;
	public float attack_Delay = 3f;
	public float die_Delay = 1f;
	public float attack_Damage = 10f;
	public float rotSpeed = 10f;

	public bool isDie = false;

	private void Awake() {
		init = this;

		dieEffect = Resources.Load<GameObject>("monsterDead");
		playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		enemyTr = GetComponent<Transform>();
		moveAgent = GetComponent<MoveAgent>();
		animator = GetComponent<Animator>();
		_audio = GetComponent<AudioSource>();
		attackRange = this.transform.Find("attackRange");

		stateCheck_time = new WaitForSeconds(0.3f);
	}

	private void OnEnable() {
		StartCoroutine(CheckState());
		StartCoroutine(Action());
	}

	private void Update() {
		if (isAttack) {
			Vector3 dir = playerTr.position - enemyTr.position;
			enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);

			if ( Time.time > nextAttack ) {
				Attack();
				nextAttack = Time.time + attack_Delay;
				attackRange.gameObject.SetActive(true);
			}

			else attackRange.gameObject.SetActive(false);
		}
	}

	private void Attack() {
		animator.SetTrigger(hash_isAttack);
		animator.SetInteger(hash_attackIdx, Random.Range(0, 2));
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
					dieCount++;
					if ( Quest.init.QuestIdx == 0 ) Quest.init.QuestUpdate();

					isDie = true;
					isAttack = false;
					_audio.PlayOneShot(dieSfx, 1f);
					enemyTr.Find("Cylinder").gameObject.SetActive(false);
					GameObject effect = Instantiate(dieEffect, enemyTr.position, enemyTr.rotation);
					Destroy(effect, 2f);

					moveAgent.Stop();
					animator.SetBool(hash_isDie, true);
					GetComponent<CapsuleCollider>().enabled = false;
					Destroy(gameObject, die_Delay);
					break;
			}
		}
	}

	public static EnemyAI init;
}
