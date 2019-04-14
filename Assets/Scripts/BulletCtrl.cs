using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
	public float bulletDamage = 20.0f;
	public float bulletSpeed = 1000.0f;

    void Start() {
		GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }
}
