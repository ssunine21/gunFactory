using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGun : GunManager {

	protected override void Awake() {
		_bulletDamage = 20f;

		rid = GetComponent<Rigidbody>();
		bulletTr = GetComponent<Transform>();
	}

	protected override void OnDisable() {
		bulletTr.position = Vector3.zero;
		bulletTr.rotation = Quaternion.identity;

		//rid.Sleep();
	}
}
