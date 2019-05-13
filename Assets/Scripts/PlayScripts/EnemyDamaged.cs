using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamaged : MonoBehaviour
{
	public float initHp = 100f;
	private float hp = 100f;
	public float bloodEffect_destroyDelay = 1f;

	private const string bulletTeg = "BULLET";
	private GameObject bloodEffect;

	private void Start() {
		bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
		hp = initHp;
	}

	private void Update() {
		if ( hp <= 0f )
			GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
	}

	private void OnCollisionEnter(Collision collision) {
		if(collision.collider.tag == bulletTeg) {
			ShowBloodEffect(collision);
			collision.gameObject.SetActive(false);
			hp -= collision.gameObject.GetComponent<Gun>()._bulletDamage;
			Debug.Log(hp);
		}
	}

	private void ShowBloodEffect(Collision coll) {
		Vector3 hitPos = coll.contacts[0].point;
		Vector3 _normal = coll.contacts[0].normal;

		Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);


		GameObject blood = Instantiate(bloodEffect, hitPos, rot);
		Destroy(blood, bloodEffect_destroyDelay);
	}

	public void Damaged(float value) {
		hp -= value;
		Debug.Log(hp);
	}
}
