using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
	public float bulletDamage = 20.0f;
	public float bulletSpeed = 1000.0f;

	private Rigidbody rid;
	private Transform bulletTr;

	private void Awake() {
		rid = GetComponent<Rigidbody>();
		bulletTr = GetComponent<Transform>();
	}

	private void OnEnable() {
		rid.AddForce(bulletTr.forward * bulletSpeed);
    }

	private void OnDisable() {
		bulletTr.position = Vector3.zero;
		bulletTr.rotation = Quaternion.identity;

		rid.Sleep();
	}
}
