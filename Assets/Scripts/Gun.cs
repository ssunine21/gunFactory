﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : GunManager
{
	protected override void Awake() {
		_bulletDamage = 20f;
		_bulletSpeed = 800f;
		
		rid = GetComponent<Rigidbody>();
		bulletTr = GetComponent<Transform>();
	}

	protected override void OnEnable() {
		rid.AddForce(bulletTr.forward * _bulletSpeed);
	}

	protected override void OnDisable() {
		bulletTr.position = Vector3.zero;
		bulletTr.rotation = Quaternion.identity;

		rid.Sleep();
	}
}
