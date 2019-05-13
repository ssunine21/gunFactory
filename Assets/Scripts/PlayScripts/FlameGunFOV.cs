using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGunFOV : MonoBehaviour
{
	public float viewRange = 15f;

	[Range(0, 360)]
	public float viewAngle = 60f;
	private Transform enemyTr;
	private Transform playerTr;
	private float flameGunDamage;
	public LayerMask enemyLayer;
	public LayerMask obstacleLayer;


	private void Start() {
		playerTr = GetComponent<Transform>();
		flameGunDamage = this.GetComponentInParent<Gun>()._bulletDamage;
	}

	private void Update() {
		if ( isEnemy() && FireCtrl.init.isShotFlame)
			Debug.Log("d");
	}

	public Vector3 CirclePoint(float angle ) {
		angle += transform.eulerAngles.y;
		return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
	}

	public bool isEnemy() {
		bool isEnemy = false;

		Collider[] colls = Physics.OverlapSphere(playerTr.position, viewRange, enemyLayer);

		foreach(Collider coll in colls ) {
			enemyTr = coll.transform;
			Vector3 dir = (enemyTr.position - playerTr.position).normalized;
			
			if ( Vector3.Angle(playerTr.forward, dir) < viewAngle * 0.5f ) {
				if ( FireCtrl.init.isShotFlame ) {
					Attack(coll);
				}
			}
		}

		return isEnemy;
	}

	public void Attack( Collider coll) {
		coll.gameObject.GetComponent<EnemyDamaged>().Damaged(flameGunDamage);
	}
}
