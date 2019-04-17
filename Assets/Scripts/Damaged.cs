using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damaged : MonoBehaviour
{
	public float initHp = 100f;
	[SerializeField]
	private float currHp;
	public Image hpBar;
	
	private const string attackTag = "ATTACKING";

	private void Awake() {
		currHp = initHp;
	}

	private void OnTriggerEnter( Collider other ) {
		if(other.tag == attackTag ) {
			currHp -= EnemyAI.init.attack_Damage;
			DisplayHpbar();
		}
	}

	private void DisplayHpbar() {
		hpBar.fillAmount = (currHp / initHp);
	}
}
