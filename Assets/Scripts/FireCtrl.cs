using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
	public GameObject bullet;
	public Transform firePos;

    void Start() {
        
    }
	

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
			Fire();
		}
    }

	private void Fire() {
		Instantiate(bullet, firePos.position, firePos.rotation);
	}
}
