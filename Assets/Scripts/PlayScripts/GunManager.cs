using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
	public float _bulletDamage { get; set; }
	public float _bulletSpeed { get; set; }
	

	protected Rigidbody rid;
	protected Transform bulletTr;

	protected virtual void Awake() { }

	protected virtual void OnEnable() { }

	protected virtual void OnDisable() { }
}
