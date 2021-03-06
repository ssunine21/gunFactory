﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum WeaponType {
	GUN = 0,
	SHOTGUN,
	FIREGUN

}

public class Gun : MonoBehaviourPun {
	public WeaponType weaponType;
	public float _bulletSpeed = 800f;
	public float _bulletDamage = 20f;
	public float _reloadSpeed = 0.5f;
	public int _maxBullet = 50;

	private int _currBullet = 0;
	public int CurrBullet {
		get { return _currBullet; }
		set {
			if (value < 0)
				_currBullet = _maxBullet;
			else
				_currBullet = value;
		}
	}

	private Rigidbody rid;
	private Transform bulletTr;
	
	private void Awake() {
		CurrBullet = _maxBullet;
		rid = GetComponent<Rigidbody>();
		bulletTr = GetComponent<Transform>();
	}

	private void OnEnable() {
		rid.AddForce(bulletTr.forward * _bulletSpeed);
		Destroy(this.gameObject, 2.0f);
	}

	private void OnDisable() {
		bulletTr.position = Vector3.zero;
		bulletTr.rotation = Quaternion.identity;

		rid.Sleep();
	}

	private void OnCollisionEnter( Collision collision ) {
			this.gameObject.SetActive(false);
	}
}
